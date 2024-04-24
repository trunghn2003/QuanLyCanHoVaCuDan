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
    public class CitizensController : ControllerBase
    {
        private readonly QuanLyCanHoVaCuDanContext _context;

        public CitizensController(QuanLyCanHoVaCuDanContext context)
        {
            _context = context;
        }

        private static CitizenDto CitizenToDto(Citizen citizen)
        {
            return new CitizenDto
            {
                CitizenId = citizen.CitizenId,  // Map CitizenId
                Name = citizen.Name,
                PhoneNumber = citizen.PhoneNumber,
                Email = citizen.Email,
                DOB = citizen.DOB
            };
        }

        // GET: api/Citizens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitizenDto>>> GetCitizens()
        {
            var citizens = await _context.Citizen
                .Select(x => CitizenToDto(x))
                .ToListAsync();
            return citizens;
        }

        // GET: api/Citizens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitizenDto>> GetCitizen(int id)
        {
            var citizen = await _context.Citizen.FindAsync(id);

            if (citizen == null)
            {
                return NotFound();
            }

            return CitizenToDto(citizen);
        }

        // PUT: api/Citizens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitizen(int id, CitizenDto citizenDto)
        {
            

            var citizen = await _context.Citizen.FindAsync(id);
            if (citizen == null)
            {
                return NotFound();
            }

            citizen.Name = citizenDto.Name;
            citizen.PhoneNumber = citizenDto.PhoneNumber;
            citizen.Email = citizenDto.Email;
            citizen.DOB = citizenDto.DOB;

            _context.Entry(citizen).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitizenExists(id))
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

        // POST: api/Citizens
        [HttpPost]
        public async Task<ActionResult<CitizenDto>> PostCitizen(CitizenDto citizenDto)
        {
            var citizen = new Citizen
            {
                Name = citizenDto.Name,
                PhoneNumber = citizenDto.PhoneNumber,
                Email = citizenDto.Email,
                DOB = citizenDto.DOB
            };

            _context.Citizen.Add(citizen);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCitizen), new { id = citizen.CitizenId }, CitizenToDto(citizen));
        }

        // DELETE: api/Citizens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitizen(int id)
        {
            var citizen = await _context.Citizen.FindAsync(id);
            if (citizen == null)
            {
                return NotFound();
            }

            _context.Citizen.Remove(citizen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitizenExists(int id)
        {
            return _context.Citizen.Any(e => e.CitizenId == id);
        }
    }
}
