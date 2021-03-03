using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp1.Pages
{
    public class CreateClassroomModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public CreateClassroomModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public string Coords { get; set; }

        [BindProperty]
        public ClassRoom Classroom { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Classroom.Coordinates = Coords;
            Coords = Coords.Remove(Coords.Length - 1);
            Classroom.FileName = "test2.svg";
            List<string> list = Coords.Split(',').ToList();
            List<int> xlist = new List<int>();
            List<int> ylist = new List<int>();
            foreach (string co in list)
            {
                if (list.IndexOf(co) % 2 == 0)
                {
                    xlist.Add(Int32.Parse(co.Trim()));
                }
                else
                {
                    ylist.Add(Int32.Parse(co.Trim()));
                }
            }
            List<Seat> SeatList = new List<Seat>();
            for (int i = 0; i < xlist.Count; i++)
            {
                //System.Diagnostics.Debug.WriteLine(seat);
                SeatList.Add(new Seat { X = xlist[i], Y = ylist[i], User = new User { isModel = true } });
            }
            Classroom.Seats = SeatList;
            Classroom.isModel = true;
            _context.ClassRoom.Add(Classroom);
            await _context.SaveChangesAsync();
            return RedirectToPage("Admin/CreateSession");
        }
    }
}
