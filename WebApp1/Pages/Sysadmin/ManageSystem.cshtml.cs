using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApp1.Pages.Sysadmin
{
    public class ManageSystemModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ManageSystemModel(Data.RazorPagesSeatsContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IList<Models.Admin> Admins { get; set; }
        public IList<RegistrationToken> RegistrationTokens { get; set; }
        public IList<IdentityUser> Users { get; set; }
        public IList<IdentityUser> UsersOfRole { get; set; }
        public GlobalVariables GlobalVariables { get; set; }
        public async Task OnGetAsync()
        {
            Admins = await _context.Admin.ToListAsync();
            RegistrationTokens = await _context.RegistrationToken.Where(t => t.Role == "Admin").ToListAsync();
            Users = await _userManager.Users.ToListAsync();
            UsersOfRole = await _userManager.GetUsersInRoleAsync("Admin");
            GlobalVariables = _context.GlobalVariables.First();
        }
    }
}
