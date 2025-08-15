using Microsoft.EntityFrameworkCore;

namespace SBMS.Infrastructure.Persistence.MySQL.Entities;

public partial class SBMSContext : DbContext
{
    public SBMSContext()
    {
    }

    public SBMSContext(DbContextOptions<SBMSContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Test> Tests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("test");

            entity.Property(e => e.Message).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}