using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Problem { get; set; }
        public ReportType Type { get; set; }
        public Guid TargetId { get; set; }
        public string UserId { get; set; }
        public virtual User? User { get; set; }
    }

    public enum ReportType
    {
        Content = 0,
        Chat = 1
    }
}
