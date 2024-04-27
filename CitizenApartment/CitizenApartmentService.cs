using System.Collections.Generic;
using System.Threading.Tasks;
using QLCuDan.DAL.Model;
using QLCuDan.DAL.Repositories;

public class CitizenApartmentService : ICitizenApartmentService
{
    UnitOfWork _unitOfWork;

    public CitizenApartmentService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CitizenApartmentService> GetCitizenApartmentAsync(int citizenId, int apartmentId)
    {
        return await _unitOfWork.CitizenApartmentRepository.GetCitizenApartmentAsync(citizenId, apartmentId);
    }

    public async Task<IEnumerable<CitizenApartment>> GetApartmentsByCitizenAsync(int citizenId)
    {
        return await _unitOfWork.CitizenApartmentRepository.GetApartmentsByCitizenAsync(citizenId);
    }

    public async Task<IEnumerable<CitizenApartment>> GetCitizensByApartmentAsync(int apartmentId)
    {
        return await _unitOfWork.CitizenApartmentRepository.GetCitizensByApartmentAsync(apartmentId);
    }

    public async Task AddCitizenApartmentAsync(CitizenApartment citizenApartment)
    {
        await _unitOfWork.CitizenApartmentRepository.AddCitizenApartmentAsync(citizenApartment);
    }

    public async Task UpdateCitizenApartmentAsync(CitizenApartment citizenApartment)
    {
        await _unitOfWork.CitizenApartmentRepository.UpdateCitizenApartmentAsync(citizenApartment);
    }

    public async Task DeleteCitizenApartmentAsync(int citizenId, int apartmentId)
    {
        await _unitOfWork.CitizenApartmentRepository.DeleteCitizenApartmentAsync(citizenId, apartmentId);
    }

    public async Task<bool> CitizenApartmentExistsAsync(int citizenId, int apartmentId)
    {
        return await _unitOfWork.CitizenApartmentRepository.CitizenApartmentExistsAsync(citizenId, apartmentId);
    }
}
