using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp1.Pages.Student
{
    public class PickSeatsModel : PageModel
    {
        private readonly Data.RazorPagesSeatsContext _context;
        public PickSeatsModel(Data.RazorPagesSeatsContext context)
        {
            _context = context;
        }
        public IList<Seat> Seat { get; set; }
        public IList<Models.Student> Students { get; set; }
        public IList<Course> Course { get; set; }
        public IList<ClassRoom> Classroom { get; set; }
        public Session Session { get; set; }
        [BindProperty]
        public int? SelectedSeatId { get; set; }
        [BindProperty]
        public int? SessNr { get; set; }
        [BindProperty]
        public string SessionTitle { get; set; }
        public IList<Seat> Seats { get; set; }
        public string Name { get; set; }
        [BindProperty]
        public Seat SelectedSeat { get; set; }
        public Seat ConfirmedSeat { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id, int? seatId)
        {
            if (id == null)
            {
                return NotFound();
            }
            Name = User.Identity.Name;
            Seat = await _context.Seat.ToListAsync();
            Course = await _context.Course.ToListAsync();
            Classroom = await _context.ClassRoom.ToListAsync();
            Students = await _context.Student.ToListAsync();
            Session = await _context.Session.FirstOrDefaultAsync(m => m.ID == id);

            var confirmedSeat = await _context.Seat.FirstOrDefaultAsync(m => m.Student.Name == Name &&
            Session.ClassRoom.Seats.Contains(m));
            if (confirmedSeat != null)
            {
                ConfirmedSeat = confirmedSeat;
            }

            SessNr = id;
            SelectedSeatId = seatId;
            SessionTitle = Session.Course.Name + " " + Session.Time.ToShortDateString() + " " + Session.Time.ToShortTimeString() + " classroom " + Session.ClassRoom.Number;

            Seats = Session.ClassRoom.Seats;
            if (seatId != null)
            {
                SelectedSeat = await _context.Seat.FirstOrDefaultAsync(m => m.ID == SelectedSeatId &&
                               Session.ClassRoom.Seats.Contains(m));
            }

            if (Session == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Seat = await _context.Seat.ToListAsync();
            Course = await _context.Course.ToListAsync();
            Classroom = await _context.ClassRoom.ToListAsync();
            Students = await _context.Student.ToListAsync();
            Session = await _context.Session.FirstOrDefaultAsync(m => m.ID == id);

            if (Session == null)
            {
                return NotFound();
            }

            var confirmedSeat = await _context.Seat.FirstOrDefaultAsync(m => m.Student.Name == User.Identity.Name &&
                                Session.ClassRoom.Seats.Contains(m));
            if (confirmedSeat != null)
            {
                ConfirmedSeat = confirmedSeat;
                ConfirmedSeat.Student.Name = "-";
                ConfirmedSeat.BookingTime = DateTime.MinValue;
                _context.Seat.Attach(ConfirmedSeat).State = EntityState.Modified;
            }
            if (SelectedSeat.ID == -1)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToPage("ConfirmationCancel");
            }
            else
            {
                ConfirmedSeat = await _context.Seat.FirstOrDefaultAsync(m => m.ID == SelectedSeat.ID &&
                                Session.ClassRoom.Seats.Contains(m));
                ConfirmedSeat.Student.Name = User.Identity.Name;
                ConfirmedSeat.BookingTime = DateTime.Now;
                _context.Seat.Attach(ConfirmedSeat).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                if (confirmedSeat != null)
                {
                    return RedirectToPage("ConfirmationChange");
                }
                return RedirectToPage("Confirmation");
            }

        }
    }
}
