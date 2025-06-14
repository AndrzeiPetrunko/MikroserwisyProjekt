using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Mikroserwisy.PatientApi;
using Mikroserwisy.PatientApi.Dtos;
using Mikroserwisy.PatientApi.Resolvers;
namespace Mikroserwisy.PatientApi.Services
{
    public class PatientService
    {
        private readonly PatientContextDb _context;

        public PatientService(PatientContextDb context)
        {
            _context = context;
        }

        public async Task<PatientDto?> GetById(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null) return null;

            return new PatientDto
            {
                FullName = patient.FullName,
                PESEL = patient.PESEL,
                EmailAddress = patient.EmailAddress,
                Appointments = patient.Appointments.Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    PatientId = a.PatientId,
                    DoctorExternalId = a.DoctorExternalId,
                    DateTime = a.DateTime,
                    DoctorSpecialization = a.DoctorSpecialization
                }).ToList()
            };
        }

        public async Task<IEnumerable<Entities.Patient?>> Get()
        {
            return await _context.Patients
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Add(Entities.Patient entity)
        {
            ValidatePatientEntity(entity);

            var resolver = new DoctorResolver();

            foreach (var doctor in entity.Appointments)
            {
                try
                {
                    doctor.DoctorSpecialization = await resolver.ResolverFor(doctor.DoctorExternalId);
                }
                catch (InvalidOperationException ex)
                {
                    throw new ArgumentException(ex.Message);
                }

                _context.Patients.Add(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            var entity = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (entity != null)
            {
                _context.Patients.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Patient with ID {id} not found.");
            }
        }

        public async Task Update(int id, PatientDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            ValidatePatientDto(dto);

            int affectedRows = await _context.Patients
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.FullName, dto.FullName)
                    .SetProperty(p => p.PESEL, dto.PESEL)
                    .SetProperty(p => p.EmailAddress, dto.EmailAddress)
                );

            if (affectedRows == 0)
            {
                throw new KeyNotFoundException($"Patient with ID {id} not found.");
            }
        }

        private void ValidatePatientDto(PatientDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new ArgumentException("Full name is required.");

            if (string.IsNullOrWhiteSpace(dto.PESEL) || dto.PESEL.Length != 11 || !dto.PESEL.All(char.IsDigit))
                throw new ArgumentException("PESEL must be an 11-digit number.");

            if (string.IsNullOrWhiteSpace(dto.EmailAddress) || !dto.EmailAddress.Contains("@"))
                throw new ArgumentException("A valid email address is required.");
        }

        private void ValidatePatientEntity(Entities.Patient entity)
        {
            if (string.IsNullOrWhiteSpace(entity.FullName))
                throw new ArgumentException("Full name is required.");

            if (string.IsNullOrWhiteSpace(entity.PESEL) || entity.PESEL.Length != 11 || !entity.PESEL.All(char.IsDigit))
                throw new ArgumentException("PESEL must be an 11-digit number.");

            if (string.IsNullOrWhiteSpace(entity.EmailAddress) || !entity.EmailAddress.Contains("@"))
                throw new ArgumentException("A valid email address is required.");
        }
    }
}
