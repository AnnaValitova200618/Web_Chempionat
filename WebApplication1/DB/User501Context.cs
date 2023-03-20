using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.DB;

public partial class User501Context : DbContext
{
    public User501Context()
    {
    }

    public User501Context(DbContextOptions<User501Context> options)
        : base(options)
    {
    }

    public virtual DbSet<CrossUserRequest> CrossUserRequests { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<GroupVisit> GroupVisits { get; set; }

    public virtual DbSet<PersonalVisit> PersonalVisits { get; set; }

    public virtual DbSet<RejectonReason> RejectonReasons { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Subdivision> Subdivisions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VisitPurpose> VisitPurposes { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=192.168.200.35;database=user50_1;user=user50;password=26643;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_100_CS_AI_SC_UTF8");

        modelBuilder.Entity<CrossUserRequest>(entity =>
        {
            entity.ToTable("CrossUserRequest");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdRequest).HasColumnName("ID_Request");
            entity.Property(e => e.IdUser).HasColumnName("ID_User");

            entity.HasOne(d => d.IdRequestNavigation).WithMany(p => p.CrossUserRequests)
                .HasForeignKey(d => d.IdRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CrossUserRequest_Request");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.CrossUserRequests)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CrossUserRequest_User");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GroupVisit>(entity =>
        {
            entity.ToTable("GroupVisit");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.IdWorker).HasColumnName("ID_Worker");
            entity.Property(e => e.NumberGroup)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.GroupVisits)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupVisit_User");

            entity.HasOne(d => d.IdWorkerNavigation).WithMany(p => p.GroupVisits)
                .HasForeignKey(d => d.IdWorker)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupVisit_Worker");
        });

        modelBuilder.Entity<PersonalVisit>(entity =>
        {
            entity.ToTable("PersonalVisit");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.IdWorker).HasColumnName("ID_Worker");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.PersonalVisits)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonalVisit_User");

            entity.HasOne(d => d.IdWorkerNavigation).WithMany(p => p.PersonalVisits)
                .HasForeignKey(d => d.IdWorker)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonalVisit_Worker");
        });

        modelBuilder.Entity<RejectonReason>(entity =>
        {
            entity.ToTable("RejectonReason");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.ToTable("Request");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateEnd).HasColumnType("date");
            entity.Property(e => e.DateStart).HasColumnType("date");
            entity.Property(e => e.IdRejectionReason).HasColumnName("ID_RejectionReason");
            entity.Property(e => e.IdStatus).HasColumnName("ID_Status");
            entity.Property(e => e.IdVisitPurpose).HasColumnName("ID_VisitPurpose");
            entity.Property(e => e.IdWorker).HasColumnName("ID_Worker");

            entity.HasOne(d => d.IdRejectionReasonNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.IdRejectionReason)
                .HasConstraintName("FK_Request_RejectonReason");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_Status");

            entity.HasOne(d => d.IdVisitPurposeNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.IdVisitPurpose)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_VisitPurpose");

            entity.HasOne(d => d.IdWorkerNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.IdWorker)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_Worker");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subdivision>(entity =>
        {
            entity.ToTable("Subdivision");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NameScan)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Note)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumberPhone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Organization)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PassportSeries)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VisitPurpose>(entity =>
        {
            entity.ToTable("VisitPurpose");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.ToTable("Worker");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdDepartment).HasColumnName("ID_Department");
            entity.Property(e => e.IdSubdivision).HasColumnName("ID_Subdivision");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartmentNavigation).WithMany(p => p.Workers)
                .HasForeignKey(d => d.IdDepartment)
                .HasConstraintName("FK_Worker_Department");

            entity.HasOne(d => d.IdSubdivisionNavigation).WithMany(p => p.Workers)
                .HasForeignKey(d => d.IdSubdivision)
                .HasConstraintName("FK_Worker_Subdivision");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
