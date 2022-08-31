using AutoMapper;
using HashidsNet;
using Loto3000.Application.Mapper;
using Loto3000.Application.Repositories;
using Loto3000.Application.Services;
using Loto3000.Application.Services.Implementation;
using Loto3000.Domain.Entities;
using Loto3000.Infrastructure;
using Loto3000.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddScoped<ISuperAdminService, SuperAdminService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IDrawService, DrawService>();
builder.Services.AddScoped<IRepository<Admin>, BaseRepository<Admin>>();
builder.Services.AddScoped<IRepository<Player>, BaseRepository<Player>>();
builder.Services.AddScoped<IRepository<Draw>, BaseRepository<Draw>>();
//builder.Services.AddScoped<IRepository<Session>, BaseRepository<Session>>();
builder.Services.AddScoped<IRepository<TransactionTracker>, BaseRepository<TransactionTracker>>();
//builder.Services.AddScoped<IRepository<Admin>, AdminRepository>();
//builder.Services.AddScoped<IRepository<Player>, PlayerRepository>();
//builder.Services.AddScoped<IRepository<TransactionTracker>, TransactionsRepository>();
//builder.Services.AddScoped<IRepository<Draw>, DrawRepository>();
builder.Services.AddScoped<IHashids>((sp) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var secret = configuration["Secret"];
    return new Hashids(secret);
});
builder.Services.AddSingleton(sp => ModelMapper.GetConfiguration());
builder.Services.AddScoped(sp => sp.GetRequiredService<MapperConfiguration>().CreateMapper());
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
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
