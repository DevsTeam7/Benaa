using AutoMapper;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;


namespace Benaa.Core.Mapper
{
    public class BaseMapper : Profile
    {
        public BaseMapper()
        {
            CreateMap<User, StudentRegisterDto>().ReverseMap();
            CreateMap<User, TeacherRegisterDto>().ReverseMap();
            CreateMap<Sceduale, SchedualDetailsDto>().ReverseMap();
            CreateMap<SchedualDto, Sceduale>();
            CreateMap<SchedualDetailsDto, Sceduale>();
            CreateMap<UpdateUserInfo, User>().ReverseMap();
            CreateMap<SchedualDto, Sceduale>().ReverseMap();
            CreateMap<SchedualDetailsDto, Sceduale>().ReverseMap();
            CreateMap<User, LoginRequestDto.Response>().ReverseMap();
            CreateMap<Course, CreateCourseDto>().ReverseMap();
            CreateMap<CourseChapter, CreateChapterDto>().ReverseMap();
            CreateMap<CourseLesson, CreateLessonDto>().ReverseMap();
            CreateMap<Rate, RateDTO.Request>().ReverseMap();
            CreateMap<Rate, RateDTO.Response>().ReverseMap();
        }
    }
}
