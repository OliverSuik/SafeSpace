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
    public class ViewSessionModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public ViewSessionModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        public IList<Models.Student> Students { get; set; }
        public Session Session { get; set; }
        public IList<Seat> Seats { get; set; }
        public IList<Seat> OccupiedSeats { get; set; }
        public string SessionTitle { get; set; }
        public string SessionTime { get; set; }
        public int NrOfSeats { get; set; }
        public int Occupancy { get; set; }
        public IList<Seat> Seat { get; set; }
        public IList<Course> Course { get; set; }
        public IList<ClassRoom> Classroom { get; set; }
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
            Seats = Session.ClassRoom.Seats;
            SessionTitle = Session.Course.Name + ", classroom " + Session.ClassRoom.Number;
            SessionTime = Session.Time.ToString("HH:mm dd.MM");
            NrOfSeats = Seats.Count;
            Occupancy = Seats.Where(s => s.Student.Name != "-").Count();
            OccupiedSeats = Seats.Where(s => s.Student.Name != "-").ToList();
            if (Session == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
