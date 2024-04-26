using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyCuDan.Model;

public class CitizenApartmentService : ICitizenApartmentService
{
    private readonly ICitizenApartmentRepository _repository;

    public CitizenApartmentService(ICitizenApartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<CitizenApartment> GetCitizenApartmentAsync(int citizenId, int apartmentId)
    {
        return await _repository.GetCitizenApartmentAsync(citizenId, apartmentId);
    }

    public async Task<IEnumerable<CitizenApartment>> GetApartmentsByCitizenAsync(int citizenId)
    {
        return await _repository.GetApartmentsByCitizenAsync(citizenId);
    }

    public async Task<IEnumerable<CitizenApartment>> GetCitizensByApartmentAsync(int apartmentId)
    {
        return await _repository.GetCitizensByApartmentAsync(apartmentId);
    }

    public async Task AddCitizenApartmentAsync(CitizenApartment citizenApartment)
    {
        await _repository.AddCitizenApartmentAsync(citizenApartment);
    }

    public async Task UpdateCitizenApartmentAsync(CitizenApartment citizenApartment)
    {
        await _repository.UpdateCitizenApartmentAsync(citizenApartment);
    }

    public async Task DeleteCitizenApartmentAsync(int citizenId, int apartmentId)
    {
        await _repository.DeleteCitizenApartmentAsync(citizenId, apartmentId);
    }

    public async Task<bool> CitizenApartmentExistsAsync(int citizenId, int apartmentId)
    {
        return await _repository.CitizenApartmentExistsAsync(citizenId, apartmentId);
    }
}
