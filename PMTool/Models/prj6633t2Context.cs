using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PMTool.Models
{
    public partial class prj6633t2Context : DbContext
    {


        public prj6633t2Context(DbContextOptions<prj6633t2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Effort> Effort { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Requirements> Requirements { get; set; }
        public virtual DbSet<Risks> Risks { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Effort>(entity =>
            {
                entity.ToTable("EFFORT");

                entity.Property(e => e.EffortId).HasColumnName("EffortID");

                entity.Property(e => e.CompleteDate).HasColumnType("date");

                entity.Property(e => e.TaskIdFk).HasColumnName("TaskID_fk");

                entity.HasOne(d => d.TaskIdFkNavigation)
                    .WithMany(p => p.Effort)
                    .HasForeignKey(d => d.TaskIdFk)
                    .HasConstraintName("FK_EFFORT_TASKS");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.HasKey(e => e.ProjId);

                entity.ToTable("PROJECTS");

                entity.Property(e => e.ProjId).HasColumnName("ProjID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrjDueDate).HasColumnType("date");

                entity.Property(e => e.PrjMgr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrjName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Requirements>(entity =>
            {
                entity.HasKey(e => e.ReqId);

                entity.ToTable("REQUIREMENTS");

                entity.Property(e => e.ReqId).HasColumnName("ReqID");

                entity.Property(e => e.CompleteDate).HasColumnType("date");

                entity.Property(e => e.PrjIdFk).HasColumnName("PrjID_fk");

                entity.Property(e => e.ReqDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PrjIdFkNavigation)
                    .WithMany(p => p.Requirements)
                    .HasForeignKey(d => d.PrjIdFk)
                    .HasConstraintName("FK_REQUIREMENTS_PROJECTS");
            });

            modelBuilder.Entity<Risks>(entity =>
            {
                entity.HasKey(e => e.RiskId);

                entity.ToTable("RISKS");

                entity.Property(e => e.RiskId).HasColumnName("RiskID");

                entity.Property(e => e.ProjIdFk).HasColumnName("ProjID_fk");

                entity.Property(e => e.RiskDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProjIdFkNavigation)
                    .WithMany(p => p.Risks)
                    .HasForeignKey(d => d.ProjIdFk)
                    .HasConstraintName("FK_RISKS_PROJECTS");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.HasKey(e => e.TaskId);

                entity.ToTable("TASKS");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.ReqIdFk).HasColumnName("ReqID_fk");

                entity.Property(e => e.TaskDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserIdFk).HasColumnName("UserID_fk");

                entity.HasOne(d => d.ReqIdFkNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ReqIdFk)
                    .HasConstraintName("FK_TASKS_REQUIREMENTS");

                entity.HasOne(d => d.UserIdFkNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.UserIdFk)
                    .HasConstraintName("FK_TASKS_USER");
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.ToTable("TEAMS");

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.PriIdFk).HasColumnName("PriID_fk");

                entity.Property(e => e.RoleIdFk).HasColumnName("RoleID_fk");

                entity.Property(e => e.UserIdFk).HasColumnName("UserID_fk");

                entity.HasOne(d => d.PriIdFkNavigation)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.PriIdFk)
                    .HasConstraintName("FK_TEAMS_PROJECTS");

                entity.HasOne(d => d.RoleIdFkNavigation)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.RoleIdFk)
                    .HasConstraintName("FK_TEAMS_ROLE");

                entity.HasOne(d => d.UserIdFkNavigation)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.UserIdFk)
                    .HasConstraintName("FK_TEAMS_USER");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
