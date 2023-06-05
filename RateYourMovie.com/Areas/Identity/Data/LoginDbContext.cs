using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateYourMovie.Areas.Identity.Data;
using RateYourMovie.Models;

namespace RateYourMovie.Data;

public class LoginDbContext : IdentityDbContext<RateYourMovieUser>
{
    public LoginDbContext(DbContextOptions<LoginDbContext> options)
        : base(options)
    {
    }
    public DbSet<Movie> Movie { get; set; }
    public DbSet<UserComment> Rating { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new RateYourMovieUserEntityConfig());
    }

}

public class RateYourMovieUserEntityConfig : IEntityTypeConfiguration<RateYourMovieUser>
{
    public void Configure(EntityTypeBuilder<RateYourMovieUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(100);
        builder.Property(u => u.LastName).HasMaxLength(100);

    }
}
