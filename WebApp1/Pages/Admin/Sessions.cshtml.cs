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
    public class SessionsModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public SessionsModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        public IList<Seat> Seats { get; set; }
        public IList<Course> Courses { get; set; }
        public IList<ClassRoom> Classrooms { get; set; }
        public IList<Session> UpcomingSessions { get; set; }
        public IList<Session> PastSessions { get; set; }
        public IList<Models.Student> Students { get; set; }
        public IList<Cases> Cases { get; set; }
        public async Task OnGetAsync()
        {
            Seats = await _context.Seat.ToListAsync();
            Cases = await _context.Cases.ToListAsync();
            Courses = await _context.Course.ToListAsync();
            Classrooms = await _context.ClassRoom.ToListAsync();
            Students = await _context.Student.Where(s => !s.isModel).ToListAsync();
            UpcomingSessions = await _context.Session.Where(s => s.Time > DateTime.Now).ToListAsync();
            PastSessions = await _context.Session.Where(s => s.Time <= DateTime.Now).ToListAsync();
        }
    }
}
