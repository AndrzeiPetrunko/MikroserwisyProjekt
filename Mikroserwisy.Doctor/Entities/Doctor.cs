using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mikroserwisy.DoctorApi.Entities
{
    [Table("Doctor", Schema = "ProjectSzT3")]
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? DoctorSpecialization { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
