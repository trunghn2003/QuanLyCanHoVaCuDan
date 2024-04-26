using Microsoft.AspNetCore.Mvc;
using QuanLyCuDan.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class CitizenApartmentController : ControllerBase
{
    private readonly ICitizenApartmentService _service;

    public CitizenApartmentController(ICitizenApartmentService service)
    {
        _service = service;
    }

    // GET: api/CitizenApartment
    [HttpGet("{citizenId}/{apartmentId}")]
    public async Task<ActionResult<CitizenApartment>> GetCitizenApartment(int citizenId, int apartmentId)
    {
        var citizenApartment = await _service.GetCitizenApartmentAsync(citizenId, apartmentId);
        if (citizenApartment == null)
        {
            return NotFound();
        }
        return Ok(citizenApartment);
    }

    // GET: api/CitizenApartment/ApartmentsByCitizen/5
    [HttpGet("ApartmentsByCitizen/{citizenId}")]
    public async Task<ActionResult<IEnumerable<CitizenApartment>>> GetApartmentsByCitizen(int citizenId)
    {
        var apartments = await _service.GetApartmentsByCitizenAsync(citizenId);
        return Ok(apartments);
    }

    // GET: api/CitizenApartment/CitizensByApartment/5
    [HttpGet("CitizensByApartment/{apartmentId}")]
    public async Task<ActionResult<IEnumerable<CitizenApartment>>> GetCitizensByApartment(int apartmentId)
    {
        var citizens = await _service.GetCitizensByApartmentAsync(apartmentId);
        return Ok(citizens);
    }

    // POST: api/CitizenApartment
    [HttpPost]
    public async Task<ActionResult<CitizenApartment>> PostCitizenApartment([FromBody] CitizenApartment citizenApartment)
    {
        await _service.AddCitizenApartmentAsync(citizenApartment);
        return CreatedAtAction(nameof(GetCitizenApartment), new { citizenId = citizenApartment.CitizenId, apartmentId = citizenApartment.ApartmentID }, citizenApartment);
    }

    // PUT: api/CitizenApartment/5/10
    [HttpPut("{citizenId}/{apartmentId}")]
    public async Task<IActionResult> PutCitizenApartment(int citizenId, int apartmentId, [FromBody] CitizenApartment citizenApartment)
    {
        if (citizenId != citizenApartment.CitizenId || apartmentId != citizenApartment.ApartmentID)
        {
            return BadRequest();
        }

        try
        {
            await _service.UpdateCitizenApartmentAsync(citizenApartment);
        }
        catch
        {
            if (!await _service.CitizenApartmentExistsAsync(citizenId, apartmentId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/CitizenApartment/5/10
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
