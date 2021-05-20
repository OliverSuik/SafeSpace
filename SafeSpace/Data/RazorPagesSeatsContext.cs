using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SafeSpace.Models;

namespace SafeSpace.Data
{
    public class RazorPagesSeatsContext : DbContext
    {
        public RazorPagesSeatsContext(DbContextOptions<RazorPagesSeatsContext> options) : base(options)
        {
        }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<ClassRoom> ClassRoom { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Lecturer> Lecturer { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<RegistrationToken> RegistrationToken { get; set; }
        public DbSet<GlobalVariables> GlobalVariables { get; set; }
        public DbSet<Cases> Cases { get; set; }
    }
}
