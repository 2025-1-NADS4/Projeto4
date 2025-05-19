using Fasor.Infrastructure.Data;
using Fasor.Application.Services.Companies.Interfaces;
using Fasor.Application.Services.Companies;
using Fasor.Infrastructure.Repositories.Companies.Interfaces;
using Fasor.Infrastructure.Repositories.Companies;
using Fasor.Infrastructure.Repositories.CompanyServices.Interfaces;
using Fasor.Infrastructure.Repositories.CompanyServices;
using Microsoft.EntityFrameworkCore;
using Fasor.Infrastructure.Repositories.CompanyRides.Interface;
using Fasor.Infrastructure.Repositories.CompanyRides;
using Fasor.Application.Services.Users.Interfaces;
using Fasor.Application.Services.Users;
using Fasor.Infrastructure.Repositories.Users.Interfaces;
using Fasor.Infrastructure.Repositories.Users;
using Fasor.Application.Services.CompanyServices.Interfaces;
using Fasor.Application.Services.CompanyServices;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IAppServicesRepository, AppServicesRepository>();
builder.Services.AddScoped<ICompanyRideService, CompanyRideService>();
builder.Services.AddScoped<ICompanyRideRepository, CompanyRideRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAppServicesService, AppServicesService>();
builder.Services.AddScoped<IAppServicesRepository, AppServicesRepository>();

// Correção aqui: adicionar AddControllers apenas uma vez com o ReferenceHandler
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
