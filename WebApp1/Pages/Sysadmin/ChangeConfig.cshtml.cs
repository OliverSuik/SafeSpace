using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp1.Pages.Sysadmin
{
    public class ChangeConfigModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public ChangeConfigModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        [BindProperty]
        public GlobalVariables GlobalVar { get; set; }
        public async Task OnGetAsync(int? id)
        {
            GlobalVar = await _context.GlobalVariables.FirstOrDefaultAsync(m => m.ID == id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Attach(GlobalVar).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToPage("ManageSystem");
        }
    }
}
