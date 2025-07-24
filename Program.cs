using CommunityEventHub.DAL;
using CommunityEventHub.Data;
using CommunityEventHub.Mapper;
using CommunityEventHub.Models.Dto;
using CommunityEventHub.Services;
using CommunityEventHub.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventHub;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add DbContext to services
        builder.Services.AddDbContext<CommunityEventHubContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Register dependencies (Repositories, Services, AutoMapper)
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
        builder.Services.AddAutoMapper(typeof(MappingProfile)); // AutoMapper configuration
        builder.Services.AddScoped<IEventRepository, EventRepository>();
        builder.Services.AddScoped<EventService>();
        builder.Services.AddScoped<IValidator<CreateEventDto>, CreateEventDtoValidator>();
        builder.Services.AddScoped<IEventRegistrationRepository, EventRegistrationRepository>();
        builder.Services.AddScoped<EventRegistrationService>();
        builder.Services.AddScoped<IValidator<CreateEventRegistrationDto>, CreateEventRegistrationDtoValidator>();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("ReactAppPolicy",
                builder => builder
                    .WithOrigins("http://localhost:4000") // Your React app URL
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });
        // In your .NET backend (Program.cs or Startup.cs)
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        // Use the CORS policy

        // הוספת קונטרולרים ו-Swagger
        builder.Services.AddControllers();
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
        app.UseCors("ReactAppPolicy");
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
