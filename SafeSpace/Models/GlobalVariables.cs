using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SafeSpace.Models
{
    public class GlobalVariables
    {
        public int ID { get; set; }
        public string EmailSuffix { get; set; }
        public int TokenExpirationDays { get; set; }
        public int SessionDuration { get; set; }
        public int SessionExpirationDays { get; set; }
    }
}
