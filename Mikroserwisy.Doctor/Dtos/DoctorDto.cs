using System.ComponentModel.DataAnnotations;

namespace Mikroserwisy.DoctorApi.Dtos
{
    public class DoctorDto
    {
        public string? FullName { get; set; } = string.Empty;
        public string? Specialization { get; set; } = string.Empty;
    }
}
