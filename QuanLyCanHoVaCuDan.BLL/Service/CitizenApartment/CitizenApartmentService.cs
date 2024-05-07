using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyCuDan.Model;
using QuanLyCanHoVaCuDan.Dto;
using Microsoft.EntityFrameworkCore;
using QuanLyCanHoVaCuDan.DAL.Repositories;
public class CitizenApartmentService : ICitizenApartmentService
{
    private readonly UnitOfWork _unitOfWork;


    // Constructor without DI, for backward compatibility
    public CitizenApartmentService(UnitOfWork unit)
    {
        _unitOfWork = unit;
    }

    // Constructor with DI
   
    public async Task<IEnumerable<CitizenApartmentDto>> GetAllCitizenApartmentsAsync()
    {
        var entities = await _unitOfWork.CitizenApartmentRepository.GetAllAsync();
        return entities.Select(entity => new CitizenApartmentDto
        {
            CitizenId = entity.CitizenId,
            ApartmentId = entity.ApartmentID,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate
        });
    }

    // Get a single CitizenApartment by citizenId and apartmentId, converted to a DTO
    public async Task<CitizenApartmentDto> GetCitizenApartmentAsync(int citizenId, int apartmentId)
    {
        var entity = await _unitOfWork.CitizenApartmentRepository.GetCitizenApartmentAsync(citizenId, apartmentId);
        return entity != null ? new CitizenApartmentDto
        {
            CitizenId = entity.CitizenId,
            ApartmentId = entity.ApartmentID,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate
        } : null;
    }

    // Get all apartments by citizen, returned as a list of DTOs
    public async Task<IEnumerable<CitizenApartmentDto>> GetApartmentsByCitizenAsync(int citizenId)
    {
        var entities = await _unitOfWork.CitizenApartmentRepository.GetApartmentsByCitizenAsync(citizenId);
        return entities.Select(entity => new CitizenApartmentDto
        {
            CitizenId = entity.CitizenId,
            ApartmentId = entity.ApartmentID,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate
        });
    }

    // Get all citizens by apartment, returned as a list of DTOs
    public async Task<IEnumerable<CitizenApartmentDto>> GetCitizensByApartmentAsync(int apartmentId)
    {
        var entities = await _unitOfWork.CitizenApartmentRepository.GetCitizensByApartmentAsync(apartmentId);
        return entities.Select(entity => new CitizenApartmentDto
        {
            CitizenId = entity.CitizenId,
            ApartmentId = entity.ApartmentID,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate
        });
    }

    // Add a new CitizenApartment using the DTO
    public async Task AddCitizenApartmentAsync(CitizenApartmentDto citizenApartmentDto)
    {
        var entity = new CitizenApartment
        {
            CitizenId = citizenApartmentDto.CitizenId,
            ApartmentID = citizenApartmentDto.ApartmentId,
            StartDate = citizenApartmentDto.StartDate,
            EndDate = citizenApartmentDto.EndDate
        };

        await _unitOfWork.CitizenApartmentRepository.AddCitizenApartmentAsync(entity);
        await _unitOfWork.CitizenApartmentRepository.SaveAsync();
    }

    // Update a CitizenApartment using the DTO
    public async Task UpdateCitizenApartmentAsync(CitizenApartmentDto citizenApartmentDto)
    {
        var existingEntity = await _unitOfWork.CitizenApartmentRepository.GetCitizenApartmentAsync(citizenApartmentDto.CitizenId, citizenApartmentDto.ApartmentId);

        if (existingEntity == null)
        {
            throw new KeyNotFoundException("CitizenApartment not found");
        }

        existingEntity.StartDate = citizenApartmentDto.StartDate;
        existingEntity.EndDate = citizenApartmentDto.EndDate;

        try
        {
            await _unitOfWork.CitizenApartmentRepository.UpdateAsync(existingEntity);
            await _unitOfWork.CitizenApartmentRepository.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new InvalidOperationException("Data has been modified or deleted since it was loaded");
        }
    }


    // Delete a CitizenApartment by citizenId and apartmentId
    public async Task DeleteCitizenApartmentAsync(int citizenId, int apartmentId)
    {
        await _unitOfWork.CitizenApartmentRepository.DeleteCitizenApartmentAsync(citizenId, apartmentId);
        await _unitOfWork.CitizenApartmentRepository.SaveAsync();
    }

    // Check if a CitizenApartment exists by citizenId and apartmentId
    public async Task<bool> CitizenApartmentExistsAsync(int citizenId, int apartmentId)
    {
        return await _unitOfWork.CitizenApartmentRepository.CitizenApartmentExistsAsync(citizenId, apartmentId);
    }

    public async Task<IEnumerable<CitizenApartmentDto>> GetAllCitizenApartmentsByApartmentIdAsync(int id)
    {
        var entities = await _unitOfWork.CitizenApartmentRepository.GetCitizensByApartmentAsync(id);
        return entities.Select(entity => new CitizenApartmentDto
        {
            CitizenId = entity.CitizenId,
            ApartmentId = entity.ApartmentID,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate
        });
    }
}
