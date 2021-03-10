using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApp1.Areas.Identity.Pages.Account.Manage
{
    public class PositiveModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public PositiveModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        public IList<Seat> Seats { get; set; }
        public IList<Course> Courses { get; set; }
        public IList<ClassRoom> Classrooms { get; set; }
        public IList<Session> PastSessions { get; set; }
        public IList<Models.Student> Students { get; set; }
        public List<Session> Sessions { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty, MaxLength(300)]
        public string MainText { get; set; }
        public async Task OnGetAsync()
        {
            Seats = await _context.Seat.ToListAsync();
            Courses = await _context.Course.ToListAsync();
            Classrooms = await _context.ClassRoom.ToListAsync();
            Students = await _context.Student.Where(s => !s.isModel).ToListAsync();
            PastSessions = await _context.Session.Where(s => s.Time <= DateTime.Now).ToListAsync();
            List<string> sessionNames = new List<string>();
            List<Session> sessions = new List<Session>();
            foreach (Session session in PastSessions)
            {
                foreach (Seat seat in session.ClassRoom.Seats)
                {
                    if (seat.Student.Name == User.Identity.Name)
                    {
                        sessionNames.Add(session.Name);
                        sessions.Add(session);
                    }
                }
            }
            Sessions = sessions;
            string output = string.Join(Environment.NewLine, sessionNames.ToArray());
            MainText = "I have tested positive for COVID-19. Sessions I have attended:" + "\n" + output;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (MainText != "")
            {
                Cases covidCase = new Cases {Name = User.Identity.Name, Description = MainText };
                _context.Cases.Add(covidCase);
                await _context.SaveChangesAsync();
                StatusMessage = "Your message has been sent.";
            }
            return RedirectToPage();
        }
    }
}
