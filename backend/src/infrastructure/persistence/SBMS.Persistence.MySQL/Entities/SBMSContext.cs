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

    public virtual DbSet<BusesBoy> BusesBoys { get; set; }

    public virtual DbSet<BusesDriver> BusesDrivers { get; set; }

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
        });

        modelBuilder.Entity<BusesBoy>(entity =>
        {
            entity.HasKey(e => new { e.BusId, e.BoysId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("buses_boys");

            entity.HasIndex(e => e.BoysId, "BoysId_UNIQUE").IsUnique();

            entity.HasOne(d => d.Boys).WithOne(p => p.BusesBoy)
                .HasForeignKey<BusesBoy>(d => d.BoysId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Boys_Buses");

            entity.HasOne(d => d.Bus).WithMany(p => p.BusesBoys)
                .HasForeignKey(d => d.BusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Buses_Boys");
        });

        modelBuilder.Entity<BusesDriver>(entity =>
        {
            entity.HasKey(e => new { e.BusId, e.DriversId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("buses_drivers");

            entity.HasIndex(e => e.BusId, "BusId_UNIQUE").IsUnique();

            entity.HasIndex(e => e.DriversId, "DriversId_UNIQUE").IsUnique();

            entity.HasOne(d => d.Bus).WithOne(p => p.BusesDriver)
                .HasForeignKey<BusesDriver>(d => d.BusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Buses_Drivers");

            entity.HasOne(d => d.Drivers).WithOne(p => p.BusesDriver)
                .HasForeignKey<BusesDriver>(d => d.DriversId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Drivers_Buses");
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