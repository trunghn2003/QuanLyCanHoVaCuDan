using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuanLyCanHoVaCuDan.Data;
using QuanLyCanHoVaCuDan.Repositories;
using QuanLyCuDan.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<QuanLyCanHoVaCuDanContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuanLyCanHoVaCuDanContext") ?? throw new InvalidOperationException("Connection string 'QuanLyCanHoVaCuDanContext' not found.")
    , x => x.MigrationsAssembly("QuanLyCanHoVaCuDan.DAL")));

// Add services to the container.

builder.Services.AddControllers();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
