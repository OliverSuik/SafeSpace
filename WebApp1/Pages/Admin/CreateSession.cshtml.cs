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
        public SelectList SelectClassroom { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Please select a classroom.")]
        public int SelectedClassroomId { get; set; }
        public string SessionName { get; set; }
        public async Task OnGetAsync()
        {
            Courses = await _context.Course.Where(s => s.isModel).ToListAsync();
            Classrooms = await _context.ClassRoom.Where(s => s.isModel).ToListAsync();
            SelectCourse = new SelectList(Courses, nameof(Course.ID), nameof(Course.Name), null, nameof(Course.LecturerName));
            SelectClassroom = new SelectList(Classrooms, nameof(ClassRoom.ID), nameof(ClassRoom.Name));
            Lecturers = await _context.Lecturer.ToListAsync();
            Seats = await _context.Seat.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Seats = await _context.Seat.ToListAsync();
            Lecturers = await _context.Lecturer.ToListAsync();

            Course selectedCourse = _context.Course.Find(SelectedCourseId);
            ClassRoom selectedClassroom = _context.ClassRoom.Find(SelectedClassroomId);
            var copyClassroom = selectedClassroom.DeepCopy();
            var copyCourse = selectedCourse.DeepCopy();
            List<Seat> newSeats = new List<Seat>();
            foreach (Seat seat in selectedClassroom.Seats)
            {
                Seat newSeat = new Seat
                {
                    Student = new Models.Student(),
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

            Course newCourse = new Course
            {
                Name = copyCourse.Name,
                Lecturer = selectedCourse.Lecturer,
                NrOfStudents = copyCourse.NrOfStudents,
                LecturerName = copyCourse.LecturerName
            };

            SessionName = DateTime.ToString("HH:mm dd.MM") + " " + newCourse.Name + " classroom " + newClassroom.Number;
            Session.Course = newCourse;
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
