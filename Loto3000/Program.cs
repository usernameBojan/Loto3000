using AutoMapper;
using Loto3000.Application.Mapper;
using Loto3000.Application.Repositories;
using Loto3000.Application.Services;
using Loto3000.Application.Services.Implementation;
using Loto3000.Domain.Models;
using Loto3000.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddScoped<ISuperAdminService, SuperAdminService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IRepository<Admin>, AdminRepository>();
builder.Services.AddScoped<IRepository<Player>, PlayerRepository>();
builder.Services.AddSingleton(sp => ModelMapper.GetConfiguration());
builder.Services.AddScoped(sp =>
{
    MapperConfiguration config = sp.GetRequiredService<MapperConfiguration>();
    return config.CreateMapper();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
