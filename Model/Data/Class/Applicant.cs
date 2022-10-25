using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace applicant_api.Model.Data.Class
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }
        public Guid GUID { get; set; }
        [Required]
        public string Salutation { get; set; } = String.Empty;
        [Required]
        public string FirstName { get; set; } = String.Empty;
        [Required]
        public string LastName { get; set; } = String.Empty;
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string Mobile { get; set; } = String.Empty;
        [Required]
        public string Email { get; set; } = String.Empty;
    }
}
