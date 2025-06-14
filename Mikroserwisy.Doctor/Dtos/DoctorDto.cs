using System.ComponentModel.DataAnnotations;

namespace Mikroserwisy.DoctorApi.Dtos
{
    public class DoctorDto
    {
        public string? FullName { get; set; } = string.Empty;
        public string? DoctorSpecialization { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}
