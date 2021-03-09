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
    public class RemoveLecturerModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public RemoveLecturerModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Lecturer Lecturer { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Lecturer = await _context.Lecturer.FirstOrDefaultAsync(m => m.ID == id);

            if (Lecturer == null)
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

            Lecturer = await _context.Lecturer.FindAsync(id);

            if (Lecturer != null)
            {
                _context.Lecturer.Remove(Lecturer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("ManageLecturers");
        }
    }
}
