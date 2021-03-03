using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp1.Pages.Admin
{
    public class CreateSessionModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public CreateSessionModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        [BindProperty]
        public DateTime DateTime { get; set; } = DateTime.Now;
        public IList<Course> Courses { get; set; }
        public IList<Seat> Seats { get; set; }
        public IList<ClassRoom> Classrooms { get; set; }
        [BindProperty]
        public Session Session { get; set; } = new Session();
        public SelectList SelectCourse { get; set; }

        [BindProperty]
        public int SelectedCourseId { get; set; }
        public Course selectedCourse { get; set; }
        public SelectList SelectClassroom { get; set; }
        [BindProperty]
        public int SelectedClassroomId { get; set; }
        public ClassRoom selectedClassroom { get; set; }
        public string SessionName { get; set; }
        public async Task OnGetAsync()
        {
            Courses = await _context.Course.AsNoTracking().ToListAsync();
            Classrooms = await _context.ClassRoom.Where(s => s.isModel).ToListAsync();
            SelectCourse = new SelectList(Courses, nameof(Course.ID), nameof(Course.Name), null, nameof(Course.Lecturer));
            SelectClassroom = new SelectList(Classrooms, nameof(ClassRoom.ID), nameof(ClassRoom.Number));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Seats = await _context.Seat.ToListAsync();
            var selectedCourse = _context.Course.Find(SelectedCourseId);
            ClassRoom selectedClassroom = _context.ClassRoom.Find(SelectedClassroomId);
            var copyClassroom = selectedClassroom.DeepCopy();

            //System.Diagnostics.Debug.WriteLine(selectedClassroom.Seats.Count);

            List<Seat> newSeats = new List<Seat>();
            foreach (Seat seat in selectedClassroom.Seats)
            {
                Seat newSeat = new Seat
                {
                    User = new User(),
                    X = seat.X,
                    Y = seat.Y
                };
                newSeats.Add(newSeat);
            }

            ClassRoom newClassroom = new ClassRoom
            {
                Coordinates = copyClassroom.Coordinates,
                Seats = newSeats,
                Number = copyClassroom.Number,
                FileName = copyClassroom.FileName
            };

            SessionName = selectedCourse.Name + " " + DateTime.ToString() + " classroom " + newClassroom.Number;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Session.Course = selectedCourse;
            Session.ClassRoom = newClassroom;
            Session.Time = DateTime;
            Session.Name = SessionName;

            _context.Session.Add(Session);
            await _context.SaveChangesAsync();

            return RedirectToPage("Sessions");

        }
    }
}
