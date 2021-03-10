using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace WebApp1.Pages
{
    public class SessionsModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public SessionsModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        public string LecturerName { get; set; }
        public IList<Course> Courses { get; set; }
        public IList<ClassRoom> Classrooms { get; set; }
        public IList<Session> Sessions { get; set; }
        public IList<Session> LecturerSessions { get; set; }
        public async Task OnGetAsync()
        {
            var userEmail = User.Identity.Name;
            var lecturer = await _context.Lecturer.FirstOrDefaultAsync(l => l.Email == userEmail);
            LecturerName = lecturer.Name;
            Courses = await _context.Course.Where(c => !c.isModel).ToListAsync();
            Classrooms = await _context.ClassRoom.ToListAsync();
            Sessions = await _context.Session.ToListAsync();
            LecturerSessions = Sessions.Where(s => s.Course.Lecturer.Email == userEmail).ToList();
        }
    }
}
