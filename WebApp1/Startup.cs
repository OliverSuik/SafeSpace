using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp1.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebPWrecover.Services;
using WebApp1.Services;

namespace WebApp1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<RazorPagesSeatsContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("RazorPagesSeatsContext")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddServerSideBlazor();
            services.AddAuthorization(ops =>
            {
                ops.AddPolicy("Admin", policy =>
                {
                    policy.RequireRole("Admin");
                });

                ops.AddPolicy("Lecturer", policy =>
                {
                    policy.RequireRole("Lecturer");
                });

                ops.AddPolicy("Student", policy =>
                {
                    policy.RequireRole("Student");
                });

                ops.AddPolicy("Sysadmin", policy =>
                {
                    policy.RequireRole("Sysadmin");
                });
            });
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin", "Admin");
                options.Conventions.AuthorizeFolder("/Sysadmin", "Sysadmin");
                options.Conventions.AuthorizeFolder("/Lecturer", "Lecturer");
                options.Conventions.AuthorizeFolder("/Student", "Student");
                options.Conventions.AuthorizePage("/CreateClassroom", "Admin");
            });
            services.AddRazorPages().AddMvcOptions(options =>
            {
                options.MaxModelValidationErrors = 50;
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    _ => "The field is required.");
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapBlazorHub();
            });
        }
    }
}
