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
    public class RemoveStudentModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public RemoveStudentModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        [BindProperty]
        public User User { get; set; }
        public Seat Seat { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User.FirstOrDefaultAsync(m => m.ID == id);


            if (User == null)
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

            User = await _context.User.FirstOrDefaultAsync(m => m.ID == id);
            Seat = await _context.Seat.FirstOrDefaultAsync(s => s.User.ID == id);
            Seat.User = new User();
            if (User != null)
            {
                _context.Attach(Seat).State = EntityState.Modified;
                _context.User.Remove(User);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Sessions");
        }
    }
}
