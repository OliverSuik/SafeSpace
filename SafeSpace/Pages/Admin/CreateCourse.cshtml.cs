using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeSpace.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SafeSpace.Pages.Admin
{
    public class CreateCourseModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public CreateCourseModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; }
        public IList<Models.Lecturer> Lecturers { get; set; }
        public SelectList SelectLecturer { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Please select a lecturer.")]
        public int SelectedLecturerId { get; set; }
        public async Task OnGetAsync()
        {
            Lecturers = await _context.Lecturer.ToListAsync();
            SelectLecturer = new SelectList(Lecturers, nameof(Models.Lecturer.ID), nameof(Models.Lecturer.Name));
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Lecturers = await _context.Lecturer.ToListAsync();
            var selectedLecturer = _context.Lecturer.Find(SelectedLecturerId);
            Course.Lecturer = selectedLecturer;
            Course.LecturerName = selectedLecturer.Name;
            Course.isModel = true;
            _context.Course.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("CreateSession");
        }
    }
}
