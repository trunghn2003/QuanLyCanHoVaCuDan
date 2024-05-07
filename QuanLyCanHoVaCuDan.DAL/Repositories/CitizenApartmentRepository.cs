﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyCuDan.Model;
using QuanLyCanHoVaCuDan.Data;
using QuanLyCanHoVaCuDan.DAL.Repositories;

public class CitizenApartmentRepository : GenericRepository<CitizenApartment>
{
    private readonly QuanLyCanHoVaCuDanContext _context;

    public CitizenApartmentRepository(QuanLyCanHoVaCuDanContext context):base(context)
    {
        _context = context;
    }
   

    public async Task<CitizenApartment> GetCitizenApartmentAsync(int citizenId, int apartmentId)
    {
        return await _context.CitizenApartment
            .FirstOrDefaultAsync(ca => ca.CitizenId == citizenId && ca.ApartmentID == apartmentId);
    }

    public async Task<IEnumerable<CitizenApartment>> GetApartmentsByCitizenAsync(int citizenId)
    {
        return await _context.CitizenApartment
            .Where(ca => ca.CitizenId == citizenId)
            .ToListAsync();
    }

    public async Task<IEnumerable<CitizenApartment>> GetCitizensByApartmentAsync(int apartmentId)
    {
        return await _context.CitizenApartment
            .Where(ca => ca.ApartmentID == apartmentId)
            .ToListAsync();
    }

    public async Task AddCitizenApartmentAsync(CitizenApartment citizenApartment)
    {
        _context.CitizenApartment.Add(citizenApartment);
        await SaveAsync();
    }

    

    public async Task DeleteCitizenApartmentAsync(int citizenId, int apartmentId)
    {
        var citizenApartment = await GetCitizenApartmentAsync(citizenId, apartmentId);
        if (citizenApartment != null)
        {
            _context.CitizenApartment.Remove(citizenApartment);
            await SaveAsync();
        }
    }

    public async Task<bool> CitizenApartmentExistsAsync(int citizenId, int apartmentId)
    {
        return await _context.CitizenApartment
            .AnyAsync(e => e.CitizenId == citizenId && e.ApartmentID == apartmentId);
    }
   
}
