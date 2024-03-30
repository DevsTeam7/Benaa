﻿using AutoMapper;
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
            CreateMap<SchedualDto, Sceduale>().ReverseMap();
            CreateMap<SchedualDetailsDto, Sceduale>().ReverseMap();
            CreateMap<User, LoginRequestDto.Response>().ReverseMap();
        }
    }
}
