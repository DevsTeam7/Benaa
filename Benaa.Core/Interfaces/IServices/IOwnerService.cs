using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IOwnerService
    {
        public  Task<List<User>> Get();
        public  Task del(string id);
        public  Task ac(string id);
        public Task<List<User>> GetA();
        public Task UpdateUser(UpdateUserInfo nu);
        public Task<List<JoinPayment>> GetDues();
        public Task delpay(Guid id);
        public Task<IncomsInfo> getInfo();
        public Task<List<JoinPayment>> GetPaid();


    }
}
