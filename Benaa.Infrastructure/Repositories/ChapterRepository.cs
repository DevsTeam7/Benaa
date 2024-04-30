using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Benaa.Infrastructure.Repositories
{
    public class ChapterRepository : BaseRepository<CourseChapter>, IChapterRepository
    {
        public ChapterRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public new async Task<List<CourseChapter>> Select(Expression<Func<CourseChapter, bool>> predicate)
        {
            var lsitOfItems = await _dbContext.CourseChapters.Where(predicate).Include(chpter => chpter.CourseLessons).ToListAsync();
            return lsitOfItems;
        }
    }
}
