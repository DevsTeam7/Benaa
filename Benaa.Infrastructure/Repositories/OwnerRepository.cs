using Benaa.Core.Entities.General;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Benaa.Infrastructure.Utils.Users;
using Benaa.Core.Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Benaa.Core.Interfaces.IRepositories;
using static Benaa.Core.Entities.General.Course;

namespace Benaa.Infrastructure.Repositories
{
    public class OwnerRepository : BaseRepository<User>, IOwnerRepository
    {
        private readonly UserManager<User> _userManager;
        public OwnerRepository(ApplicationDbContext dbContext, UserManager<User> userManager) : base(dbContext)
        {
            _userManager = userManager;
        }
		public async Task<List<User>> GetNT()
		{
			var usersInTeacherRole = await _userManager.GetUsersInRoleAsync(Role.Teacher);
			var filteredUsers = _dbContext.Users
				.Where(user => usersInTeacherRole.Contains(user) && user.IsApproved == false)
				.Include(user => user.BankInformation)
				.ToList();
			return filteredUsers;
		}


		public async Task<List<User>> GetAD()
        {
            var usersInAdminRole = await _userManager.GetUsersInRoleAsync(Role.Admin);
            return usersInAdminRole.ToList();
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
            //List<JoinPayment> resultlist = new List<JoinPayment>();
            //var result = await _dbContext.Users.Join(_dbContext.Payments, user => user.Id, payment => payment.TeacherId, (user, payment) => new { user, payment }).Join(_dbContext.BankInformations, u => u.user.BankInformationId, bank => bank.Id, (u, bank) => new JoinPayment { UserName = u.user.UserName, Amount = u.payment.Amount, Status = u.payment.Status, Id = u.payment.Id, BankName = bank.BankName, AccontNumber = bank.Account_Number }).ToListAsync();
            //resultlist.AddRange(result);
            //return resultlist;
            List<JoinPayment> resultlist = new List<JoinPayment>();
            var result = await _dbContext.Users.Join(_dbContext.Payments, user => user.Id, payment => payment.TeacherId, (user, payment) => new { user, payment }).Where(up=>up.payment.Status==0||up.payment.Status==1).Join(_dbContext.BankInformations, u => u.user.BankInformationId, bank => bank.Id, (u, bank) => new JoinPayment { UserName = u.user.UserName, Amount = u.payment.Amount, Status = u.payment.Status, Id = u.payment.Id, BankName = bank.BankName, AccontNumber = bank.Account_Number }).ToListAsync();
            resultlist.AddRange(result);
            return resultlist;
        }

        public async Task<List<JoinPayment>> GetP()
        {
            await Status();
           
            List<JoinPayment> resultlist = new List<JoinPayment>();
            var result = await _dbContext.Users.Join(_dbContext.Payments, user => user.Id, payment => payment.TeacherId, (user, payment) => new { user, payment }).Where(up => up.payment.Status == 2).Join(_dbContext.BankInformations, u => u.user.BankInformationId, bank => bank.Id, (u, bank) => new JoinPayment { UserName = u.user.UserName, Amount = u.payment.Amount, Status = u.payment.Status, Id = u.payment.Id, BankName = bank.BankName, AccontNumber = bank.Account_Number }).ToListAsync();
            resultlist.AddRange(result);
            return resultlist;
        }


        public async Task<IncomsInfo> GetINFO()
        {
            IncomsInfo incomsInfo = new IncomsInfo();
            int students = _userManager.GetUsersInRoleAsync(Role.Student).Result.Count();
            int teachers = _userManager.GetUsersInRoleAsync(Role.Teacher).Result.Count(); ;
            decimal dues = _dbContext.Payments.Where(u => u.Status == 1).Sum(p => p.Amount);
            decimal AllIncoms = _dbContext.Payments.Sum(u => u.Amount);
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
