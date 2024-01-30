using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Benaa.Core.Entities.General;
using Microsoft.IdentityModel.Protocols;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Benaa.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>()
                .HasOne(x => x.Teacher)
                .WithMany(x => x.TeacherDues)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<Payment>()
                   .HasOne(x => x.Student)
                   .WithMany(x => x.StudentPayments)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Chat>()
                     .HasOne(x => x.Sender)
                     .WithMany(x => x.SenderChats)
                     .HasForeignKey(x => x.SenderId)
                     .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Chat>()
                       .HasOne(x => x.Receiver)
                       .WithMany(x => x.ReceiverChats)
                       .HasForeignKey(x => x.ReceiverId)
                       .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Sceduale>()
                       .HasOne(x => x.Student)
                       .WithMany(x => x.Appointments)
                       .HasForeignKey(x => x.StudentId)
                       .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Sceduale>()
                       .HasOne(x => x.Teacher)
                       .WithMany(x => x.Sceduales)
                       .HasForeignKey(x => x.TeacherId)
                       .OnDelete(DeleteBehavior.ClientSetNull);  
            

            modelBuilder.Entity<Rate>()
                       .HasOne(x => x.Student)
                       .WithMany(x => x.Rates)
                       .HasForeignKey(x => x.StudentId)
                       .OnDelete(DeleteBehavior.ClientSetNull); 


            modelBuilder.Entity<UserCourses>()
                       .HasOne(x => x.Student)
                       .WithMany(x => x.UserCourses)
                       .HasForeignKey(x => x.StudentId)
                       .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }

}


