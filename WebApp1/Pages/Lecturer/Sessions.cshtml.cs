using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp1.Pages
{
    public class SessionsModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public SessionsModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string LecturerName { get; set; } = "Mart Smith";
        public IList<Course> Course { get; set; }
        public IList<ClassRoom> Classroom { get; set; }
        public IList<Session> Session { get; set; }
        public IList<Session> LecturerSession { get; set; }
        public async Task OnGetAsync()
        {
            Course = await _context.Course.ToListAsync();
            Classroom = await _context.ClassRoom.ToListAsync();
            Session = await _context.Session.ToListAsync();
            LecturerSession = Session.Where(s => s.Course.Lecturer == LecturerName).ToList();
        }
    }
}
