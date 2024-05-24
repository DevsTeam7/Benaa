namespace Benaa.Dashboard.Models
{
    public class JoinPayments
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public string BankName { get; set; }
        public long AccontNumber { get; set; }
    }
}
