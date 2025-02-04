using Microsoft.EntityFrameworkCore;
using TalentHub.UserService.Infrastructure.Models;

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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка таблицы User
        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasKey(u => u.UserId);

        // Настройка таблицы Employer
        modelBuilder.Entity<Employer>()
            .ToTable("Employers")
            .HasBaseType<User>();

        // Настройка таблицы Staff
        modelBuilder.Entity<Staff>()
            .ToTable("Staff")
            .HasBaseType<User>();

        // Настройка таблицы Person
        modelBuilder.Entity<Person>()
            .ToTable("Persons")
            .HasBaseType<User>();

        // Настройка связи один-к-одному между User и UserSettings
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserSettings)
            .WithOne() //us => us.User
            .HasForeignKey<UserSettings>(us => us.UserId);

        // Настройка таблицы UserSettings
        modelBuilder.Entity<UserSettings>()
            .ToTable("UserSettings")
            .HasKey(us => us.UserSettingsId);

        base.OnModelCreating(modelBuilder);
    }
}