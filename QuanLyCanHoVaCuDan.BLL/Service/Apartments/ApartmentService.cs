using QuanLyCanHoVaCuDan.Dto;
using QuanLyCuDan.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ApartmentService : IApartmentService
{
    private readonly IApartmentRepository _apartmentRepository;

    public ApartmentService(IApartmentRepository apartmentRepository)
    {
        _apartmentRepository = apartmentRepository;
    }

    public async Task<List<ApartmentDto>> GetAllApartmentsAsync()
    {
        var apartments = await _apartmentRepository.GetAllAsync();
        return apartments.Select(a => new ApartmentDto
        {
            ApartmentID = a.ApartmentID,
            UnitNumber = a.UnitNumber,
            Floor = a.Floor,
            Size = a.Size
        }).ToList();
    }

    public async Task<ApartmentDto> GetApartmentByIdAsync(int id)
    {
        var apartment = await _apartmentRepository.GetByIdAsync(id);
        if (apartment == null)
            return null;
        return new ApartmentDto
        {
            ApartmentID = apartment.ApartmentID,
            UnitNumber = apartment.UnitNumber,
            Floor = apartment.Floor,
            Size = apartment.Size
        };
    }

    public async Task<ApartmentDto> CreateApartmentAsync(ApartmentDto apartmentDto)
    {
        var apartment = new Apartment
        {
            UnitNumber = apartmentDto.UnitNumber,
            Floor = apartmentDto.Floor,
            Size = apartmentDto.Size
        };
        await _apartmentRepository.AddAsync(apartment);
        apartmentDto.ApartmentID = apartment.ApartmentID;
        return apartmentDto;
    }

    public async Task<bool> UpdateApartmentAsync(int id, ApartmentDto apartmentDto)
    {
        var apartment = await _apartmentRepository.GetByIdAsync(id);
        if (apartment == null)
            return false;

        apartment.UnitNumber = apartmentDto.UnitNumber;
        apartment.Floor = apartmentDto.Floor;
        apartment.Size = apartmentDto.Size;
        await _apartmentRepository.UpdateAsync(apartment);
        return true;
    }

    public async Task<bool> DeleteApartmentAsync(int id)
    {
        var apartment = await _apartmentRepository.GetByIdAsync(id);
        if (apartment == null)
            return false;

        await _apartmentRepository.DeleteAsync(apartment);
        return true;
    }
}
