using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebApp1.Models;

namespace WebApp1.Areas.Identity.Pages.Account
{
    public class RegisterAdminModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        public RegisterAdminModel(Data.RazorPagesSeatsContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public DateTime Exp_time { get; set; }
        public string UserEmail { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public class InputModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var token = await _context.RegistrationToken.FirstOrDefaultAsync(m => m.Token == id);
            Exp_time = token.ExpirationTime;
            UserEmail = token.Email;

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var token = await _context.RegistrationToken.FirstOrDefaultAsync(m => m.Token == id);
            if (token == null)
            {
                return NotFound();
            }
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                //System.Diagnostics.Debug.WriteLine(UserEmail);
                await CreateRole();
                var user = new IdentityUser { UserName = token.Email, Email = token.Email, EmailConfirmed = true};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (token.Role == "Admin")
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                } else
                {
                    await _userManager.AddToRoleAsync(user, "Lecturer");
                }
                
                if (result.Succeeded)
                {
                    _context.RegistrationToken.Remove(token);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToPage("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }

        public async Task CreateRole()
        {
            bool admin = await _roleManager.RoleExistsAsync("Admin");
            if (!admin)
            {
                // first we create Admin role 
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                await _roleManager.CreateAsync(role);
            }
            bool lecturer = await _roleManager.RoleExistsAsync("Lecturer");
            if (!lecturer)
            {
                // first we create Admin role 
                var role = new IdentityRole
                {
                    Name = "Lecturer"
                };
                await _roleManager.CreateAsync(role);
            }
        }
    }
}
