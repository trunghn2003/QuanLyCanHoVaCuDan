using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyCuDan.Model;

public interface ICitizenApartmentRepository: IDisposable
{
    Task<IEnumerable<CitizenApartment>> GetAllCitizenApartmentsAsync();
    Task<CitizenApartment> GetCitizenApartmentAsync(int citizenId, int apartmentId);
    Task<IEnumerable<CitizenApartment>> GetApartmentsByCitizenAsync(int citizenId);
    Task<IEnumerable<CitizenApartment>> GetCitizensByApartmentAsync(int apartmentId);
    Task AddCitizenApartmentAsync(CitizenApartment citizenApartment);
    Task UpdateCitizenApartmentAsync(CitizenApartment citizenApartment);
    Task DeleteCitizenApartmentAsync(int citizenId, int apartmentId);

    Task<bool> CitizenApartmentExistsAsync(int citizenId, int apartmentId);
    Task SaveAsync();

}
