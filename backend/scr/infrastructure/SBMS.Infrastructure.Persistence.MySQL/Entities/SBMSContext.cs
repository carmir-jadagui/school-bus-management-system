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

    public virtual DbSet<Boy> Boys { get; set; }

    public virtual DbSet<Bus> Buses { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Boy>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Dni })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("boys");

            entity.HasIndex(e => e.Dni, "Dni_UNIQUE").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(45);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.LastName).HasMaxLength(45);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Plate })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("buses");

            entity.HasIndex(e => e.Plate, "Patente_UNIQUE").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Plate).HasMaxLength(7);
            entity.Property(e => e.Brand).HasMaxLength(45);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

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