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
using WebApp1.Models;

namespace WebApp1.Pages.Sysadmin
{
    public class AddAdminModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        private readonly IEmailSender _emailSender;
        public AddAdminModel(Data.RazorPagesSeatsContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        [BindProperty]
        public Models.Admin Admin { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var expirationTime = _context.GlobalVariables.First().TokenExpirationDays;
            DateTime d_time = DateTime.Now;
            RegistrationToken regToken = new RegistrationToken
            {
                GenerateTime = d_time,
                ExpirationTime = d_time.AddDays(expirationTime),
                Email = Admin.Email,
                Role = "Admin"
            };
            _context.RegistrationToken.Add(regToken);
            await _context.SaveChangesAsync();

            var callbackUrl = Url.Page(
                        "/Account/RegisterAdmin",
                        pageHandler: null,
                        values: new { area = "Identity", id = regToken.Token},
                        protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(Admin.Email, "Create account",
                $"Please register your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");


            return RedirectToPage("ManageSystem");
        }
    }
}
