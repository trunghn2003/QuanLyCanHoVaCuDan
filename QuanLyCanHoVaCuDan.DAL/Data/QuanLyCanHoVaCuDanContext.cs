using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyCuDan.Model;

namespace QuanLyCanHoVaCuDan.Data
{
    public class QuanLyCanHoVaCuDanContext : DbContext
    {
        public QuanLyCanHoVaCuDanContext (DbContextOptions<QuanLyCanHoVaCuDanContext> options)
            : base(options)
        {
        }

        public DbSet<QuanLyCuDan.Model.Apartment> Apartment { get; set; } = default!;
        public DbSet<QuanLyCuDan.Model.Citizen> Citizen { get; set; } = default!;
        public DbSet<QuanLyCuDan.Model.CitizenApartment> CitizenApartment { get; set; } = default!;
    }
}
