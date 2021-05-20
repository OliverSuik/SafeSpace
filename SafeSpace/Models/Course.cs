using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SafeSpace.Models
{
    public class Course
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }
        public Lecturer Lecturer { get; set; }
        public int NrOfStudents { get; set; }
        public string LecturerName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Course code")]
        public string CourseCode { get; set; }
        public bool isModel { get; set; }
    }
}
