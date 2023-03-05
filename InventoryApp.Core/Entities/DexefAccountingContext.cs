using System;
using System.Collections.Generic;
using InventoryApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace InventoryApp.Core.Entities
{
    public partial class DexefAccountingContext : DbContext
    {
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<TableName> TableNames { get; set; } = null!;
        public virtual DbSet<TranslationDetail> TranslationDetails { get; set; } = null!;
        public virtual DbSet<TranslationHeader> TranslationHeaders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; }

        public DexefAccountingContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data source=.;initial catalog=DexefERP;Integrated security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language", "Lang");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TableName>(entity =>
            {
                entity.ToTable("TableName", "Lang");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TranslationDetail>(entity =>
            {
                entity.ToTable("TranslationDetail", "Lang");

                entity.Property(e => e.FieldName).HasMaxLength(50);

                entity.Property(e => e.Translation).HasMaxLength(500);

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.TranslationDetails)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TranslationDetail_Language");

                entity.HasOne(d => d.TranslationHeader)
                    .WithMany(p => p.TranslationDetails)
                    .HasForeignKey(d => d.TranslationHeaderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TranslationDetail_TranslationHeader");
            });

            modelBuilder.Entity<TranslationHeader>(entity =>
            {
                entity.ToTable("TranslationHeader", "Lang");

                entity.HasOne(d => d.TableName)
                    .WithMany(p => p.TranslationHeaders)
                    .HasForeignKey(d => d.TableNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TranslationHeader_TableName");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18,2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
