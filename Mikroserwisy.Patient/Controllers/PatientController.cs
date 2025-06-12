using Microsoft.AspNetCore.Mvc;
using Mikroserwisy.PatientApi.Dtos;
using Mikroserwisy.PatientApi.Services;

namespace Mikroserwisy.PatientApi.Controllers
{
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;
        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpGet("patients")]
        public async Task<IEnumerable<Entities.Patient?>> Read() => await _patientService.Get();
        [HttpGet("patients/{id}")]
        public async Task<IActionResult> ReadById(int id)
        {
            var dto = await _patientService.GetById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }
        [HttpPost("patient")]
        public async Task<IActionResult> Create([FromBody] Entities.Patient dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _patientService.Add(dto);
            return Ok();
        }
        [HttpDelete("patients/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _patientService.DeleteById(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpPut("patients/{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] PatientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _patientService.Update(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
