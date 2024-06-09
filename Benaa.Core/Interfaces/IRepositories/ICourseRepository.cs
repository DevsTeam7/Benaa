using Benaa.Core.Entities.General;
using System.Linq.Expressions;
using static Benaa.Core.Entities.General.Course;

namespace Benaa.Core.Interfaces.IRepositories
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<List<Course>> SelectQuantity(int quantity, CourseType? type = null);
        new Task<Course> SelectOneItem(Expression<Func<Course, bool>> predicate);
        new Task<List<Course>> Select(Expression<Func<Course, bool>> predicate);
        new Task<List<Course>> GetAll();

	}
}
