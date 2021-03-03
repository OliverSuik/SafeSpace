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
        public IList<Seat> Seat { get; set; }
        public IList<Course> Course { get; set; }
        public IList<ClassRoom> Classroom { get; set; }
        public IList<Session> Session { get; set; }
        public IList<User> User { get; set; }

        public async Task OnGetAsync()
        {
            Seat = await _context.Seat.ToListAsync();
            Course = await _context.Course.ToListAsync();
            Classroom = await _context.ClassRoom.ToListAsync();
            Session = await _context.Session.ToListAsync();
            User = await _context.User.Where(s => !s.isModel).ToListAsync();
        }
    }
}
