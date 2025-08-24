namespace SBMS.Persistence.MySQL.Entities;

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

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Boy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("boys");

            entity.HasIndex(e => e.Dni, "Dni_UNIQUE").IsUnique();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("buses");

            entity.HasIndex(e => e.Plate, "Patente_UNIQUE").IsUnique();

            entity.Property(e => e.Brand).HasMaxLength(45);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Plate)
                .IsRequired()
                .HasMaxLength(9);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasMany(d => d.Boys).WithMany(p => p.Buses)
                .UsingEntity<Dictionary<string, object>>(
                    "BusesBoy",
                    r => r.HasOne<Boy>().WithMany()
                        .HasForeignKey("BoysId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Boys_Buses"),
                    l => l.HasOne<Bus>().WithMany()
                        .HasForeignKey("BusId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Buses_Boys"),
                    j =>
                    {
                        j.HasKey("BusId", "BoysId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("buses_boys");
                        j.HasIndex(new[] { "BoysId" }, "FK_Boys_Buses");
                    });

            entity.HasMany(d => d.Drivers).WithMany(p => p.Buses)
                .UsingEntity<Dictionary<string, object>>(
                    "BusesDriver",
                    r => r.HasOne<Driver>().WithMany()
                        .HasForeignKey("DriversId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Drivers_Buses"),
                    l => l.HasOne<Bus>().WithMany()
                        .HasForeignKey("BusId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Buses_Drivers"),
                    j =>
                    {
                        j.HasKey("BusId", "DriversId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("buses_drivers");
                        j.HasIndex(new[] { "DriversId" }, "FK_Drivers_Buses");
                    });
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("drivers");

            entity.HasIndex(e => e.Dni, "Dni_UNIQUE").IsUnique();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Telephone).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("test");

            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}