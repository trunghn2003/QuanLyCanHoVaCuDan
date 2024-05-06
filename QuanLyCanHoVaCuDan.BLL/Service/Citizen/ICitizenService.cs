using QuanLyCanHoVaCuDan.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICitizenService
{
    Task<List<CitizenDto>> GetAllCitizensAsync();
    Task<List<CitizenDto>> GetCitizensByApartmentId(int id);
    Task<CitizenDto> GetCitizenByIdAsync(int id);
    Task<CitizenDto> CreateCitizenAsync(CitizenDto citizenDto);
    Task<bool> UpdateCitizenAsync(int id, CitizenDto citizenDto);
    Task<bool> DeleteCitizenAsync(int id);
}
