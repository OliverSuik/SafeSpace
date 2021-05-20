using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeSpace.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace SafeSpace.Pages
{
    public class TestModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        public TestModel(Data.RazorPagesSeatsContext context, UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public IList<Course> Courses { get; set; }
        public IList<IdentityUser> Users { get; set; }
        [BindProperty]
        public string TestInt { get; set; }
        public async Task OnGetAsync()
        {
            Courses = await _context.Course.ToListAsync();
            Users = await _userManager.Users.ToListAsync();
            //await _emailSender.SendEmailAsync("suik99@ut.ee", "Confirm your email","Please work");
        }
    }
}
