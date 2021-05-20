using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SafeSpace.Models
{
    public class Session
    {
        public int ID { get; set; }
        public Course Course { get; set; }
        [Display(Name = "Classroom")]
        public ClassRoom ClassRoom { get; set; }
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public string CourseName { get; set; }
    }
}
