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

namespace SafeSpace.Pages.Admin
{
    public class ManageLecturersModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ManageLecturersModel(Data.RazorPagesSeatsContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IList<Models.Lecturer> Lecturers { get; set; }
        public IList<RegistrationToken> RegistrationTokens { get; set; }
        public IList<IdentityUser> UsersOfRole { get; set; }
        public async Task OnGetAsync()
        {
            Lecturers = await _context.Lecturer.ToListAsync();
            UsersOfRole = await _userManager.GetUsersInRoleAsync("Lecturer");
            RegistrationTokens = await _context.RegistrationToken.Where(t => t.Role == "Lecturer").ToListAsync();
        }
    }
}
