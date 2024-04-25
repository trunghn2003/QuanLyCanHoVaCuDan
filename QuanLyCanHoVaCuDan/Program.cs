using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuanLyCanHoVaCuDan.Data;
using QuanLyCanHoVaCuDan.Repositories.Interface;
using QuanLyCanHoVaCuDan.Repositories;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<QuanLyCanHoVaCuDanContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuanLyCanHoVaCuDanContext") ?? throw new InvalidOperationException("Connection string 'QuanLyCanHoVaCuDanContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
builder.Services.AddScoped<IApartmentService, ApartmentService>();
builder.Services.AddScoped<ICitizenRepository, CitizenRepository>();
builder.Services.AddScoped<ICitizenService, CitizenService>();
builder.Services.AddScoped<ICitizenApartmentRepository, CitizenApartmentRepository>();
builder.Services.AddScoped<ICitizenApartmentService, CitizenApartmentService>();

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
