using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Mikroserwisy.Patient;
using Mikroserwisy.Patient.Dtos;
using Mikroserwisy.Patient.Entities;
namespace Mikroserwisy.Patient.Services
{
    public class PatientService
    {
        private readonly PatientContextDb _context;
        public PatientService(PatientContextDb context)
        {
            _context = context;
        }
        public async Task<Entities.Patient?> GetById(int id)
        {
            return await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<IEnumerable<Entities.Patient?>> Get()
        {
            return await _context.Patients
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task Add(Entities.Patient entity)
        {
            _context.Patients.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteById(int id)
        {
            var entity = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (entity != null)
            {
                _context.Patients.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Update(int id, PatientDto dto)
        {
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
    }
}
