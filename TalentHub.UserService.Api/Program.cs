using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TalentHub.UserService.Api.Extensions;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Abstractions.DomainEvents;
using TalentHub.UserService.Infrastructure.Data;
using TalentHub.UserService.Infrastructure.EventHandlers;
using TalentHub.UserService.Infrastructure.Models.Notification;
using TalentHub.UserService.Infrastructure.Providers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

builder.Services.RegisterOptions();
builder.Services.RegisterDbContext();
builder.Services.RegisterMapper();
builder.Services.RegisterMassTransitProducer();
builder.Services.RegisterRepositories();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEventHandler<NotificationEvent>, NotificationEventHandler>();
builder.Services.AddScoped<INotificationEventFactory, NotificationEventFactory>();

builder.Services.RegisterServices();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    logger.LogInformation("Applying migrations...");
    dbContext.Database.Migrate();
    logger.LogInformation("Migrations applied successfully.");
}

app.Run();