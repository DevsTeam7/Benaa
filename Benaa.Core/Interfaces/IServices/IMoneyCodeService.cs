using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IMoneyCodeService
    {
        Task<object> Generate(int amount, int number);
        
    }
}
