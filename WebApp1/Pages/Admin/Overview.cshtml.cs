using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp1.Models;

namespace WebApp1.Pages.Admin
{
    public class OverviewModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public OverviewModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        public IList<Seat> Seats { get; set; }
        public IList<Models.Student> Students { get; set; }
        public IList<Course> Courses { get; set; }
        public IList<ClassRoom> Classrooms { get; set; }
        public IList<Session> Sessions { get; set; }
        public IList<Models.Lecturer> Lecturers { get; set; }

        public async Task OnGetAsync()
        {
            Seats = await _context.Seat.ToListAsync();
            Students = await _context.Student.ToListAsync();
            Courses = await _context.Course.ToListAsync();
            Classrooms = await _context.ClassRoom.ToListAsync();
            Sessions = await _context.Session.ToListAsync();
            Lecturers = await _context.Lecturer.ToListAsync();
        }
    }
}
