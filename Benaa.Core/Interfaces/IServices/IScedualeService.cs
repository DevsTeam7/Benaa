using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IScedualeService
    { 
        Task<IEnumerable<Sceduale>> GetSchedual();
        //Task<Sceduale> AddSchedual(SchedualDto sceduale);
        void AddSchedualList(List<Sceduale> sc);
        Task Appointment(SchedualDetailsDto sc);
        Task<ActionResult<Sceduale>> GetById(Guid id);
        Task UpdateSceduale(SchedualDetailsDto sc);
         Task Delete(Guid id);
    }
}
