using System;
using CourseLibrary.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourseLibrary.API.DbContexts
{
    public class UserContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public UserContext(DbContextOptions<UserContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    FirstName = "Someone",
                    LastName = "Everyone",
                    Username = "Try",
                    Role = "Teacher",
                    PasswordHash = new byte[10],
                    PasswordSalt = new byte[10]

                });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}
