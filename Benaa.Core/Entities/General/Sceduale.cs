using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Benaa.Core.Entities.General
{
    public class Sceduale
    {
        private Wallet wallet;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        [DataType(DataType.Time)]
        public TimeSpan TimeStart { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan TimeEnd { get; set; }
        public decimal Price { get; set; }
        [Required]
        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }
        [ForeignKey(nameof(Student))]
        public string? StudentId { get; set; }
        public virtual User? Teacher { get; set; }
        public virtual User? Student { get; set; }

        public void SetWallet(Wallet wallet)
        {
            this.wallet = wallet;
        }
        //public bool SendRequest()
        //{
        //    decimal price = 100;
        //    return wallet.CheckWallet(price, id);
        //}
    }
}
