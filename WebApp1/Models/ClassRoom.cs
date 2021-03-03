using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApp1.Models
{
    public class ClassRoom
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public int Number { get; set; }
        public string Coordinates { get; set; }
        public IList<Seat> Seats { get; set; }
        public bool isModel { get; set; }
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
