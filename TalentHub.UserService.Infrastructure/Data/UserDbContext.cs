using Microsoft.EntityFrameworkCore;
using TalentHub.UserService.Infrastructure.Models.Settings;
using TalentHub.UserService.Infrastructure.Models.Users;

namespace TalentHub.UserService.Infrastructure.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(){}
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Staff> Staff { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employer> Employers { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }
    public DbSet<UserNotificationSettings> UserNotificationSettings { get; set; }
    public DbSet<EmailNotificationSettings> EmailNotificationSettings { get; set; }
    public DbSet<PushNotificationSettings> PushNotificationSettings { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Конфигурация таблиц, связанных с User
        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasKey(u => u.UserId);
        
        modelBuilder.Entity<Employer>()
            .ToTable("Employers")
            .HasBaseType<User>();
        
        modelBuilder.Entity<Staff>()
            .ToTable("Staff")
            .HasBaseType<User>();
        
        modelBuilder.Entity<Person>()
            .ToTable("Persons")
            .HasBaseType<User>();
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserSettings)
            .WithOne(us => us.User)
            .HasForeignKey<UserSettings>(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // Конфигурация таблиц, связанных с UserSettings
        modelBuilder.Entity<UserSettings>()
            .ToTable("UserSettings")
            .HasKey(us => us.UserSettingsId);

        modelBuilder.Entity<UserSettings>()
            .HasOne(us => us.NotificationSettings)
            .WithOne(uns => uns.UserSettings)
            .HasForeignKey<UserNotificationSettings>(uns => uns.UserSettingsId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<UserNotificationSettings>()
            .HasKey(uns => uns.Id);

        modelBuilder.Entity<UserNotificationSettings>()
            .HasOne(uns => uns.Email)
            .WithOne(e => e.UserNotificationSettings)
            .HasForeignKey<EmailNotificationSettings>(e => e.UserNotificationSettingsId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<UserNotificationSettings>()
            .HasOne(uns => uns.Push)
            .WithOne(p => p.UserNotificationSettings)
            .HasForeignKey<PushNotificationSettings>(p => p.UserNotificationSettingsId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<EmailNotificationSettings>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<PushNotificationSettings>()
            .HasKey(p => p.Id);

        base.OnModelCreating(modelBuilder);
    }
}