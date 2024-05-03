
namespace Benaa.Core.Entities.DTOs
{
    public class CreateBankInfoDto
    {
        public required string FullName { get; set; }
        public required string BankName { get; set; }
        public required long Account_Number { get; set; }
    }
}
