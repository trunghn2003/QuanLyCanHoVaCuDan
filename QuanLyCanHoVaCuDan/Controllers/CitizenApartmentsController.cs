using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCanHoVaCuDan.Data;
using QuanLyCuDan.Model;
using QuanLyCanHoVaCuDan.Dto;

namespace QuanLyCanHoVaCuDan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenApartmentsController : ControllerBase
    {
        private readonly QuanLyCanHoVaCuDanContext _context;

        public CitizenApartmentsController(QuanLyCanHoVaCuDanContext context)
        {
            _context = context;
        }

        // GET: api/CitizenApartments/5/10
        [HttpGet("{citizenId}/{apartmentId}")]
        public async Task<ActionResult<CitizenApartmentDto>> GetCitizenApartment(int citizenId, int apartmentId)
        {
            var citizenApartment = await _context.CitizenApartment
                .FirstOrDefaultAsync(ca => ca.CitizenId == citizenId && ca.ApartmentID == apartmentId);

            if (citizenApartment == null)
            {
                return NotFound();
            }

            return new CitizenApartmentDto
            {
                CitizenId = citizenApartment.CitizenId,
                ApartmentID = citizenApartment.ApartmentID,
                StartDate = citizenApartment.StartDate,
                EndDate = citizenApartment.EndDate
            };
        }

        // PUT: api/CitizenApartments/5/10
        [HttpPut("{citizenId}/{apartmentId}")]
        public async Task<IActionResult> PutCitizenApartment(int citizenId, int apartmentId, CitizenApartmentDto citizenApartmentDto)
        {
            var citizenApartment = await _context.CitizenApartment
                .FirstOrDefaultAsync(ca => ca.CitizenId == citizenId && ca.ApartmentID == apartmentId);

            if (citizenApartment == null)
            {
                return NotFound();
            }

            citizenApartment.StartDate = citizenApartmentDto.StartDate;
            citizenApartment.EndDate = citizenApartmentDto.EndDate;

            _context.Entry(citizenApartment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/CitizenApartments
        [HttpPost]
        public async Task<ActionResult<CitizenApartmentDto>> PostCitizenApartment(CitizenApartmentDto citizenApartmentDto)
        {
            // Check if the CitizenApartment record already exists
            bool exists = await _context.CitizenApartment.AnyAsync(ca => ca.CitizenId == citizenApartmentDto.CitizenId && ca.ApartmentID == citizenApartmentDto.ApartmentID);
            if (exists)
            {
                return Conflict("A relationship between the given citizen and apartment already exists.");
            }

            var citizenApartment = new CitizenApartment
            {
                CitizenId = citizenApartmentDto.CitizenId,
                ApartmentID = citizenApartmentDto.ApartmentID,
                StartDate = citizenApartmentDto.StartDate,
                EndDate = citizenApartmentDto.EndDate
            };

            _context.CitizenApartment.Add(citizenApartment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitizenApartment", new { citizenId = citizenApartment.CitizenId, apartmentId = citizenApartment.ApartmentID }, citizenApartmentDto);
        }
        // GET: api/CitizenApartments/Citizens/5
        [HttpGet("Citizens/{citizenId}")]
        public async Task<ActionResult<IEnumerable<CitizenApartmentDto>>> GetApartmentsByCitizen(int citizenId)
        {
            var citizenApartments = await _context.CitizenApartment
                .Where(ca => ca.CitizenId == citizenId)
                .Select(ca => new CitizenApartmentDto
                {
                    CitizenId = ca.CitizenId,
                    ApartmentID = ca.ApartmentID,
                    StartDate = ca.StartDate,
                    EndDate = ca.EndDate
                })
                .ToListAsync();

            if (!citizenApartments.Any())
            {
                return NotFound("No apartments found for this citizen.");
            }

            return citizenApartments;
        }
        // GET: api/CitizenApartments/Apartments/10
        [HttpGet("Apartments/{apartmentId}")]
        public async Task<ActionResult<IEnumerable<CitizenApartmentDto>>> GetCitizensByApartment(int apartmentId)
        {
            var citizenApartments = await _context.CitizenApartment
                .Where(ca => ca.ApartmentID == apartmentId)
                .Select(ca => new CitizenApartmentDto
                {
                    CitizenId = ca.CitizenId,
                    ApartmentID = ca.ApartmentID,
                    StartDate = ca.StartDate,
                    EndDate = ca.EndDate
                })
                .ToListAsync();

            if (!citizenApartments.Any())
            {
                return NotFound("No citizens found for this apartment.");
            }

            return citizenApartments;
        }
        private bool CitizenApartmentExists(int citizenId, int apartmentId)
        {
            return _context.CitizenApartment.Any(e => e.CitizenId == citizenId && e.ApartmentID == apartmentId);
        }
    }
}
