using EcoTRack_.Areas.Identity.Data;
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
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