using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Entities.DTOs
{
    public class JoinPayment
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public string BankName { get; set; }
        public long AccontNumber { get; set; }
    }
}
