namespace Database;

public partial class ParsethingContext : DbContext
{
    public ParsethingContext() { }

    public virtual DbSet<Law> Laws { get; set; }

    public virtual DbSet<Method> Methods { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Platform> Platforms { get; set; }

    public virtual DbSet<Procurement> Procurements { get; set; }

    public virtual DbSet<TimeZone> TimeZones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _ = optionsBuilder.UseSqlServer("Password=Voice49:dusk;Persist Security Info=True;User ID=denzelcrocker;Initial Catalog=Parsething;Data Source=176.112.98.217, 1433;Trust Server Certificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Procurement>(entity =>
        {
            _ = entity.Property(e => e.Deadline).HasColumnType("datetime");
            _ = entity.Property(e => e.InitialPrice).HasColumnType("decimal(12, 2)");
            _ = entity.Property(e => e.StartDate).HasColumnType("datetime");
            _ = entity.Property(e => e.Warranty)
                .HasMaxLength(10)
                .IsFixedLength();

            _ = entity.HasOne(d => d.Law).WithMany(p => p.Procurements)
                .HasForeignKey(d => d.LawId)
                .HasConstraintName("FK_Procurements_Laws");

            _ = entity.HasOne(d => d.Method).WithMany(p => p.Procurements)
                .HasForeignKey(d => d.MethodId)
                .HasConstraintName("FK_Procurements_Methods");

            _ = entity.HasOne(d => d.Organization).WithMany(p => p.Procurements)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_Procurements_Organizations");

            _ = entity.HasOne(d => d.Platform).WithMany(p => p.Procurements)
                .HasForeignKey(d => d.PlatformId)
                .HasConstraintName("FK_Procurements_Platforms");

            _ = entity.HasOne(d => d.TimeZone).WithMany(p => p.Procurements)
                .HasForeignKey(d => d.TimeZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Procurements_TimeZones");
        });

        _ = modelBuilder.Entity<TimeZone>(entity =>
        {
            _ = entity.Property(e => e.Offset).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}