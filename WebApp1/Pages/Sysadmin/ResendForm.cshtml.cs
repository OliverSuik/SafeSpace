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
using Microsoft.EntityFrameworkCore;

namespace WebApp1.Pages.Sysadmin
{
    public class ResendFormModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        private readonly IEmailSender _emailSender;
        public ResendFormModel(Data.RazorPagesSeatsContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        public String Email { get; set; }
        public RegistrationToken Token { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Token = await _context.RegistrationToken.FirstOrDefaultAsync(m => m.ID == id);
            if (Token== null)
            {
                return NotFound();
            }
            Email = Token.Email;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Token = await _context.RegistrationToken.FirstOrDefaultAsync(m => m.ID == id);
            if (Token == null)
            {
                return NotFound();
            }
            DateTime d_time = DateTime.Now;
            RegistrationToken regToken = new RegistrationToken
            {
                GenerateTime = d_time,
                ExpirationTime = d_time.AddDays(2),
                Email = Token.Email,
                Role = "Admin"
            };
            _context.RegistrationToken.Add(regToken);
            _context.RegistrationToken.Remove(Token);
            await _context.SaveChangesAsync();

            var callbackUrl = Url.Page(
            "/Account/RegisterAdmin",
            pageHandler: null,
            values: new { area = "Identity", id = regToken.Token },
            protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(regToken.Email, "Create account",
                $"Please register your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return RedirectToPage("ManageSystem");
        }
    }
}
