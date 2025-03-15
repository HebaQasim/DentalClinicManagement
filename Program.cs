using DentalClinicManagement.ApiLayer.Services;
using DentalClinicManagement.Auth_Jwt;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using DentalClinicManagement.SharedLayer.Validation.EmailSettingValidation;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Get the connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register DbContext with SQL Server
builder.Services.AddDbContext<DentalClinicDbContext>(options =>
    options.UseSqlServer(connectionString));
//Add authentication services
builder.Services.AddAuthInfrastructure();
//Add AutoMapper Service

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//Add MediatR Service
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//Add IPasswordHasher Service
builder.Services.AddScoped<IPasswordHasher<Admin>, PasswordHasher<Admin>>();
builder.Services.AddScoped<IPasswordHasher<Doctor>, PasswordHasher<Doctor>>();
builder.Services.AddScoped<IPasswordHasher<CustomerService>, PasswordHasher<CustomerService>>();
//Add Repositories Services
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ICustomerServiceRepository, CustomerServiceRepository>();
builder.Services.AddScoped<IDoctorRepository,DoctorRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

//Add PasswordGenerator Service
builder.Services.AddScoped<IPasswordGenerator, PasswordGenerator>();
//Add Email Service
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IValidator<EmailSettings>, EmailSettingsValidator>();
builder.Services.AddTransient<IEmailService, EmailService>();
//Add fluent validation service
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

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
