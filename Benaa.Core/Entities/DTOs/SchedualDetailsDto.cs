namespace Benaa.Core.Entities.DTOs
{
    public class SchedualDetailsDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public decimal Price { get; set; }
        public  string TeacherId { get; set; }
    }
}
