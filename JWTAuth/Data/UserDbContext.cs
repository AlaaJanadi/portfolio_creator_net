using JWTAuth.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Data;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<PersonalInfo> PersonalInfos { get; set; }
    
    public DbSet<Education> Educations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Portfolio)
            .WithOne(p => p.User)
            .HasForeignKey<Portfolio>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Portfolio>()
            .HasOne(pf => pf.PersonalInfo)
            .WithOne(pi => pi.Portfolio)
            .HasForeignKey<PersonalInfo>(pi => pi.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Portfolio>()
            .HasMany(p => p.Educations)
            .WithOne(e => e.Portfolio)
            .HasForeignKey(e => e.PortfolioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}