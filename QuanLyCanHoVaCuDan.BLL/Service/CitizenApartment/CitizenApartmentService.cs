using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyCuDan.Model;
using QuanLyCanHoVaCuDan.Dto;
using Microsoft.EntityFrameworkCore;
public class CitizenApartmentService : ICitizenApartmentService
{
    private readonly ICitizenApartmentRepository _repository;

    // Constructor without DI, for backward compatibility
    public CitizenApartmentService()
    {
        _repository = new CitizenApartmentRepository(new QuanLyCanHoVaCuDan.Data.QuanLyCanHoVaCuDanContext());
    }

    // Constructor with DI
    public CitizenApartmentService(ICitizenApartmentRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<CitizenApartmentDto>> GetAllCitizenApartmentsAsync()
    {
        var entities = await _repository.GetAllCitizenApartmentsAsync();
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
        var entity = await _repository.GetCitizenApartmentAsync(citizenId, apartmentId);
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
        var entities = await _repository.GetApartmentsByCitizenAsync(citizenId);
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
        var entities = await _repository.GetCitizensByApartmentAsync(apartmentId);
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

        await _repository.AddCitizenApartmentAsync(entity);
        await _repository.SaveAsync();
    }

    // Update a CitizenApartment using the DTO
    public async Task UpdateCitizenApartmentAsync(CitizenApartmentDto citizenApartmentDto)
    {
        var existingEntity = await _repository.GetCitizenApartmentAsync(citizenApartmentDto.CitizenId, citizenApartmentDto.ApartmentId);

        if (existingEntity == null)
        {
            throw new KeyNotFoundException("CitizenApartment not found");
        }

        existingEntity.StartDate = citizenApartmentDto.StartDate;
        existingEntity.EndDate = citizenApartmentDto.EndDate;

        try
        {
            await _repository.UpdateCitizenApartmentAsync(existingEntity);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new InvalidOperationException("Data has been modified or deleted since it was loaded");
        }
    }


    // Delete a CitizenApartment by citizenId and apartmentId
    public async Task DeleteCitizenApartmentAsync(int citizenId, int apartmentId)
    {
        await _repository.DeleteCitizenApartmentAsync(citizenId, apartmentId);
        await _repository.SaveAsync();
    }

    // Check if a CitizenApartment exists by citizenId and apartmentId
    public async Task<bool> CitizenApartmentExistsAsync(int citizenId, int apartmentId)
    {
        return await _repository.CitizenApartmentExistsAsync(citizenId, apartmentId);
    }

    public async Task<IEnumerable<CitizenApartmentDto>> GetAllCitizenApartmentsByApartmentIdAsync(int id)
    {
        var entities = await _repository.GetCitizensByApartmentAsync(id);
        return entities.Select(entity => new CitizenApartmentDto
        {
            CitizenId = entity.CitizenId,
            ApartmentId = entity.ApartmentID,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate
        });
    }
}
