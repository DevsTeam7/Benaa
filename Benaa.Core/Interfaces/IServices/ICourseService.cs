using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using ErrorOr;

namespace Benaa.Core.Interfaces.IServices
{
    public interface ICourseService
    {
        Task<ErrorOr<Guid>> CreateCourseOnly(CreateCourseDto newCourse, string teacherId);
        //Task<ErrorOr<Guid>> CreateCourse(CourseDto newCourse, string teacherId);
        Task<ErrorOr<Course>> GetById(string courseId);
        Task<ErrorOr<List<Course>>> GetByType(int courseType);
        Task<ErrorOr<List<Course>>> GetByQuantity(int quantity, int courseType);
        Task<ErrorOr<Guid>> AddCourseToCart(string userId, string courseId);
		Task<ErrorOr<List<Course>>> GetCartContent(string userId);
        Task<ErrorOr<Success>> DeleteCartItem(string userId, string coursesId);
		Task GetBestRatedCorse();
        Task<ErrorOr<Guid>> AddRate(RateDTORequest newRate, string studentId);
        //Task<ErrorOr<List<RateDTO.Response>>> GetAllRate(string courseId);
        Task<ErrorOr<List<Course>>> GetByStudentId(string studentId);
        Task<ErrorOr<List<Course>>> GetByTeacherId(string teacherId);
        //Task<ErrorOr<List<CourseChapter>>> GetChapterLessons(string courseId);
        Task<ErrorOr<Guid>> CreateChapterLessons(ChapterLessonsDto chapterLessons, string courseId);
        Task<ErrorOr<Success>> BuyCourse(string userId, List<string> courses);
        Task<ErrorOr<Success>> ReturnTheCourse(string courseId, string studentId);
        Task<ErrorOr<Success>> Delete(Guid courseId);
        Task<ErrorOr<List<Course>>> GetAll();

	}
}
