using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Benaa.Infrastructure.Repositories
{
    public class UserCoursesRepository : BaseRepository<UserCourses>, IUserCoursesRepository
    {
        public UserCoursesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async new Task<List<UserCourses>> Select(Expression<Func<UserCourses, bool>> predicate)
        {
            var lsitOfItems =  _dbContext.UserCourses.Where(predicate).Include(cart => cart.Course).ToList();
            return lsitOfItems;
        }
    }
}
