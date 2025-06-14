using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Mikroserwisy.PatientApi.Entities
{
    [Table("Appointments", Schema = "ProjectSzT3")]
    public class PatientAppointment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        public int DoctorExternalId { get; set;}
        [Required]
        public DateTime? DateTime { get; set; }
        public string? DoctorSpecialization { get; set; }

    }
}
