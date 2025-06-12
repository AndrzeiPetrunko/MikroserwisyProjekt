using Microsoft.AspNetCore.Mvc;
using Mikroserwisy.DoctorApi.Dtos;
using Mikroserwisy.DoctorApi.Services;

namespace Mikroserwisy.DoctorApi.Controllers
{
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _doctorService;
        public DoctorController(DoctorService patientService)
        {
            _doctorService = patientService;
        }
        [HttpGet("doctors")]
        public async Task<IEnumerable<Entities.Doctor?>> Read() => await _doctorService.Get();
        [HttpGet("doctors/{id}")]
        public async Task<IActionResult> ReadById(int id)
        {
            var dto = await _doctorService.GetById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }
        [HttpPost("doctor")]
        public async Task<IActionResult> Create([FromBody] Entities.Doctor dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _doctorService.Add(dto);
            return Ok();
        }
        [HttpDelete("doctors/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _doctorService.DeleteById(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpPut("doctors/{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] DoctorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _doctorService.Update(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
