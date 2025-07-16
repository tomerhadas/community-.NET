using Microsoft.EntityFrameworkCore;
using CommunityEventHub.Data;
using CommunityEventHub.DAL;
using CommunityEventHub.Services;
using CommunityEventHub.Models.Dto;
using CommunityEventHub.Mapper;
using CommunityEventHub.Validators;

using FluentValidation;

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

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
