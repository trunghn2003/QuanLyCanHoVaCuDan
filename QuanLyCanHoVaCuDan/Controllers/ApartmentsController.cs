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
    public class ApartmentsController : ControllerBase
    {
        private readonly QuanLyCanHoVaCuDanContext _context;

        public ApartmentsController(QuanLyCanHoVaCuDanContext context)
        {
            _context = context;
        }

        private static ApartmentDto ApartmentToDto(Apartment apartment)
        {
            return new ApartmentDto
            {
                ApartmentID = apartment.ApartmentID,
                UnitNumber = apartment.UnitNumber,
                Floor = apartment.Floor,
                Size = apartment.Size
            };
        }

        // GET: api/Apartments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApartmentDto>>> GetApartment()
        {
            return await _context.Apartment
                .Select(a => ApartmentToDto(a))
                .ToListAsync();
        }

        // GET: api/Apartments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApartmentDto>> GetApartment(int id)
        {
            var apartment = await _context.Apartment.FindAsync(id);

            if (apartment == null)
            {
                return NotFound();
            }

            return ApartmentToDto(apartment);
        }

        // PUT: api/Apartments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApartment(int id, ApartmentDto apartmentDto)
        {
            var apartment = await _context.Apartment.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }

            apartment.UnitNumber = apartmentDto.UnitNumber;
            apartment.Floor = apartmentDto.Floor;
            apartment.Size = apartmentDto.Size;

            _context.Entry(apartment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApartmentExists(id))
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

        // POST: api/Apartments
        [HttpPost]
        public async Task<ActionResult<ApartmentDto>> PostApartment(ApartmentDto apartmentDto)
        {
            var apartment = new Apartment
            {
                UnitNumber = apartmentDto.UnitNumber,
                Floor = apartmentDto.Floor,
                Size = apartmentDto.Size
            };

            _context.Apartment.Add(apartment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApartment", new { id = apartment.ApartmentID }, ApartmentToDto(apartment));
        }

        // DELETE: api/Apartments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var apartment = await _context.Apartment.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }

            _context.Apartment.Remove(apartment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartment.Any(e => e.ApartmentID == id);
        }
    }
}
