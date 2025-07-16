using DentalClinicManagement.ApiLayer.Services;
using DentalClinicManagement.Auth_Jwt;
using DentalClinicManagement.DomainLayer.Entities;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using DentalClinicManagement.DomainLayer.Interfaces.IServices;
using DentalClinicManagement.DomainLayer.Models;
using DentalClinicManagement.InfrastructureLayer.DbContexts;
using DentalClinicManagement.InfrastructureLayer.Repositories;
using DentalClinicManagement.SharedLayer.Validation.EmailSettingValidation;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000", // local development frontend
            "https://your-frontend.web.app" // deployed frontend (e.g. Firebase, Vercel, etc.)
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

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
builder.Services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();
builder.Services.AddScoped<ITreatmentRepository, TreatmentRepository>();

//Add Pagination Service
builder.Services.AddScoped(typeof(IPagination<>), typeof(PaginationRepository<>));
//Add PasswordGenerator Service
builder.Services.AddScoped<IPasswordGenerator, PasswordGenerator>();
//Add Email Service
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IValidator<EmailSettings>, EmailSettingsValidator>();
builder.Services.AddTransient<IEmailService, EmailService>();
//Add fluent validation service
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
//Get color code from appSetting
builder.Services.Configure<DoctorSettings>(builder.Configuration.GetSection("DoctorSettings"));
builder.Services.AddSingleton<DoctorColorProvider>();
//Add UserContext service
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserContext, UserContext>();


//var passwordHasher = new PasswordHasher<object>();
//string hashedPassword = passwordHasher.HashPassword(null, "heba");

//Console.WriteLine("Hashed Password: " + hashedPassword);

var app = builder.Build();
app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
