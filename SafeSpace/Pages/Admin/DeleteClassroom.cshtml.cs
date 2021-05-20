using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeSpace.Models;
using Microsoft.EntityFrameworkCore;

namespace SafeSpace.Pages.Admin
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
        public IList<Seat> Seats { get; set; }
        public IList<Models.Student> Students { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Seats = await _context.Seat.ToListAsync();
            Students = await _context.Student.ToListAsync();

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

            Seats = await _context.Seat.ToListAsync();
            Students = await _context.Student.ToListAsync();
            Classroom = await _context.ClassRoom.FindAsync(id);

            if (Classroom != null)
            {
                foreach (Seat seat in Classroom.Seats)
                {
                    _context.Student.Remove(seat.Student);
                    _context.Seat.Remove(seat);
                }
                _context.ClassRoom.Remove(Classroom);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("ClassroomList");
        }
    }
}
