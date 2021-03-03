using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class Session
    {
        public int ID { get; set; }
        public Course Course { get; set; }
        public ClassRoom ClassRoom { get; set; }
        public DateTime Time { get; set; }
        public string Name { get; set; }
    }
}
