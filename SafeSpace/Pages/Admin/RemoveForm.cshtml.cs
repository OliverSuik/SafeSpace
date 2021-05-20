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
    public class RemoveFormModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public RemoveFormModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        [BindProperty]
        public RegistrationToken Token { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Token = await _context.RegistrationToken.FirstOrDefaultAsync(m => m.ID == id);
            if (Token == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Token = await _context.RegistrationToken.FirstOrDefaultAsync(m => m.ID == id);
            if (Token == null)
            {
                return NotFound();
            }
            _context.RegistrationToken.Remove(Token);
            await _context.SaveChangesAsync();
            return RedirectToPage("ManageLecturers");
        }
    }
}
