using EcoTRack_.Areas.Identity.Data;
using EcoTRack_.NewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoTRack_.Areas.Identity.Data;

public class AppDbContext : IdentityDbContext<EcoTrackUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Electrate> Electrates { get; set; }
    public DbSet<Insight> Insights { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        base.OnModelCreating(builder);

        // Apply User to EcoTrackUser relationship
        builder.Entity<User>()
            .HasOne(u => u.EcoTrackUser)
            .WithOne()
            .HasForeignKey<User>(u => u.ID)
            .OnDelete(DeleteBehavior.Cascade);

        // Define relationship for Electrate and User
        builder.Entity<Electrate>()
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.Uid)
            .OnDelete(DeleteBehavior.Cascade);

        // Define relationship for Insight and User
        builder.Entity<Insight>()
            .HasOne(i => i.User)
            .WithMany()
            .HasForeignKey(i => i.Uid)
            .OnDelete(DeleteBehavior.Cascade);


    }
}

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<EcoTrackUser>
{
    public void Configure(EntityTypeBuilder<EcoTrackUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.Type_).HasMaxLength(100);
        builder.Property(x => x.PhoneNumber).HasMaxLength(10);
    }
    
}