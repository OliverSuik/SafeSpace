using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class ClassRoom
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please select a file.")]
        public string FileName { get; set; }
        [Required(ErrorMessage = "Please insert the classroom number.")]
        public int Number { get; set; }
        public string Coordinates { get; set; }
        public IList<Seat> Seats { get; set; }
        public bool isModel { get; set; }
        public string Name { get; set; }
    }
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var serialized = JsonConvert.SerializeObject(self);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
