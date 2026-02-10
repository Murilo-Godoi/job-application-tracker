using JobTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Infrastructure;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<JobApplication> Applications => Set<JobApplication>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<FollowUpTask> FollowUps => Set<FollowUpTask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobApplication>(e =>
        {
            e.ToTable("applications");

            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnType("uuid");

            e.Property(x => x.CompanyName)
                .HasColumnType("varchar(200)")
                .IsRequired();

            e.Property(x => x.RoleTitle)
                .HasColumnType("varchar(200)")
                .IsRequired();

            e.Property(x => x.Source)
                .HasColumnType("varchar(100)");

            e.Property(x => x.SalaryEstimate)
                .HasColumnType("float");

            e.Property(x => x.Notes)
                .HasColumnType("text");

            e.Property(x => x.Status)
                .HasColumnType("int")
                .IsRequired();

            e.Property(x => x.AppliedAt)
                .HasColumnType("timestamptz");

            e.Property(x => x.CreatedAt)
                .HasColumnType("timestamptz")
                .IsRequired();

            e.Property(x => x.UpdatedAt)
                .HasColumnType("timestamptz")
                .IsRequired();

            e.HasMany(x => x.Contacts)
                .WithOne(x => x.Application)
                .HasForeignKey(x => x.ApplicationId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasMany(x => x.FollowUps)
                .WithOne(x => x.Application)
                .HasForeignKey(x => x.ApplicationId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(x => x.Status);
            e.HasIndex(x => x.CompanyName);
        });

        modelBuilder.Entity<Contact>(e =>
        {
            e.ToTable("contacts");

            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnType("uuid");

            e.Property(x => x.ApplicationId)
                .HasColumnType("uuid")
                .IsRequired();

            e.Property(x => x.Name)
                .HasColumnType("varchar(200)")
                .IsRequired();

            e.Property(x => x.Title)
                .HasColumnType("varchar(200)");

            e.Property(x => x.Channel)
                .HasColumnType("int");

            e.Property(x => x.Value)
                .HasColumnType("varchar(500)");

            e.Property(x => x.Notes)
                .HasColumnType("text");

            e.HasIndex(x => x.ApplicationId);
        });

        modelBuilder.Entity<FollowUpTask>(e =>
        {
            e.ToTable("followups");

            e.HasKey(x => x.Id);

            e.Property(x => x.Id)
                .HasColumnType("uuid");

            e.Property(x => x.ApplicationId)
                .HasColumnType("uuid")
                .IsRequired();

            e.Property(x => x.DueAt)
                .HasColumnType("timestamptz")
                .IsRequired();

            e.Property(x => x.Type)
                .HasColumnType("int")
                .IsRequired();

            e.Property(x => x.Status)
                .HasColumnType("int")
                .IsRequired();

            e.Property(x => x.DoneAt)
                .HasColumnType("timestamptz");

            e.Property(x => x.Notes)
                .HasColumnType("text");

            e.Property(x => x.CreatedAt)
                .HasColumnType("timestamptz")
                .IsRequired();

            e.HasIndex(x => new { x.Status, x.DueAt });
            e.HasIndex(x => x.ApplicationId);
        });
    }
}
