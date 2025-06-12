using Microsoft.EntityFrameworkCore;
using Mikroserwisy.Doctor.Dtos;

namespace Mikroserwisy.Doctor.Services
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
            _context.Doctors.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteById(int id)
        {
            var entity = await _context.Doctors.FirstOrDefaultAsync(p => p.Id == id);
            if (entity != null)
            {
                _context.Doctors.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Update(int id, DoctorDto dto)
        {
            int affectedRows = await _context.Doctors
            .Where(d => d.Id == id)
            .ExecuteUpdateAsync(setters => setters
            .SetProperty(d => d.FullName, dto.FullName)
            .SetProperty(d => d.Specialization, dto.Specialization)
            );
            if (affectedRows == 0)
            {
                throw new KeyNotFoundException($"Doctor with ID {id} not found.");
            }
        }
    }
}
