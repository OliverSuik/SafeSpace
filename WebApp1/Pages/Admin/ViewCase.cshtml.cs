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
    public class ViewCaseModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public ViewCaseModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Cases CovidCase { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CovidCase = await _context.Cases.FirstOrDefaultAsync(m => m.ID == id);

            if (CovidCase == null)
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

            CovidCase = await _context.Cases.FindAsync(id);

            if (CovidCase != null)
            {
                _context.Cases.Remove(CovidCase);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Sessions");
        }
    }
}
