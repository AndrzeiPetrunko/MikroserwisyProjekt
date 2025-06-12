using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mikroserwisy.Patient.Entities
{
    [Table("Patients", Schema = "ProjectSzT3")]
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string PESEL { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}
