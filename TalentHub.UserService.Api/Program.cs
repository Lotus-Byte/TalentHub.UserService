using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TalentHub.UserService.Api.Configurations;
using TalentHub.UserService.Api.Extensions;
using TalentHub.UserService.Application.Interfaces;
using TalentHub.UserService.Application.Services;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Data;
using TalentHub.UserService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddOptions<ApplicationConfiguration>()
    .BindConfiguration(nameof(ApplicationConfiguration));

builder.Services.AddDbContext<UserDbContext>((sp, options) =>
{
    var settings = sp.GetRequiredService<IOptions<ApplicationConfiguration>>();
    options.EnableSensitiveDataLogging();
    options.UseNpgsql(settings.Value.ConnectionString);
});

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IEmployerRepository, EmployerRepository>();
builder.Services.AddScoped<IUserSettingsRepository, UserSettingsRepository>();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IEmployerService, EmployerService>();
builder.Services.AddScoped<IUserSettingsService, UserSettingsService>();

builder.Services.RegisterMapper();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Run();