using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace applicant_api.Model.Data.Class
{
    public class Quote
    {
        [Key]
        public int Id { get; set; }
        public Guid? GUID { get; set; }
        public int Term { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        public int ApplicantId { get; set; }
        [NotMapped]
        public double PMT { get; set; }
    }
}
