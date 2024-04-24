using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuanLyCanHoVaCuDan.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<QuanLyCanHoVaCuDanContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuanLyCanHoVaCuDanContext") ?? throw new InvalidOperationException("Connection string 'QuanLyCanHoVaCuDanContext' not found.")));

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
