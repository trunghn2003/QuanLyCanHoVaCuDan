using QuanLyCanHoVaCuDan.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IApartmentService
{
    Task<List<ApartmentDto>> GetAllApartmentsAsync();
    Task<ApartmentDto> GetApartmentByIdAsync(int id);
    Task<ApartmentDto> CreateApartmentAsync(ApartmentDto apartmentDto);
    Task<bool> UpdateApartmentAsync(int id, ApartmentDto apartmentDto);
    Task<bool> DeleteApartmentAsync(int id);
}
