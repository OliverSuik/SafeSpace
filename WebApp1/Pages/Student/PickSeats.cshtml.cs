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
        public IList<User> Users { get; set; }
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
            Users = await _context.User.ToListAsync();

            var confirmedSeat = await _context.Seat.FirstOrDefaultAsync(m => m.User.Name == Name);
            if (confirmedSeat != null)
            {
                ConfirmedSeat = confirmedSeat;
            }

            Session = await _context.Session.FirstOrDefaultAsync(m => m.ID == id);
            SessNr = id;
            SelectedSeatId = seatId;
            SessionTitle = Session.Course.Name + " " + Session.Time.ToShortDateString() + " " + Session.Time.ToShortTimeString() + " classroom " + Session.ClassRoom.Number;

            Seats = Session.ClassRoom.Seats;
            if (seatId != null)
            {
                SelectedSeat = await _context.Seat.FirstOrDefaultAsync(m => m.ID == SelectedSeatId);
            }

            if (Session == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Seat = await _context.Seat.ToListAsync();
            Course = await _context.Course.ToListAsync();
            Classroom = await _context.ClassRoom.ToListAsync();
            Users = await _context.User.ToListAsync();

            var confirmedSeat = await _context.Seat.FirstOrDefaultAsync(m => m.User.Name == User.Identity.Name);
            if (confirmedSeat != null)
            {
                ConfirmedSeat = confirmedSeat;
                ConfirmedSeat.User.Name = "-";
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
                ConfirmedSeat = await _context.Seat.FirstOrDefaultAsync(m => m.ID == SelectedSeat.ID);
                ConfirmedSeat.User.Name = User.Identity.Name;
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
