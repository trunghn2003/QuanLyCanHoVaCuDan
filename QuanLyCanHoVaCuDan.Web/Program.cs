using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuanLyCanHoVaCuDan.Data;
using QuanLyCanHoVaCuDan.Repositories.Interface;
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

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
app.UseCors("AllowSpecificOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
