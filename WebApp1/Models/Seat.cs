using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class Seat
    {
        public int ID { get; set; }
        public User User { get; set; }

        [Display(Name = "Booking time")]
        public DateTime BookingTime { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
