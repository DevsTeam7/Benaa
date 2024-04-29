using AutoMapper;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace Benaa.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly ICourseRepository _courseRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IChapterRepository _chapterRepository;
        private readonly IUserCoursesRepository _userCoursesRepository;
        private readonly IRateRepository _rateRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IWalletService _walletService;

        public CourseService(IFileUploadService fileUploadService, ICourseRepository courseRepository, IMapper mapper
            ,IChapterRepository chapterRepository, ILessonRepository lessonRepository,
            IUserCoursesRepository userCoursesRepository, IRateRepository rateRepository,
            UserManager<User> userManager, IWalletService walletService)
        { _fileUploadService = fileUploadService; _courseRepository = courseRepository; _mapper = mapper;
            _lessonRepository = lessonRepository; _chapterRepository = chapterRepository;
             _userCoursesRepository = userCoursesRepository; _rateRepository = rateRepository;
            _userManager = userManager; _walletService = walletService;
        }

        private async Task<ErrorOr<Guid>> CreateCourseOnly(CreateCourseDto newCourse, string teacherId)
        {
            var videoUrl = await _fileUploadService.UploadFile(newCourse.VideoUrl);
            var imageUrl = await _fileUploadService.UploadFile(newCourse.ImageUrl);
            if (string.IsNullOrEmpty(imageUrl) && string.IsNullOrEmpty(videoUrl))
            { return Error.Failure("فشل رفع الملفات"); }

            Course course = _mapper.Map<Course>(newCourse);
            switch (newCourse.Type)
            {
                case 0:
                    course.Type = Course.CourseType.HighShcool;
                    break;
                case 1:
                    course.Type = Course.CourseType.College;
                    break;
                case 2:
                    course.Type = Course.CourseType.General;
                    break;
            }
            course.ImageUrl = imageUrl;
            course.VideoUrl = videoUrl;
            course.TeacherId = teacherId;
            var result = await _courseRepository.Create(course);
            if (result == null) { Error.Failure("فشل في انشاء الكورس"); }
            return result.Id;

        }
        private async Task<ErrorOr<Guid>> CreateChapterLessons(ChapterLessonsDto chapterLessons, Guid courseId)
        {
            CourseChapter chapter = _mapper.Map<CourseChapter>(chapterLessons.createChapter);
            chapter.CourseId = courseId;
            var createdChapter = await _chapterRepository.Create(chapter);
            if(createdChapter == null) { return Error.Failure(); }
            List<CourseLesson> courseLessons = new List<CourseLesson>();
            foreach (var lesson in chapterLessons.createLessons)
            {
                var filePath = await _fileUploadService.UploadFile(lesson.FileUrl);
                if (string.IsNullOrEmpty(filePath)){ return Error.Failure("فشل رفع الملفات"); }
                CourseLesson courseLesson = _mapper.Map<CourseLesson>(lesson);
                courseLesson.FileUrl = filePath;
                courseLesson.CourseChapterId = createdChapter.Id;
                courseLessons.Add(courseLesson);
             }
            await _lessonRepository.CreateRange(courseLessons);
            return chapter.Id;
        }

        public async Task<ErrorOr<Guid>> CreateCourse(CourseDto newCourse, string teacherId)
        {
            var result = await CreateCourseOnly(newCourse.CreateCourseDto,teacherId);
            if (result.IsError) { return result.ErrorsOrEmptyList; }
            var createdChapterLessons = await CreateChapterLessons(newCourse.ChapterLessons,result.Value);
            if(createdChapterLessons.IsError) { return createdChapterLessons.ErrorsOrEmptyList; }
            return result;
        }
        public async Task<ErrorOr<Guid>> AddCourseToCart(string userId, string courseId)
        {
            UserCourses cart = new UserCourses {
                CourseId = Guid.Parse(courseId),
                StudentId = userId
            };
            var addingCourseToCart = await _userCoursesRepository.Create(cart);
            if(addingCourseToCart == null) { return Error.Failure(); }
            return addingCourseToCart.Id;
        }
        public async Task<ErrorOr<Guid>> AddRate(RateDTO.Request newRate,string studentId)
        {
            UserCourses cart = await _userCoursesRepository
                .SelectOneItem(cart => cart.StudentId == studentId 
                 && cart.CourseId == newRate.CourseId);
            if(cart  == null) { return Error.Failure(); }

            if (cart.IsPurchased is true) { 
                Rate rate = _mapper.Map<Rate>(newRate);
                rate.StudentId = studentId;
                var createdRated = await _rateRepository.Create(rate);
                if (createdRated == null) { return Error.Failure(); }
                return createdRated.Id;
            }
            return Error.Failure();
        }
        public async Task<ErrorOr<List<RateDTO.Response>>> GetAllRate(string courseId)
        {
           List<Rate> rates = await _rateRepository
                .Select(rate => rate.CourseId == Guid.Parse(courseId));
            if(rates == null) { return Error.Failure(); }
            List<RateDTO.Response> ratesResponse = _mapper.Map<List<RateDTO.Response>>(rates);
            int index = 0;
            foreach(var rate in ratesResponse)
            {
                rate.FullName = rates[index].Student!.FirstName + " " + rates[index].Student!.LastName;

            }
            return ratesResponse;
        }
        public async Task<ErrorOr<Guid>> BuyCorse(string userId, string courseId)
        {
            Course course = await _courseRepository.GetById(Guid.Parse(courseId));
            User user = await _userManager.FindByIdAsync(userId);
            if(user == null || course == null) { return Error.Failure(); }

            bool IsBalanceSufficient= await _walletService.Check(user.Id, course.Price);
            if (IsBalanceSufficient)
            {
              var payment = await  _walletService.SetPayment(course.Id, "course", course.Price, user.Id);
                if(payment == null) { return Error.Failure(); }
              var cart = await _userCoursesRepository
                    .SelectOneItem(cart => cart.CourseId == course.Id
                    && cart.StudentId == userId);
                if (cart == null) { return Error.Failure(); }
                cart.IsPurchased = true;
                await _userCoursesRepository.Update(cart);
                return course.Id;
            }
            return Error.Failure();
        }
        public async Task Delete(string courseId)
        {
            Course course = await _courseRepository.GetById(Guid.Parse(courseId));
            await _courseRepository.Delete(course);
        }
        //TODO
        public Task GetBestRatedCorse()
        {
            throw new NotImplementedException();
        }
        public async Task<ErrorOr<Course>> GetById(string courseId)
        {
            var course = await _courseRepository.GetById(Guid.Parse(courseId));
            if(course == null) { return Error.NotFound(); }
            return course;
        }
        public async Task<ErrorOr<List<Course>>> GetByStudentId(string studentId)
        {
            List<Course> courses = new List<Course>();
            List<UserCourses> userCourses = await _userCoursesRepository
                .Select(userCourses => userCourses.StudentId 
                == studentId && userCourses.IsPurchased == true);
            if (userCourses == null) { return Error.Failure(); }

            foreach (var cart in userCourses)
            {
                var course = cart.Course ;
                if(course == null) { return Error.Failure(); }
                courses.Add(course);
            }
            return courses;
        }
        public async Task<ErrorOr<List<Course>>> GetByTeacherId(string teacherId)
        {
          List<Course> courses =  await _courseRepository
                .Select(course => course.TeacherId == teacherId && course.IsPublished == true);
            if(courses == null) { return Error.NotFound(); }
            return courses;

        }
        public async Task<ErrorOr<List<Course>>> GetByType(int courseType)
        {
            Course.CourseType type = Course.CourseType.General;
            switch (courseType)
            {
                case 0:
                    type = Course.CourseType.HighShcool;
                    break;
                case 1:
                     type = Course.CourseType.College;
                    break;
            }
           var courses = await _courseRepository.Select(course => course.Type == type);
            if(courses == null) { return Error.NotFound(); }
            return courses;
        }
        public async Task<ErrorOr<ChapterLessonsDto.Respnse>> GetChapterLessons(string courseId)
        {
            var chapter = await _chapterRepository.SelectOneItem(course => course.CourseId == Guid.Parse(courseId));
            var lessons = await _lessonRepository.Select(lesson => lesson.CourseChapterId == chapter.Id);
            if (chapter == null || lessons.Count == 0) { return Error.NotFound(); }

            ChapterLessonsDto.Respnse respnse = new ChapterLessonsDto.Respnse 
            { courseChapter = chapter, courseLessons=lessons};
            return respnse;
        }

    }
}
