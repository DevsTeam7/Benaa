using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Benaa.Infrastructure.Data;

namespace Benaa.Infrastructure.Services
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=Y0IZ\\MSSQLSERVER1;Database=EFGetStarted;Trusted_Connection=True;Encrypt=False;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }

    }
}
