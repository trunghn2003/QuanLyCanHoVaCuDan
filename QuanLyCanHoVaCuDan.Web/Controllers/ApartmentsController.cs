using Microsoft.AspNetCore.Mvc;
using QuanLyCanHoVaCuDan.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ApartmentsController : ControllerBase
{
    private readonly IApartmentService _apartmentService;

    public ApartmentsController(IApartmentService apartmentService)
    {
        _apartmentService = apartmentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApartmentDto>>> GetApartments()
    {
        return Ok(await _apartmentService.GetAllApartmentsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApartmentDto>> GetApartment(int id)
    {
        var apartment = await _apartmentService.GetApartmentByIdAsync(id);
        if (apartment == null)
        {
            return NotFound();
        }
        return apartment;
    }

    [HttpPost]
    public async Task<ActionResult<ApartmentDto>> PostApartment(ApartmentDto apartmentDto)
    {
        var newApartment = await _apartmentService.CreateApartmentAsync(apartmentDto);
        return CreatedAtAction(nameof(GetApartment), new { id = newApartment.ApartmentID }, newApartment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutApartment(int id, ApartmentDto apartmentDto)
    {
        if (!await _apartmentService.UpdateApartmentAsync(id, apartmentDto))
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApartment(int id)
    {
        if (!await _apartmentService.DeleteApartmentAsync(id))
        {
            return NotFound();
        }
        return NoContent();
    }
}
