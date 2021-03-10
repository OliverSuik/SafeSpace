using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class RegistrationToken
    {
        public int ID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public string Role { get; set; } 
        public string Name { get; set; }
        public DateTime GenerateTime { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
