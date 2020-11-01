﻿using System;
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
        public virtual DbSet<TrenningData> TrenningData { get; set; }
        public virtual DbSet<TrenningSession> TrenningSession { get; set; }
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
                    .HasConstraintName("FK__Goals__UserId__5629CD9C");
            });

            modelBuilder.Entity<TrenningData>(entity =>
            {
                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.TrenningSession)
                    .WithMany(p => p.TrenningData)
                    .HasForeignKey(d => d.TrenningSessionId)
                    .HasConstraintName("FK__TrenningD__Trenn__534D60F1");
            });

            modelBuilder.Entity<TrenningSession>(entity =>
            {
                entity.Property(e => e.StartingTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TrenningSession)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__TrenningS__UserI__5070F446");
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