using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Benaa.Core.Entities.General;
using Microsoft.IdentityModel.Protocols;


namespace Benaa.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<MoneyCode> MoneyCode { get; set; }

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
        }

    }
}
