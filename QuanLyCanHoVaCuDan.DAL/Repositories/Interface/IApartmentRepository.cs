using QuanLyCuDan.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IApartmentRepository
{
    Task<Apartment> GetByIdAsync(int id);
    Task<List<Apartment>> GetAllAsync();
    Task AddAsync(Apartment apartment);
    Task UpdateAsync(Apartment apartment);
    Task DeleteAsync(Apartment apartment);
    bool Exists(int id);
}
