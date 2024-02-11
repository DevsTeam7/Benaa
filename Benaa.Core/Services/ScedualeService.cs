using Benaa.Core.Entities.General;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualBasic;
using AutoMapper;

namespace Benaa.Core.Services
{
    public class ScedualeService : IScedualeService
    {
        private readonly ISchedualRepository _schedualRepository;
        private readonly IMapper _mapper;


        public ScedualeService(ISchedualRepository SchedualRepository, IMapper mapper)
        {
            _schedualRepository = SchedualRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Sceduale>> GetSchedual()
        {
            return await _schedualRepository.GetAll();
        }

        public async Task<Sceduale> AddSchedual(SchedualDto sc)
        {
            var sceduale = _mapper.Map<Sceduale>(sc);

            await _schedualRepository.Create(sceduale);           

            return sceduale;
        }

        public async Task Appointment(SchedualDetailsDto sc)
        {
            var user = await _schedualRepository.GetById(sc.Id);

            if (user.Date == sc.Date && user.TimeStart == sc.TimeStart && user.TimeEnd == sc.TimeEnd && user.StudentId == null)
            
                user.TeacherId = sc.TeacherId;
                user.StudentId = sc.StudentId;
                user.Date = sc.Date;
                user.TimeStart = sc.TimeStart;
                user.TimeEnd = sc.TimeEnd;
            //Sceduale model = _mapper.Map<Sceduale>(sc);
               await _schedualRepository.Update(user);
            
        }


        public async Task<ActionResult<Sceduale>> GetById(int id)
        {
            return await _schedualRepository.GetById(id);
        }
        public Sceduale Delete(Sceduale sceduale)
        {
            _schedualRepository.Delete(sceduale);
  
            return sceduale;
        }

        public async Task UpdateSceduale(SchedualDetailsDto sc)
        {
            //var sce = await _schedualRepository.GetById(sc.Id);
            
            Sceduale model = _mapper.Map<Sceduale>(sc);

            //sce.TeacherId = sc.TeacherId;
            //sce.StudentId= sc.StudentId;
            //sce.Date = sc.Date;
            //sce.TimeStart = sc.TimeStart;
            //sce.TimeEnd = sc.TimeEnd;

            await _schedualRepository.Update(model);
        }

 
    }
}
