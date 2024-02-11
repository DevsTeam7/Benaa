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
        Task<Sceduale> AddSchedual(SchedualDto sceduale);
        Task Appointment(SchedualDetailsDto sc);
        Task<ActionResult<Sceduale>> GetById(int id);
         Task UpdateSceduale(SchedualDetailsDto sc);
         Sceduale Delete(Sceduale movie);
    }
}
