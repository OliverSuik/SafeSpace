using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp1.Pages.Admin
{
    public class DeleteCourseModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public DeleteCourseModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Course Course { get; set; }
        public IList<Seat> Seats { get; set; }
        public IList<ClassRoom> Classrooms { get; set; }
        public IList<Models.Lecturer> Lecturers { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Seats = await _context.Seat.ToListAsync();
            Classrooms = await _context.ClassRoom.ToListAsync();
            Lecturers = await _context.Lecturer.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course.FirstOrDefaultAsync(m => m.ID == id);

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course.FindAsync(id);

            if (Course != null)
            {
                _context.Course.Remove(Course);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("CourseList");
        }
    }
}
