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
    public class RemoveStudentModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public RemoveStudentModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Models.Student Student { get; set; }
        public Seat Seat { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student.FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null)
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

            Student = await _context.Student.FirstOrDefaultAsync(m => m.ID == id);
            Seat = await _context.Seat.FirstOrDefaultAsync(s => s.Student.ID == id);
            Seat.Student = new Models.Student();
            if (Student != null)
            {
                _context.Attach(Seat).State = EntityState.Modified;
                _context.Student.Remove(Student);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Sessions");
        }
    }
}
