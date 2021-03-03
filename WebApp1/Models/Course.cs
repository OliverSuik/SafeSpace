using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Lecturer { get; set; }
        public int NrOfStudents { get; set; }
        public bool isModel { get; set; }
    }
}
