namespace Benaa.Core.Entities.DTOs
{
    public class SchedualDto
    {
        public required string TimeStart { get; set; }
        public required string TimeEnd { get; set; }
        public required string TeacherId { get; set; }
        public DateTime Date {  get; set; }
        public decimal Price { get; set; }
    }
}
