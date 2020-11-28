using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Common.DAL.Models;

namespace Common.DAL.Context
{
    public partial class dbContext : DbContext
    {
        public dbContext()
        {
        }

        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Goals> Goals { get; set; }
        public virtual DbSet<Met> Met { get; set; }
        public virtual DbSet<Sport> Sport { get; set; }
        public virtual DbSet<TrainingData> TrainingData { get; set; }
        public virtual DbSet<TrainingSession> TrainingSession { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-5U8O6SF\\SQLEXPRESS;Database=db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goals>(entity =>
            {
                entity.Property(e => e.FinishTime).HasColumnType("datetime");

                entity.Property(e => e.StartingTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Goals__UserId__70DDC3D8");
            });

            modelBuilder.Entity<Met>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Met)
                    .HasForeignKey(d => d.SportId)
                    .HasConstraintName("FK__Met__SportId__6754599E");
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TrainingData>(entity =>
            {
                entity.HasOne(d => d.TrainingSession)
                    .WithMany(p => p.TrainingData)
                    .HasForeignKey(d => d.TrainingSessionId)
                    .HasConstraintName("FK__TrainingD__Train__06CD04F7");
            });

            modelBuilder.Entity<TrainingSession>(entity =>
            {
                entity.Property(e => e.StartingTime).HasColumnType("datetime");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.TrainingSession)
                    .HasForeignKey(d => d.SportId)
                    .HasConstraintName("FK__TrainingS__Sport__03F0984C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TrainingSession)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__TrainingS__UserI__02FC7413");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HarrisBenedictBmr).HasColumnName("HarrisBenedictBMR");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
