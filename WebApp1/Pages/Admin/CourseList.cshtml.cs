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
    public class CourseListModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public CourseListModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        public IList<Seat> Seats { get; set; }
        public IList<Course> Courses { get; set; }
        public IList<ClassRoom> Classrooms { get; set; }
        public IList<Models.Lecturer> Lecturers { get; set; }
        public async Task OnGetAsync()
        {
            Seats = await _context.Seat.ToListAsync();
            Courses = await _context.Course.Where(c => c.isModel).ToListAsync();
            Classrooms = await _context.ClassRoom.ToListAsync();
            Lecturers = await _context.Lecturer.ToListAsync();
        }
    }
}
