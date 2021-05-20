using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SafeSpace.Models;

namespace SafeSpace.Pages.Admin
{
    public class AddLecturerModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        private readonly IEmailSender _emailSender;
        public AddLecturerModel(Data.RazorPagesSeatsContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        [BindProperty]
        public Models.Lecturer Lecturer { get; set; }
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
            RegistrationToken regToken = new RegistrationToken {GenerateTime = d_time, ExpirationTime = d_time.AddDays(expirationTime),
            Email = Lecturer.Email, Role = "Lecturer", Name = Lecturer.Name};
            _context.RegistrationToken.Add(regToken);
            await _context.SaveChangesAsync();
            var callbackUrl = Url.Page(
            "/Account/RegisterAdmin",
            pageHandler: null,
            values: new { area = "Identity", id = regToken.Token },
            protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(Lecturer.Email, "Create account",
                $"Welcome to SafeSpace!<br>You have been authorized as a lecturer. Please register your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return RedirectToPage("ManageLecturers");
        }
    }
}
