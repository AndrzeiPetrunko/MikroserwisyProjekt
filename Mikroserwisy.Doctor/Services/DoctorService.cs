using Microsoft.EntityFrameworkCore;
using Mikroserwisy.DoctorApi;
using Mikroserwisy.DoctorApi.Dtos;

namespace Mikroserwisy.DoctorApi.Services
{
    public class DoctorService
    {
        private readonly DoctorContextDb _context;

        public DoctorService(DoctorContextDb context)
        {
            _context = context;
        }

        public async Task<Entities.Doctor?> GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            return await _context.Doctors
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Entities.Doctor?>> Get()
        {
            return await _context.Doctors
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Add(Entities.Doctor entity)
        {
            ValidateDoctorEntity(entity);

            _context.Doctors.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            var entity = await _context.Doctors.FirstOrDefaultAsync(p => p.Id == id);
            if (entity != null)
            {
                _context.Doctors.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Doctor with ID {id} not found.");
            }
        }

        public async Task Update(int id, DoctorDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            ValidateDoctorDto(dto);

            int affectedRows = await _context.Doctors
                .Where(d => d.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(d => d.FullName, dto.FullName)
                    .SetProperty(d => d.DoctorSpecialization, dto.DoctorSpecialization)
                );

            if (affectedRows == 0)
            {
                throw new KeyNotFoundException($"Doctor with ID {id} not found.");
            }
        }

        private void ValidateDoctorDto(DoctorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new ArgumentException("Full name is required.");

            if (string.IsNullOrWhiteSpace(dto.DoctorSpecialization))
                throw new ArgumentException("Specialization is required.");
        }

        private void ValidateDoctorEntity(Entities.Doctor entity)
        {
            if (string.IsNullOrWhiteSpace(entity.FullName))
                throw new ArgumentException("Full name is required.");

            if (string.IsNullOrWhiteSpace(entity.DoctorSpecialization))
                throw new ArgumentException("Specialization is required.");
        }
    }
}
