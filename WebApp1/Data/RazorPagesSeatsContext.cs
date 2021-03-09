using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp1.Models;

namespace WebApp1.Data
{
    public class RazorPagesSeatsContext : DbContext
    {
        public RazorPagesSeatsContext(DbContextOptions<RazorPagesSeatsContext> options)
            : base(options)
        {
        }

        public DbSet<WebApp1.Models.Seat> Seat { get; set; }
        public DbSet<WebApp1.Models.User> User { get; set; }
        public DbSet<WebApp1.Models.Course> Course { get; set; }
        public DbSet<WebApp1.Models.ClassRoom> ClassRoom { get; set; }
        public DbSet<WebApp1.Models.Session> Session { get; set; }
        public DbSet<WebApp1.Models.Lecturer> Lecturer { get; set; }
        public DbSet<WebApp1.Models.Admin> Admin { get; set; }
        public DbSet<WebApp1.Models.RegistrationToken> RegistrationToken { get; set; }
    }
}
