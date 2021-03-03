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
    public class DeleteClassroomModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public DeleteClassroomModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ClassRoom Classroom { get; set; }
        public IList<Seat> Seat { get; set; }
        public IList<User> User { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Seat = await _context.Seat.ToListAsync();
            User = await _context.User.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }

            Classroom = await _context.ClassRoom.FirstOrDefaultAsync(m => m.ID == id);

            if (Classroom == null)
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

            Seat = await _context.Seat.ToListAsync();
            User = await _context.User.ToListAsync();
            Classroom = await _context.ClassRoom.FindAsync(id);

            if (Classroom != null)
            {
                foreach (Seat seat in Classroom.Seats)
                {
                    _context.User.Remove(seat.User);
                    _context.Seat.Remove(seat);
                }
                _context.ClassRoom.Remove(Classroom);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("DeleteClassroomList");
        }
    }
}
