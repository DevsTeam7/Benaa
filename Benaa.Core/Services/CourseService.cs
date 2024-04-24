using AutoMapper;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using ErrorOr;

namespace Benaa.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public CourseService(IFileUploadService fileUploadService, ICourseRepository courseRepository, IMapper mapper)
        { _fileUploadService = fileUploadService; _courseRepository = courseRepository; _mapper = mapper; }

        public async Task<ErrorOr<Guid>> CreateCourseOnly(CreateCourseDto newCourse, string teacherId)
        {
            var videoUrl = await _fileUploadService.UploadFile(newCourse.VideoUrl);
            var imageUrl = await _fileUploadService.UploadFile(newCourse.ImageUrl);
            if (string.IsNullOrEmpty(imageUrl) && string.IsNullOrEmpty(videoUrl))
            { return Error.Failure("فشل رفع الملفات"); }
            Course course = _mapper.Map<Course>(newCourse);
            course.ImageUrl = imageUrl;
            course.VideoUrl = videoUrl;
            course.TeacherId = teacherId;
            var result = await _courseRepository.Create(course);
            if (result == null) { Error.Failure("فشل في انشاء الكورس"); }
            return result.Id;

        }
        private async Task CreateChapterLessons(ChapterLessonsDto chapterLessons)
        {

        }

        public async Task<ErrorOr<Course>> CreateCourse(CourseDto newCourse, string teacherId)
        {
            newCourse.CreateCourseDto.TeacherId = teacherId;
           // var result = await CreateCourseOnly(newCourse.CreateCourseDto);
            //if (result.IsError) { return result.ErrorsOrEmptyList; }

            throw new NotImplementedException();
        }

        public Task AddCourseToCart(string userId, string courseId)
        {
            throw new NotImplementedException();
        }

        public Task AddRate()
        {
            throw new NotImplementedException();
        }

        public Task BuyCorse(string userId, string courseId)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public Task GetBestRatedCorse()
        {
            throw new NotImplementedException();
        }

        public Task<CourseDto> GetById(string courseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CourseDto>> GetByType(string courseType)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CourseDto>> GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<Course>> CreateCourse(CourseDto newCourse)
        {
            throw new NotImplementedException();
        }
    }
}
