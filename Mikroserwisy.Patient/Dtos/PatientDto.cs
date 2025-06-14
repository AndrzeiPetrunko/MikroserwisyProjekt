using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Mikroserwisy.PatientApi.Dtos
{
    public class PatientDto
    {
        public string? FullName { get; set; }
        public string? PESEL { get; set; }
        public string? EmailAddress { get; set; }

        public List<AppointmentDto> Appointments { get; set; } = new();
    }

    public class AppointmentDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorExternalId { get; set; }
        public DateTime? DateTime { get; set; }
        public string? DoctorSpecialization { get; set; }
    }
}
