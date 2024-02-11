using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Services
{
    public class MoneyCodeService : IMoneyCodeService
    {
        private readonly IMoneyCodeRepository _moneyCodeRepository;
        public MoneyCodeService(IMoneyCodeRepository moneyCodeRepository)
        {
            _moneyCodeRepository = moneyCodeRepository;
        }

       
        public async Task< object> Generate(int amount, int number)
        {
            if(amount==0|| number == 0) 
            {
                return "the amount field or the number field cannot be 0"; 
            }
            
            Random rnd = new Random();
            List<string> list = await _moneyCodeRepository.GetAllCode();
            List<string> newlist = new List<string>();

            int counter = 0;
            while (counter < number)
            {

                string rand = rnd.Next(0, 9).ToString() + rnd.Next(0, 9).ToString() + rnd.Next(0, 9).ToString() + rnd.Next(0, 9).ToString() + rnd.Next(0, 9).ToString() + rnd.Next(0, 9).ToString() + rnd.Next(0, 9).ToString() + rnd.Next(0, 9).ToString() + rnd.Next(0, 9).ToString();
                if (!list.Contains(rand) && !newlist.Contains(rand))
                {
                    MoneyCode mc = new MoneyCode();
                    mc.Amount = amount;
                    mc.Code = rand;
                    await _moneyCodeRepository.Create(mc);
                    counter++;
                    newlist.Add(rand);
                    

                }
            }

           return newlist;

        }




       

    }
}
