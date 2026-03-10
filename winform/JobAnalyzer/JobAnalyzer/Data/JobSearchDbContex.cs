using Microsoft.EntityFrameworkCore;
using JobAnalyzer.BLL;
namespace JobAnalyzer.Data;

public partial class JobSearchDbContex : DbContext
{
    public JobSearchDbContex(DbContextOptions<JobSearchDbContex> options)
        : base(options)
    {
    }

    public virtual DbSet<JobObject> JobObjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<JobObject>(entity =>
        {
            entity.HasKey(e => e.id);
            
            entity.Property(e => e.CompanyName).
                HasColumnName("CompanyName").
                HasColumnType("varchar(100)");

            entity.Property(e => e.id).
                HasColumnName("id").
                HasColumnType("varchar(32)");

            entity.Property(e => e.JobRequirements).
                HasColumnName("JobRequirements").
                HasColumnType("varchar(1500)");

            entity.Property(e => e.MissingRequirements).
                HasColumnName("MissingRequirements").
                HasColumnType("varchar(1500)");

            entity.Property(e => e.Coverletter).
                HasColumnName("Coverletter").
                HasColumnType("varchar(2500)");

            entity.Property(e => e.Applied).
                HasColumnName("Applied").
                HasColumnType("tinyint(1)");

            entity.Property(e => e.Matched).
                HasColumnName("Matched").
                HasColumnType("tinyint(1)");

            entity.Property(e => e.CompanyLinkedIn).
                HasColumnName("CompanyLinkedIn").
                HasColumnType("varchar(2500)");

            //entity.Property(e => e.JobPost).
            //    HasColumnName("JobPost").
            //    HasColumnType("varchar(1500)");


            entity.Property(e => e.JobLocation).
                HasColumnName("JobLocation").
                HasColumnType("varchar(1500)");


            entity.Property(e => e.JobType).
                HasColumnName("JobType").
                HasColumnType("varchar(1500)");


            entity.Property(e => e.SalaryRange).
                HasColumnName("SalaryRange").
                HasColumnType("varchar(1500)");

        });

        OnModelCreatingPartial(modelBuilder);


    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
