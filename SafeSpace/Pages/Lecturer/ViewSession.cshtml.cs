using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeSpace.Models;
using Microsoft.EntityFrameworkCore;

namespace SafeSpace.Pages.Lecturer
{
    public class ViewSessionModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public ViewSessionModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        public string SessionTime { get; set; }
        public string SessionTitle { get; set; }
        public int Occupancy { get; set; }
        public int NrOfSeats { get; set; }
        public Session Session { get; set; }
        public IList<Seat> Seat { get; set; }
        public IList<Course> Course { get; set; }
        public IList<ClassRoom> Classroom { get; set; }
        public IList<Models.Student> Students { get; set; }
        public IList<Seat> OccupiedSeats { get; set; }
        public IList<Seat> Seats { get; set; }
        public bool Ended { get; set; }
        public string BackGroundImage { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Seat = await _context.Seat.ToListAsync();
            Course = await _context.Course.ToListAsync();
            Classroom = await _context.ClassRoom.ToListAsync();
            Students = await _context.Student.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }
            Session = await _context.Session.FirstOrDefaultAsync(m => m.ID == id);
            if (Session == null)
            {
                return NotFound();
            }
            if (Session.Time < DateTime.Now)
            {
                Ended = true;
            }
            Seats = Session.ClassRoom.Seats;
            SessionTitle = Session.CourseName;
            var endTime = Session.Time.AddMinutes(105);
            var month = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(Session.Time.Month);
            SessionTime = Session.Time.ToString("HH:mm") + "-" + endTime.ToString("HH:mm") + ", " + month + " " + Session.Time.ToString("dd") + ", classroom " + Session.ClassRoom.Number;
            NrOfSeats = Seats.Count;
            Occupancy = Seats.Where(s => s.Student.Name != "-").Count();
            OccupiedSeats = Seats.Where(s => s.Student.Name != "-").ToList();
            if (Session.ClassRoom.Number == 2)
            {
                BackGroundImage = "https://safespacestorage.blob.core.windows.net/container/pyhavaimu-auditoorium-png.png";
            }
            else
            {
                BackGroundImage = "https://safespacestorage.blob.core.windows.net/container/pyhavaimu-auditoorium_2.png";
            }
            return Page();
        }
    }
}
