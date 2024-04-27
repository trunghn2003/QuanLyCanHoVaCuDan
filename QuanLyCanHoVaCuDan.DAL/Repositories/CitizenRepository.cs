using Microsoft.EntityFrameworkCore;
using QuanLyCanHoVaCuDan.Data;
using QuanLyCanHoVaCuDan.Repositories.Interface;
using QuanLyCuDan.Model;

namespace QuanLyCanHoVaCuDan.Repositories
{
    public class CitizenRepository : ICitizenRepository, IDisposable
    {
        private readonly QuanLyCanHoVaCuDanContext _context;

        public CitizenRepository(QuanLyCanHoVaCuDanContext context)
        {
            _context = context;
        }

        public async Task<Citizen> GetByIdAsync(int id)
        {
            return await _context.Citizen.FindAsync(id);
        }

        public async Task<List<Citizen>> GetAllAsync()
        {
            return await _context.Citizen.ToListAsync();
        }

        public async Task AddAsync(Citizen citizen)
        {
            _context.Citizen.Add(citizen);
            //await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Citizen citizen)
        {
            _context.Entry(citizen).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Citizen citizen)
        {
            _context.Citizen.Remove(citizen);
            //await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _context.Citizen.Any(c => c.CitizenId == id);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
