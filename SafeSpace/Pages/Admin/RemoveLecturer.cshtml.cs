using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeSpace.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SafeSpace.Pages.Admin
{
    public class RemoveLecturerModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public RemoveLecturerModel(UserManager<IdentityUser> userManager, Data.RazorPagesSeatsContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public IdentityUser Lecturer { get; set; }
        public IList<Course> Courses { get; set; }
        public IList<Models.Lecturer> Lecturers { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            Lecturer = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            Courses = await _context.Course.ToListAsync();
            Lecturers = await _context.Lecturer.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            var lecturer = await _context.Lecturer.FirstOrDefaultAsync(m => m.Email == user.Email);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (lecturer == null)
            {
                return NotFound($"Unable to load lecturer.");
            }
            var courses = await _context.Course.ToListAsync();
            var isTeaching = false;
            foreach (Course course in courses)
            {
                if (course.Lecturer.ID == lecturer.ID)
                {
                    isTeaching = true;
                }
            }
            if (!isTeaching)
            {
                _context.Lecturer.Remove(lecturer);
                await _context.SaveChangesAsync();
                var result = await _userManager.DeleteAsync(user);
                return RedirectToPage("ManageLecturers");
            }

            return RedirectToPage("LecturerError");
        }
    }
}
