using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp1.Pages
{
    public class SelectSessionModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public SelectSessionModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        public IList<Course> Courses { get; set; }
        public IList<ClassRoom> Classrooms { get; set; }
        public IList<Session> Sessions { get; set; }
        public IList<Seat> Seats { get; set; }
        public IList<User> Users { get; set; }
        public SelectList SelectSession { get; set; }
        [BindProperty]
        public string Texttest { get; set; }
        [BindProperty]
        public int SelectedSessionId { get; set; }
        public Course Course { get; set; }
        public async Task OnGetAsync()
        {
            Courses = await _context.Course.ToListAsync();
            Classrooms = await _context.ClassRoom.ToListAsync();
            Sessions = await _context.Session.ToListAsync();
            Seats = await _context.Seat.ToListAsync();
            Users = await _context.User.ToListAsync();
            SelectSession = new SelectList(Sessions, nameof(Session.ID), nameof(Session.Name));
        }
        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage("PickSeats", new { id = SelectedSessionId });
        }
    }
}
