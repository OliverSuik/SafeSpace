using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Please select time of the session.")]
        [Display(Name = "time")]
        public DateTime DateTime { get; set; } = DateTime.Now;
        public IList<Course> Courses { get; set; }
        public IList<Seat> Seats { get; set; }
        public IList<ClassRoom> Classrooms { get; set; }
        public IList<Models.Lecturer> Lecturers { get; set; }
        [BindProperty]
        public Session Session { get; set; } = new Session();
        public SelectList SelectCourse { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select a course.")]
        public int SelectedCourseId { get; set; }
        public Course selectedCourse { get; set; }
        public SelectList SelectClassroom { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Please select a classroom.")]
        public int SelectedClassroomId { get; set; }
        public ClassRoom selectedClassroom { get; set; }
        public string SessionName { get; set; }
        public async Task OnGetAsync()
        {
            Courses = await _context.Course.ToListAsync();
            Lecturers = await _context.Lecturer.ToListAsync();
            Classrooms = await _context.ClassRoom.Where(s => s.isModel).ToListAsync();
            SelectCourse = new SelectList(Courses, nameof(Course.ID), nameof(Course.Name), null, nameof(Course.LecturerName));
            SelectClassroom = new SelectList(Classrooms, nameof(ClassRoom.ID), nameof(ClassRoom.Name));
            Seats = await _context.Seat.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Seats = await _context.Seat.ToListAsync();
            var selectedCourse = _context.Course.Find(SelectedCourseId);
            ClassRoom selectedClassroom = _context.ClassRoom.Find(SelectedClassroomId);
            var copyClassroom = selectedClassroom.DeepCopy();

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

            SessionName = DateTime.ToString("HH:mm MM.dd") + " " + selectedCourse.Name + " classroom " + newClassroom.Number;
            Session.Course = selectedCourse;
            Session.ClassRoom = newClassroom;
            Session.Time = DateTime;
            Session.Name = SessionName;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Session.Add(Session);
            await _context.SaveChangesAsync();

            return RedirectToPage("Sessions");

        }
    }
}
