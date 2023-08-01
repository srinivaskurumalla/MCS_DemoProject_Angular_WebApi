using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MCS_DemoProject_Angular_WebApi.Models
{
    public partial class MCSContext : DbContext
    {
        public MCSContext()
        {
        }

        public MCSContext(DbContextOptions<MCSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClaimsMaster> ClaimsMasters { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=MCSConString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClaimsMaster>(entity =>
            {
                entity.HasKey(e => e.ClaimId)
                    .HasName("PK__CLAIMS__A0BE721BAEA18506");

                entity.ToTable("CLAIMS_MASTER");

                entity.Property(e => e.ClaimId).HasColumnName("CLAIM_ID");

                entity.Property(e => e.ClaimAmount)
                    .HasColumnType("money")
                    .HasColumnName("CLAIM_AMOUNT");

                entity.Property(e => e.ClaimDate)
                    .HasColumnType("date")
                    .HasColumnName("CLAIM_DATE");

                entity.Property(e => e.ClaimName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CLAIM_NAME");

                entity.Property(e => e.ClaimType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CLAIM_TYPE")
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.HasIndex(e => e.Email, "UQ__Temp_USE__161CF7243D7CAB2E")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(64)
                    .HasColumnName("PASSWORD_HASH");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(128)
                    .HasColumnName("PASSWORD_SALT");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
