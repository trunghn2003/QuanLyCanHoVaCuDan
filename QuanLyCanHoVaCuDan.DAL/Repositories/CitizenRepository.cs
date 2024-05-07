using Microsoft.EntityFrameworkCore;
using QuanLyCanHoVaCuDan.DAL.Repositories;
using QuanLyCanHoVaCuDan.Data;
using QuanLyCuDan.Model;

namespace QuanLyCanHoVaCuDan.Repositories
{
    public class CitizenRepository : GenericRepository<Citizen>
    {
        private readonly QuanLyCanHoVaCuDanContext _context;

        public CitizenRepository(QuanLyCanHoVaCuDanContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Citizen>> GetCitizensByApartmentIdAsync(int id)
        {
            return await _context.CitizenApartment
             .Where(ca => ca.ApartmentID == id)
             .Select(ca => ca.Citizen)
             .ToListAsync();
        }
    }
}
