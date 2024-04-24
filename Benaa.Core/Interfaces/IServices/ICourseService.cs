using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using ErrorOr;

namespace Benaa.Core.Interfaces.IServices
{
    public interface ICourseService
    {
        Task<ErrorOr<Course>> CreateCourse(CourseDto newCourse);
        Task<ErrorOr<Guid>> CreateCourseOnly(CreateCourseDto newCourse, string teacherId);
        Task Delete(Guid courseId);
        Task<CourseDto> GetById(string courseId); //+rates
        Task<IEnumerable<CourseDto>> GetByType(string courseType);
        Task<IEnumerable<CourseDto>> GetByUserId(string userId);//for teacher and student how did buy the course
        Task AddCourseToCart(string userId, string courseId);
        Task BuyCorse(string userId, string courseId);
        Task GetBestRatedCorse();
        Task AddRate();//the user must own the course
    }
}
