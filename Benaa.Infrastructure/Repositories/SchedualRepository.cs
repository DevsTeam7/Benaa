using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Benaa.Infrastructure.Repositories
{
    public class SchedualRepository : BaseRepository<Sceduale>, ISchedualeRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public SchedualRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TimeRangeDto>> SelectTimes(Expression<Func<Sceduale, bool>> predicate)
        {
            var listOfItems = await _dbContext.Sceduales
                .Where(predicate)
                .Select(sceduales => new { sceduales.TimeStart, sceduales.TimeEnd })
                .ToListAsync();
            return listOfItems.Cast<TimeRangeDto>().ToList();
        }


        public async Task<bool> CheckAvailability(SchedualDetailsDto schedualDetails)
        {
            var Isnull = await _dbContext
                .Sceduales.AnyAsync(sceduale => (sceduale.StudentId == null) 
                &&(sceduale.Date == schedualDetails.Date) 
                && (sceduale.TimeStart == schedualDetails.TimeStart));
            if (!Isnull) { throw new Exception(); }
            return Isnull;
        }


    }
}
