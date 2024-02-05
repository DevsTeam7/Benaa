using System.Threading.Tasks;
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
        }
    }
}
