using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;



namespace Benaa.Infrastructure.Repositories
{
    public class OwnerRepository : BaseRepository<User>, IOwnerRepository
    {
        public OwnerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task <List<User>> GetNT()
        {
           List<User> filteruser=await _dbContext.Users.Where(u=>u.IsApproved==false&&u.Role== "Teacher").ToListAsync();
            return filteruser;
        }

        public async Task<List<User>> GetAD()
        {
            List<User> filteruser = await _dbContext.Users.Where(u => u.Role == "Admin").ToListAsync();   
            return filteruser;
        }



        public async Task Status()
        {
            var holddate = DateTime.Now.AddDays(-3);
            DateTimeOffset holdtime = new DateTimeOffset(holddate).ToOffset(TimeSpan.Zero);
            var paymentToApdate = await _dbContext.Payments.Where(p => p.Status == 0 && p.CreatedAt <= holdtime).ToListAsync();
            foreach (var item in paymentToApdate)
            {
                item.Status = 1;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<JoinPayment>> GetD()
        {
            await Status();
            List<JoinPayment> resultlist = new List<JoinPayment>();
            var result = await _dbContext.Users.Join(_dbContext.Payments, user => user.Id, payment => payment.TeacherId, (user, payment) => new { user, payment }).Join(_dbContext.BankInformations, u => u.user.BankInformationId, bank => bank.Id, (u, bank) => new JoinPayment { UserName = u.user.UserName, Amount = u.payment.Amount, Status = u.payment.Status, Id = u.payment.Id, BankName = bank.BankName, AccontNumber = bank.Account_Number }).ToListAsync();
            resultlist.AddRange(result);
            return resultlist;
        }




        /////////////////////////////////////////////
        ///

        public async Task<IncomsInfo> GetINFO()
        {
            IncomsInfo incomsInfo= new IncomsInfo();
            int students =  _dbContext.Users.Count(u => u.Role == "Student");
            int teachers = _dbContext.Users.Count(u => u.Role == "Techre");
            decimal dues =  _dbContext.Payments.Where(u => u.Status == 1).Sum(p=>p.Amount);
            decimal AllIncoms =  _dbContext.Payments.Sum(u => u.Amount);
            double a = 0.2;
            decimal Profits = AllIncoms * Convert.ToDecimal(a);

            incomsInfo.teachers = teachers;
            incomsInfo.students = students;
            incomsInfo.dues = dues;
            incomsInfo.Profits = Profits;
            incomsInfo.AllIncoms = AllIncoms;


            return incomsInfo;
        }









    }
}
