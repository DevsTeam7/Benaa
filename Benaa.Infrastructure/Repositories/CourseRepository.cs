using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Benaa.Core.Entities.General.Course;

namespace Benaa.Infrastructure.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Course>> SelectQuantity(int quantity, CourseType? type = null)
        {
            IQueryable<Course> query = _dbContext.Courses;

            query = query.Take(quantity);
            if (type.HasValue)
            {
                query = query.Where(c => c.Type == type.Value)
                    .Include(course => course.Rates)
                    .Include(course => course.CourseChapters);
            }

            List<Course> courses = await query.ToListAsync();
            return courses;
        }
        public async new Task<Course> SelectOneItem(Expression<Func<Course, bool>> predicate)
        {
            Course course = await _dbContext.Courses.FirstAsync(predicate);
            if(course is null) { throw new Exception(); }
            var retes = await _dbContext.Rates.Where(rate => rate.CourseId == course.Id).ToListAsync();
            var chapters = await _dbContext.CourseChapters.Where(chapter => chapter.CourseId == course.Id).ToListAsync();

            course.Rates = retes;
            course.CourseChapters = chapters;
            return course;
        }
        public async new Task<List<Course>> Select(Expression<Func<Course, bool>> predicate)
        {
            List<Course> course = await _dbContext.Courses
                .Where(predicate)
                .Include(course => course.Rates)
                .Include(course => course.CourseChapters).ToListAsync();
            if (course is null) { throw new Exception(); }

            return course;
        }
    }
}
