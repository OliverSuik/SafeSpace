using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SafeSpace.Models
{
    public class Admin
    {
        public int ID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
