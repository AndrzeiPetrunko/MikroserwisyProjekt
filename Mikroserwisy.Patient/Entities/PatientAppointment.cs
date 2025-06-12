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

        [ForeignKey(nameof(Doctor))]
        public int DoctorId { get; set;}
        [Required]
        public DateTime? DateTime { get; set; }

    }
}
