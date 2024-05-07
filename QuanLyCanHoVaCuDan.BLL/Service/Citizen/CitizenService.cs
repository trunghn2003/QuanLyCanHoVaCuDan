using QuanLyCanHoVaCuDan.Dto;
using QuanLyCuDan.Model;
using QuanLyCanHoVaCuDan.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyCanHoVaCuDan.DAL.Repositories;

public class CitizenService : ICitizenService
{
    private readonly UnitOfWork _unitOfWork;
    public CitizenService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    

    public async Task<List<CitizenDto>> GetAllCitizensAsync()
    {
        var citizens = await _unitOfWork.CitizenRepository.GetAllAsync();
        return citizens.Select(c => new CitizenDto
        {
            CitizenId = c.CitizenId,
            Name = c.Name,
            PhoneNumber = c.PhoneNumber,
            Email = c.Email,
            DOB = c.DOB
        }).ToList();
    }

    public async Task<CitizenDto> GetCitizenByIdAsync(int id)
    {
        var citizen = await _unitOfWork.CitizenRepository.GetByIdAsync(id);
        if (citizen == null)
            return null;

        return new CitizenDto
        {
            CitizenId = citizen.CitizenId,
            Name = citizen.Name,
            PhoneNumber = citizen.PhoneNumber,
            Email = citizen.Email,
            DOB = citizen.DOB
        };
    }

    public async Task<CitizenDto> CreateCitizenAsync(CitizenDto citizenDto)
    {
        var citizen = new Citizen
        {
            Name = citizenDto.Name,
            PhoneNumber = citizenDto.PhoneNumber,
            Email = citizenDto.Email,
            DOB = citizenDto.DOB
        };

        await _unitOfWork.CitizenRepository.AddAsync(citizen);
        await _unitOfWork.CitizenRepository.SaveAsync();

        citizenDto.CitizenId = citizen.CitizenId;
        return citizenDto;
    }

    public async Task<bool> UpdateCitizenAsync(int id, CitizenDto citizenDto)
    {
        var citizen = await _unitOfWork.CitizenRepository.GetByIdAsync(id);
        if (citizen == null)
            return false;

        citizen.Name = citizenDto.Name;
        citizen.PhoneNumber = citizenDto.PhoneNumber;
        citizen.Email = citizenDto.Email;
        citizen.DOB = citizenDto.DOB;

        await _unitOfWork.CitizenRepository.UpdateAsync(citizen);
        await _unitOfWork.CitizenRepository.SaveAsync();

        return true;
    }

    public async Task<bool> DeleteCitizenAsync(int id)
    {
        var citizen = await _unitOfWork.CitizenRepository.GetByIdAsync(id);
        if (citizen == null)
            return false;

        await _unitOfWork.CitizenRepository.DeleteAsync(citizen);
        await _unitOfWork.CitizenRepository.SaveAsync();

        return true;
    }

    public async Task<List<CitizenDto>> GetCitizensByApartmentId(int id)
    {
        var apartment = await _unitOfWork.ApartmentRepository.GetByIdAsync(id);
        if (apartment == null)
            return new List<CitizenDto>();

        // Retrieve citizens for the given apartment
        var citizens = await _unitOfWork.CitizenRepository.GetCitizensByApartmentIdAsync(id);

        // Convert entities to DTOs
        return citizens.Select(c => new CitizenDto
        {
            CitizenId = c.CitizenId,
            Name = c.Name,
            PhoneNumber = c.PhoneNumber,
            Email = c.Email,
            DOB = c.DOB
        }).ToList();
    }

}
