using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;

namespace Benaa.Infrastructure.Repositories
{
    public class ChapterRepository : BaseRepository<CourseChapter>, IChapterRepository
    {
        public ChapterRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
