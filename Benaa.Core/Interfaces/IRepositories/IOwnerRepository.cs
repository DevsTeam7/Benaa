using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Interfaces.IRepositories
{
    public interface IOwnerRepository : IBaseRepository<User>
    {
        public  Task<List<User>> GetNT();
        public Task<List<User>> GetAD();
        public Task<List<JoinPayment>> GetD();
        public Task<IncomsInfo> GetINFO();
        public Task Status();
        public  Task<List<JoinPayment>> GetP();
    }
}
