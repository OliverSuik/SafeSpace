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
    public class ViewClassroomModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public ViewClassroomModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        public IList<Seat> Seats { get; set; }
        public IList<ClassRoom> Classrooms { get; set; }
        public ClassRoom Classroom { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Seats = await _context.Seat.ToListAsync();
            Classrooms = await _context.ClassRoom.ToListAsync();

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
    }
}
