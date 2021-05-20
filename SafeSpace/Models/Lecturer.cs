using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SafeSpace.Models
{
    public class Lecturer
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Lecturer")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
