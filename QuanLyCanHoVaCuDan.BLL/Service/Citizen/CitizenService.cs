using QuanLyCanHoVaCuDan.Dto;
using QuanLyCuDan.Model;
using QuanLyCanHoVaCuDan.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyCanHoVaCuDan.Repositories.Interface;

public class CitizenService : ICitizenService
{
    private readonly ICitizenRepository _citizenRepository;
    public CitizenService()
    {
        _citizenRepository = new CitizenRepository(new QuanLyCanHoVaCuDan.Data.QuanLyCanHoVaCuDanContext());
    }
    public CitizenService(ICitizenRepository citizenRepository)
    {
        _citizenRepository = citizenRepository;
    }

    public async Task<List<CitizenDto>> GetAllCitizensAsync()
    {
        var citizens = await _citizenRepository.GetAllAsync();
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
        var citizen = await _citizenRepository.GetByIdAsync(id);
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

        await _citizenRepository.AddAsync(citizen);
        await _citizenRepository.SaveAsync();

        citizenDto.CitizenId = citizen.CitizenId;
        return citizenDto;
    }

    public async Task<bool> UpdateCitizenAsync(int id, CitizenDto citizenDto)
    {
        var citizen = await _citizenRepository.GetByIdAsync(id);
        if (citizen == null)
            return false;

        citizen.Name = citizenDto.Name;
        citizen.PhoneNumber = citizenDto.PhoneNumber;
        citizen.Email = citizenDto.Email;
        citizen.DOB = citizenDto.DOB;

        await _citizenRepository.UpdateAsync(citizen);
        await _citizenRepository.SaveAsync();

        return true;
    }

    public async Task<bool> DeleteCitizenAsync(int id)
    {
        var citizen = await _citizenRepository.GetByIdAsync(id);
        if (citizen == null)
            return false;

        await _citizenRepository.DeleteAsync(citizen);
        await _citizenRepository.SaveAsync();

        return true;
    }
}
