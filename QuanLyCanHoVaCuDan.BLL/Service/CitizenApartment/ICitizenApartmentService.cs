using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyCanHoVaCuDan.Dto;
public interface ICitizenApartmentService
{
    Task<IEnumerable<CitizenApartmentDto>> GetAllCitizenApartmentsAsync();
    Task<CitizenApartmentDto> GetCitizenApartmentAsync(int citizenId, int apartmentId);
    Task<IEnumerable<CitizenApartmentDto>> GetApartmentsByCitizenAsync(int citizenId);
    Task<IEnumerable<CitizenApartmentDto>> GetCitizensByApartmentAsync(int apartmentId);
    Task AddCitizenApartmentAsync(CitizenApartmentDto citizenApartmentDto);
    Task UpdateCitizenApartmentAsync(CitizenApartmentDto citizenApartmentDto);
    Task DeleteCitizenApartmentAsync(int citizenId, int apartmentId);
    Task<bool> CitizenApartmentExistsAsync(int citizenId, int apartmentId);
}
