using QuanLyCuDan.Model;

namespace QuanLyCanHoVaCuDan.Repositories.Interface
{
    public interface ICitizenRepository
    {
        Task<Citizen> GetByIdAsync(int id);
        Task<List<Citizen>> GetAllAsync();
        Task<List<Citizen>> GetCitizensByApartmentIdAsync(int id);
        Task AddAsync(Citizen citizen);
        Task UpdateAsync(Citizen citizen);
        Task DeleteAsync(Citizen citizen);
        bool Exists(int id);
        Task SaveAsync();


    }

}
