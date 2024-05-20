using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using ErrorOr;


namespace Benaa.Core.Services
{
    public class MoneyCodeService : IMoneyCodeService
    {
        private readonly IMoneyCodeRepository _moneyCodeRepository;
        public MoneyCodeService(IMoneyCodeRepository moneyCodeRepository)
        {
            _moneyCodeRepository = moneyCodeRepository;
        }
       
        public async Task<ErrorOr<List<MoneyCode>>> Generate(int amount, int number)
        {
            List<MoneyCode> codes = new List<MoneyCode>();
            if(amount==0|| number == 0) 
            {
                return Error.Failure(description:"the amount field or the number field cannot be 0"); 
            }
            
            Random rnd = new Random();
            List<string> list = await _moneyCodeRepository.GetAllCode();
            List<string> newlist = new List<string>();
            string rand = "";

            int counter = 0;
            while (counter < number)
            {
                rand = "";
                for(int i=0;i<9;i++)    
                {
                     rand = rand+ rnd.Next(0, 9).ToString();
                }
                if (!list.Contains(rand) && !newlist.Contains(rand))
                {
                    MoneyCode mc = new MoneyCode();
                    mc.Amount = amount;
                    mc.Code = rand;
                   var createdCodes = await _moneyCodeRepository.Create(mc);
                    if (createdCodes == null) { throw new Exception(); }
                    codes.Add(createdCodes);
                    counter++;
                    newlist.Add(amount.ToString());
                    newlist.Add(rand);
                }
            }
           return codes;
        }
    }
}
