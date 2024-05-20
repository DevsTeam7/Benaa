using Benaa.Core.Entities.General;

namespace Benaa.Core.Entities.DTOs
{
    public class ReportDto
    {
        public required string Description { get; set; }
        public required string Problem { get; set; }
        public required ReportType Type { get; set; }
        public required string TargetId { get; set; }
    }
}
