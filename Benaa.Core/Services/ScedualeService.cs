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
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Benaa.Core.Entities.DTOs;


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

        public async Task AddSchedualList(List<SchedualDto> sc)
        {          
            foreach (var i in sc)
            {
                DateTime ts = i.TimeStart;
                int Ts = ts.Hour;
                DateTime te = i.TimeEnd;
                int Te = te.Hour;
                var scheduale = new Sceduale
                {
                    Date = i.Date,
                    TimeStart = Ts,
                    TimeEnd = Te,
                    Price = i.Price,
                    TeacherId = i.TeacherId,
                    StudentId = i.StudentId
                };
                await _schedualRepository.Create(scheduale);               
            }
            
        }
 
        public async Task Appointment(SchedualDetailsDto sc)
        {
            //var user = await _schedualRepository.GetById(sc.Id);

            Sceduale model = _mapper.Map<Sceduale>(sc);
            //if (model.StudentId == null)

            //user.TeacherId = sc.TeacherId;
            //user.StudentId = sc.StudentId;
            //user.Date = sc.Date;
            //user.TimeStart = sc.TimeStart;
            //user.TimeEnd = sc.TimeEnd;
            //Sceduale model = _mapper.Map<Sceduale>(sc);
            await _schedualRepository.Update(model);

        }


        public async Task<ActionResult<Sceduale>> GetById(Guid id)
        {
            return await _schedualRepository.GetById(id);
        }
        public async Task Delete(Guid id)
        {
            var model = await _schedualRepository.GetById(id);
            _schedualRepository.Delete(model);

        }

        public async Task UpdateSceduale(SchedualDetailsDto sc)
        {
            var sce = await _schedualRepository.GetById(sc.Id);
            //DateTime ts = sc.TimeStart;
            //int Ts = ts.Hour;
            //DateTime te = sc.TimeEnd;
            //int Te = te.Hour;
            //Sceduale model = _mapper.Map<Sceduale>(sc);

            sce.TeacherId = sc.TeacherId;
            sce.StudentId = sc.StudentId;
            sce.Date = sc.Date;
            sce.TimeStart =  sc.TimeStart;
            sce.TimeEnd = sc.TimeEnd;

            await _schedualRepository.Update(sce);
        }

    }
}
