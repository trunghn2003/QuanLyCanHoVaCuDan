using Microsoft.EntityFrameworkCore;
using QuanLyCanHoVaCuDan.Data;
using QuanLyCuDan.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ApartmentRepository : IApartmentRepository, IDisposable
{
    private readonly QuanLyCanHoVaCuDanContext _context;

    public ApartmentRepository(QuanLyCanHoVaCuDanContext context)
    {
        _context = context;
    }

    public async Task<Apartment> GetByIdAsync(int id)
    {
        return await _context.Apartment.FindAsync(id);
    }

    public async Task<List<Apartment>> GetAllAsync()
    {
        return await _context.Apartment.ToListAsync();
    }

    public async Task AddAsync(Apartment apartment)
    {
        _context.Apartment.Add(apartment);
        //await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Apartment apartment)
    {
        _context.Entry(apartment).State = EntityState.Modified;
        //await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Apartment apartment)
    {
        _context.Apartment.Remove(apartment);
        //await _context.SaveChangesAsync();
    }
    public bool Exists(int id)
    {
        return _context.Apartment.Any(c => c.ApartmentId == id);
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
   
  

