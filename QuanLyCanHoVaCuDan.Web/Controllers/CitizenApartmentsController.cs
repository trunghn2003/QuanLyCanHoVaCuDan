using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyCanHoVaCuDan.Dto;
[Route("api/[controller]")]
[ApiController]
public class CitizenApartmentController : ControllerBase
{
    private readonly ICitizenApartmentService _service;

    public CitizenApartmentController(ICitizenApartmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CitizenApartmentDto>>> GetAllCitizenApartments()
    {
        var citizenApartments = await _service.GetAllCitizenApartmentsAsync();
        return Ok(citizenApartments);
    }

    // GET: api/CitizenApartment/{citizenId}/{apartmentId}
    [HttpGet("{citizenId}/{apartmentId}")]
    public async Task<ActionResult<CitizenApartmentDto>> GetCitizenApartment(int citizenId, int apartmentId)
    {
        var citizenApartment = await _service.GetCitizenApartmentAsync(citizenId, apartmentId);
        if (citizenApartment == null)
        {
            return NotFound();
        }
        return Ok(citizenApartment);
    }

    // GET: api/CitizenApartment/ApartmentsByCitizen/{citizenId}
    [HttpGet("ApartmentsByCitizen/{citizenId}")]
    public async Task<ActionResult<IEnumerable<CitizenApartmentDto>>> GetApartmentsByCitizen(int citizenId)
    {
        var apartments = await _service.GetApartmentsByCitizenAsync(citizenId);
        return Ok(apartments);
    }

    // GET: api/CitizenApartment/CitizensByApartment/{apartmentId}
    [HttpGet("CitizensByApartment/{apartmentId}")]
    public async Task<ActionResult<IEnumerable<CitizenApartmentDto>>> GetCitizensByApartment(int apartmentId)
    {
        var citizens = await _service.GetCitizensByApartmentAsync(apartmentId);
        return Ok(citizens);
    }

    // POST: api/CitizenApartment
    [HttpPost]
    public async Task<ActionResult<CitizenApartmentDto>> PostCitizenApartment([FromBody] CitizenApartmentDto citizenApartmentDto)
    {
        await _service.AddCitizenApartmentAsync(citizenApartmentDto);
        return CreatedAtAction(nameof(GetCitizenApartment), new { citizenId = citizenApartmentDto.CitizenId, apartmentId = citizenApartmentDto.ApartmentId }, citizenApartmentDto);
    }

    // PUT: api/CitizenApartment/{citizenId}/{apartmentId}
    [HttpPut("{citizenId}/{apartmentId}")]
    public async Task<IActionResult> PutCitizenApartment(int citizenId, int apartmentId, [FromBody] CitizenApartmentDto citizenApartmentDto)
    {
        // Ensure the citizenId and apartmentId in the route match those in the request body
        if (citizenId != citizenApartmentDto.CitizenId || apartmentId != citizenApartmentDto.ApartmentId)
        {
            return BadRequest("The IDs in the URL must match the IDs in the body.");
        }

        // Ensure the entity exists before updating
        var exists = await _service.CitizenApartmentExistsAsync(citizenId, apartmentId);
        if (!exists)
        {
            return NotFound();
        }

        try
        {
            await _service.UpdateCitizenApartmentAsync(citizenApartmentDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            // Log the exception (e.g., using a logging framework)
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // DELETE: api/CitizenApartment/{citizenId}/{apartmentId}
    [HttpDelete("{citizenId}/{apartmentId}")]
    public async Task<IActionResult> DeleteCitizenApartment(int citizenId, int apartmentId)
    {
        if (!await _service.CitizenApartmentExistsAsync(citizenId, apartmentId))
        {
            return NotFound();
        }

        await _service.DeleteCitizenApartmentAsync(citizenId, apartmentId);
        return NoContent();
    }
}
