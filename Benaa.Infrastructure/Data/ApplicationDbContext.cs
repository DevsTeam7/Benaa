﻿using Benaa.Core.Entities.General;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Benaa.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<MoneyCode> MoneyCodes { get; set; }
        public DbSet<BankInformation> BankInformations { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseChapter> CourseChapters { get; set; }
        public DbSet<CourseLesson> CourseLessons { get; set; }
        public DbSet<Notifaction> Notifactions { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Sceduale> Sceduales { get; set; }
        public DbSet<UserCourses> UserCourses { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole { Name = "Student" },
                new IdentityRole { Name = "Teacher" },
                new IdentityRole { Name = "Admin" },
                new IdentityRole { Name = "Owner" }
                );
        }
    }
}


