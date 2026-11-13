using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace api.models;

public partial class KlinikaContext : DbContext
{
    public KlinikaContext()
    {
    }

    public KlinikaContext(DbContextOptions<KlinikaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Diagnoz> Diagnozs { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<SpisokNaznacheni> SpisokNaznachenis { get; set; }

    public virtual DbSet<SpisokUslug> SpisokUslugs { get; set; }

    public virtual DbSet<Vrach> Vraches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var cs = "Server=db;Port=3306;Database=vet_klinika2;User=root;Password=123456";
        options.UseMySql(
            cs,
            ServerVersion.AutoDetect(cs),
            mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Diagnoz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("diagnoz");

            entity.HasIndex(e => e.PetId, "diagnoz_pets_FK");

            entity.HasIndex(e => e.UslugaId, "diagnoz_spisok_uslugs_FK");

            entity.HasIndex(e => e.VrachId, "diagnoz_vrach_FK");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.PetId).HasColumnName("pet_id");
            entity.Property(e => e.Stadia)
                .HasMaxLength(30)
                .HasColumnName("stadia");
            entity.Property(e => e.UslugaId).HasColumnName("usluga_id");
            entity.Property(e => e.VrachId).HasColumnName("vrach_id");

            entity.HasOne(d => d.Pet).WithMany(p => p.Diagnozs)
                .HasForeignKey(d => d.PetId)
                .HasConstraintName("diagnoz_pets_FK");

            entity.HasOne(d => d.Usluga).WithMany(p => p.Diagnozs)
                .HasForeignKey(d => d.UslugaId)
                .HasConstraintName("diagnoz_spisok_uslugs_FK");

            entity.HasOne(d => d.Vrach).WithMany(p => p.Diagnozs)
                .HasForeignKey(d => d.VrachId)
                .HasConstraintName("diagnoz_vrach_FK");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("owners");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adres)
                .HasMaxLength(100)
                .HasColumnName("adres");
            entity.Property(e => e.Fio)
                .HasMaxLength(40)
                .HasColumnName("fio");
            entity.Property(e => e.Telefon)
                .HasMaxLength(11)
                .HasColumnName("telefon");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pets");

            entity.HasIndex(e => e.OwnerId, "pets_owners_FK");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Klichka)
                .HasMaxLength(30)
                .HasColumnName("klichka");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Poroda)
                .HasMaxLength(20)
                .HasColumnName("poroda");
            entity.Property(e => e.Vid)
                .HasMaxLength(10)
                .HasColumnName("vid");
            entity.Property(e => e.Vozrast).HasColumnName("vozrast");

            entity.HasOne(d => d.Owner).WithMany(p => p.Pets)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pets_owners_FK");
        });

        modelBuilder.Entity<SpisokNaznacheni>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("spisok_naznachenis");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .HasColumnName("name");
        });

        modelBuilder.Entity<SpisokUslug>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("spisok_uslugs");

            entity.HasIndex(e => e.NaznId, "spisok_uslugs_spisok_naznachenis_FK");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cena).HasColumnName("cena");
            entity.Property(e => e.Kolichesto).HasColumnName("kolichesto");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.NaznId).HasColumnName("nazn_id");

            entity.HasOne(d => d.Nazn).WithMany(p => p.SpisokUslugs)
                .HasForeignKey(d => d.NaznId)
                .HasConstraintName("spisok_uslugs_spisok_naznachenis_FK");
        });

        modelBuilder.Entity<Vrach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vrach");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fio)
                .HasMaxLength(20)
                .HasColumnName("fio");
            entity.Property(e => e.Login)
                .HasMaxLength(10)
                .HasColumnName("login");
            entity.Property(e => e.Spec)
                .HasMaxLength(30)
                .HasColumnName("spec");
            entity.Property(e => e.Telefon)
                .HasMaxLength(11)
                .HasColumnName("telefon");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
