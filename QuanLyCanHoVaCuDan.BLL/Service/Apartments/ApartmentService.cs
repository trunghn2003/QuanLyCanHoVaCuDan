using QuanLyCanHoVaCuDan.DAL.Repositories;
using QuanLyCanHoVaCuDan.Dto;
using QuanLyCuDan.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ApartmentService : IApartmentService
{
    private readonly UnitOfWork _unitOfWork;

    public ApartmentService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ApartmentDto>> GetAllApartmentsAsync()
    {
        var apartments = await _unitOfWork.ApartmentRepository.GetAllAsync();
        return apartments.Select(a => new ApartmentDto
        {
            ApartmentId = a.ApartmentId,
            UnitNumber = a.UnitNumber,
            Floor = a.Floor,
            Size = a.Size
        }).ToList();
    }

    public async Task<ApartmentDto> GetApartmentByIdAsync(int id)
    {
        var apartment = await _unitOfWork.ApartmentRepository.GetByIdAsync(id);
        if (apartment == null)
            return null;
        return new ApartmentDto
        {
            ApartmentId = apartment.ApartmentId,
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
         _unitOfWork.ApartmentRepository.AddAsync(apartment);
        _unitOfWork.Save(); // Lưu thay đổi bằng UnitOfWork

        apartmentDto.ApartmentId = apartment.ApartmentId;
        return apartmentDto;
    }

    public async Task<bool> UpdateApartmentAsync(int id, ApartmentDto apartmentDto)
    {
        var apartment = await _unitOfWork.ApartmentRepository.GetByIdAsync(id);
        if (apartment == null)
            return false;

        apartment.UnitNumber = apartmentDto.UnitNumber;
        apartment.Floor = apartmentDto.Floor;
        apartment.Size = apartmentDto.Size;
        await _unitOfWork.ApartmentRepository.UpdateAsync(apartment);
        _unitOfWork.Save(); // Lưu thay đổi bằng UnitOfWork

        return true;
    }

    public async Task<bool> DeleteApartmentAsync(int id)
    {
        var apartment = await _unitOfWork.ApartmentRepository.GetByIdAsync(id);
        if (apartment == null)
            return false;

        await _unitOfWork.ApartmentRepository.DeleteAsync(apartment);
        _unitOfWork.Save(); // Lưu thay đổi bằng UnitOfWork
        return true;
    }
}
