using Benaa.Core.Entities.General;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Benaa.Infrastructure.Utils.Users;

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
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseChapter> CourseChapters { get; set; }
        public DbSet<CourseLesson> CourseLessons { get; set; }
        public DbSet<Notification> Notifactions { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Sceduale> Sceduales { get; set; }
        public DbSet<UserCourses> UserCourses { get; set; }
        public DbSet<OTPCodes> OTPCodes { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole { Name = Role.Student, NormalizedName = Role.Student},
                new IdentityRole { Name = Role.Teacher, NormalizedName =Role.Teacher },
                new IdentityRole { Name = Role.Admin, NormalizedName = Role.Admin },
                new IdentityRole { Name = Role.Owner, NormalizedName = Role.Owner }
                );
        }
    }
}


