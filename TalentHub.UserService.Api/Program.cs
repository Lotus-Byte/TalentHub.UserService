using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using TalentHub.UserService.Api.Configurations;
using TalentHub.UserService.Api.Extensions;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.Services;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Abstractions.DomainEvents;
using TalentHub.UserService.Infrastructure.Abstractions.Repositories;
using TalentHub.UserService.Infrastructure.Data;
using TalentHub.UserService.Infrastructure.EventHandlers;
using TalentHub.UserService.Infrastructure.Models.Notification;
using TalentHub.UserService.Infrastructure.Providers;
using TalentHub.UserService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddOptions<ApplicationConfiguration>()
    .BindConfiguration(nameof(ApplicationConfiguration));
builder.Services.AddOptions<RabbitMqConfiguration>()
    .BindConfiguration(nameof(RabbitMqConfiguration));

builder.Services.AddDbContext<UserDbContext>((sp, options) =>
{
    var settings = sp.GetRequiredService<IOptions<ApplicationConfiguration>>();
    options.EnableSensitiveDataLogging();
    options.UseNpgsql(settings.Value.ConnectionString);
});

builder.Services.RegisterMapper();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
         var configuration = context.GetService<IOptions<RabbitMqConfiguration>>()
                     ?? throw new ConfigurationException($"Lack of '{nameof(RabbitMqConfiguration)}' settings");

         var rabbitMqConfiguration = configuration.Value;
         
        cfg.Host(rabbitMqConfiguration.Host, rabbitMqConfiguration.VirtualHost, h =>
        {
            h.Username(rabbitMqConfiguration.Username);
            h.Password(rabbitMqConfiguration.Password);
        });
        
        cfg.Message<NotificationEvent>(ct => 
            ct.SetEntityName("notification_event"));
        
        cfg.Publish<NotificationEvent>(p =>
        {
            p.ExchangeType = ExchangeType.Direct;
        });
        
        cfg.Send<NotificationEvent>(s => 
            s.UseRoutingKeyFormatter(busCtx =>
                rabbitMqConfiguration.QueueName));
    });
});

builder.Services.AddScoped<IEmployerRepository, EmployerRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IUserSettingsRepository, UserSettingsRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEventHandler<NotificationEvent>, NotificationEventHandler>();
builder.Services.AddScoped<INotificationEventFactory, NotificationEventFactory>();

builder.Services.AddScoped<IEmployerService, EmployerService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IUserSettingsService, UserSettingsService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    logger.LogInformation("Applying migrations...");
    dbContext.Database.Migrate();
    logger.LogInformation("Migrations applied successfully.");
}

app.UseRouting();
app.MapControllers();

app.Run();