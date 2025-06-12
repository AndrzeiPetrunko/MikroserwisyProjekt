using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Mikroserwisy.PatientApi.Dtos
{
    public class PatientDto
    {
        public string? FullName { get; set; }
        public string? PESEL { get; set; }
        public string? EmailAddress { get; set; }
    }
}
