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
			CreateMap<User, AdminRegesterDTO>().ReverseMap();
			CreateMap<User, TeacherRegisterDto>().ReverseMap();
            CreateMap<Sceduale, SchedualDetailsDto>().ReverseMap();
            CreateMap<SchedualDto, Sceduale>().ReverseMap();
            CreateMap<UpdateUserInfo, User>().ReverseMap();
            CreateMap<User, LoginResponseDto>().ReverseMap();
            CreateMap<Course, CreateCourseDto>().ReverseMap();
            CreateMap<CourseChapter, CreateChapterDto>().ReverseMap();
            CreateMap<CourseLesson, CreateLessonDto>().ReverseMap();
            CreateMap<Rate, RateDTORequest>().ReverseMap();
            //CreateMap<Rate, RateDTO.Response>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap().ForAllMembers(opts => 
            opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<BankInformation, CreateBankInfoDto>().ReverseMap();
            CreateMap<Report,ReportDto>().ReverseMap();
            CreateMap<ReportDisplyDto, Report>().ReverseMap();
        }
    }
}
