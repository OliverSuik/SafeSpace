using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeSpace.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SafeSpace.Pages
{
    public class SelectSessionModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public SelectSessionModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        public IList<Course> Courses { get; set; }
        public IList<ClassRoom> Classrooms { get; set; }
        public IList<Session> Sessions { get; set; }
        public IList<Session> UpcomingSessions { get; set; } = new List<Session>();
        public IList<Seat> Seats { get; set; }
        public IList<Models.Student> Students { get; set; }
        public SelectList SelectSession { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Please select a session.")]
        public int SelectedSessionId { get; set; }
        public string Week { get; set; }
        public async Task OnGetAsync()
        {
            Courses = await _context.Course.ToListAsync();
            Classrooms = await _context.ClassRoom.ToListAsync();
            Sessions = await _context.Session.Where(s => s.Time > DateTime.Now).ToListAsync();
            Sessions = Sessions.OrderBy(s => s.Time).ToList();
            Seats = await _context.Seat.ToListAsync();
            Students = await _context.Student.ToListAsync();
            SelectSession = new SelectList(Sessions, nameof(Session.ID), nameof(Session.Name), null, nameof(Session.CourseName));
            foreach (Session sess in Sessions)
            {
                foreach (Seat seat in sess.ClassRoom.Seats)
                {
                    if (seat.Student.Name == User.Identity.Name)
                    {
                        UpcomingSessions.Add(sess);
                        break;
                    }
                }
            }
            UpcomingSessions = UpcomingSessions.OrderBy(s => s.Time).ToList();
            DayOfWeek day = DateTime.Now.DayOfWeek;
            int days = day - DayOfWeek.Monday;
            DateTime start = DateTime.Now.AddDays(-days);
            Week = start.ToString("dd.MM") + "-" + start.AddDays(6).ToString("dd.MM");

        }
        public IActionResult OnPost()
        {
            return RedirectToPage("PickSeats", new { id = SelectedSessionId });
        }
    }
}
