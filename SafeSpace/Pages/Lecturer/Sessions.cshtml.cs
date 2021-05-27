using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SafeSpace.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace SafeSpace.Pages
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
        public IList<Models.Lecturer> Lecturers { get; set; }
        public IList<Session> UpcomingSessions { get; set; }
        public IList<Session> PastSessions { get; set; }
        public GlobalVariables GlobalVariables { get; set; }
        public string Week { get; set; }

        public async Task OnGetAsync()
        {
            var userEmail = User.Identity.Name;
            var lecturer = await _context.Lecturer.FirstOrDefaultAsync(l => l.Email == userEmail);
            LecturerName = lecturer.Name;
            Courses = await _context.Course.Where(c => !c.isModel).ToListAsync();
            Classrooms = await _context.ClassRoom.ToListAsync();
            Lecturers = await _context.Lecturer.ToListAsync();
            Sessions = await _context.Session.ToListAsync();
            LecturerSessions = Sessions.Where(s => s.Course.Lecturer.Email == userEmail).ToList();
            UpcomingSessions = LecturerSessions.Where(s => s.Time > DateTime.Now).ToList();
            UpcomingSessions = UpcomingSessions.OrderBy(s => s.Time).ToList();
            PastSessions = LecturerSessions.Where(s => s.Time <= DateTime.Now).ToList();
            PastSessions = PastSessions.OrderBy(s => s.Time).ToList();
            GlobalVariables = _context.GlobalVariables.First();
            DayOfWeek day = DateTime.Now.DayOfWeek;
            int days = day - DayOfWeek.Monday;
            DateTime start = DateTime.Now.AddDays(-days);
            Week = start.ToString("dd.MM") + "-" + start.AddDays(6).ToString("dd.MM");
        }
    }
}
