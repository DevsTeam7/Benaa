using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Problem { get; set; }
        [Required]
        public ReportType Type { get; set; }
        [Required]
        public Guid TargetId { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual User? User { get; set; }
    }

    public enum ReportType
    {
        Content = 0,
        Chat = 1
    }
}
