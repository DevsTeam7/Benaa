using AutoMapper;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;


namespace Benaa.Core.Mapper
{
    public class BaseMapper : Profile
    {
        public BaseMapper()
        {
            CreateMap<User, RegisterRequestDto>().ReverseMap();
            CreateMap<Sceduale, SchedualDetailsDto>().ReverseMap();
            CreateMap<SchedualDto, Sceduale>();
            CreateMap<SchedualDetailsDto, Sceduale>();
            CreateMap<UpdateUserInfo, User>().ReverseMap();
        }
    }
}
