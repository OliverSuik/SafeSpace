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
    public class CreateCourseModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public CreateCourseModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Course.isModel = true;
            _context.Course.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("CreateSession");
        }
    }
}
