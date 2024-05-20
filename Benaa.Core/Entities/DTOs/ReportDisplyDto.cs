using Benaa.Core.Entities.General;

namespace Benaa.Core.Entities.DTOs
{
    public class ReportDisplyDto
    {
        public string Description { get; set; }
        public string Problem { get; set; }
        public string TypeContent { get; set; }
        public string Reporter {  get; set; }
        public string ReportedAt { get; set;}
        public ReportType Type { get; set;}
        public string UserId { get; set;}
        public Guid CourseId { get; set;}
        public Guid reportId { get; set;}
    }
}
