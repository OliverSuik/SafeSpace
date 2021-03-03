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
    public class DeleteSessionModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public DeleteSessionModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Session Session { get; set; }
        public IList<Seat> Seat { get; set; }
        public IList<Seat> Seats { get; set; }
        public IList<Course> Course { get; set; }
        public IList<User> User { get; set; }
        public IList<ClassRoom> Classroom { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Seat = await _context.Seat.ToListAsync();
            Course = await _context.Course.ToListAsync();
            Classroom = await _context.ClassRoom.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }

            Session = await _context.Session.FirstOrDefaultAsync(m => m.ID == id);

            if (Session == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Seat = await _context.Seat.ToListAsync();
            Course = await _context.Course.ToListAsync();
            Classroom = await _context.ClassRoom.ToListAsync();
            User = await _context.User.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }

            Session = await _context.Session.FindAsync(id);


            if (Session != null)
            {
                foreach (Seat seat in Session.ClassRoom.Seats)
                {
                    _context.User.Remove(seat.User);
                    _context.Seat.Remove(seat);
                }
                _context.ClassRoom.Remove(Session.ClassRoom);
                _context.Session.Remove(Session);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Sessions");
        }
    }
}
