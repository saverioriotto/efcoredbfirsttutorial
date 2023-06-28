using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTutorial.Models;

public partial class TutorialdbContext : DbContext
{
    public TutorialdbContext()
    {
    }

    public TutorialdbContext(DbContextOptions<TutorialdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Corsi> Corsis { get; set; }

    public virtual DbSet<Professori> Professoris { get; set; }

    public virtual DbSet<Studenti> Studentis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("DataSource=C:\\<PATH DATABASE LOCATION\\tutorialdb.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Corsi>(entity =>
        {
            entity.ToTable("Corsi");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descrizione).HasColumnName("descrizione");
            entity.Property(e => e.Nome).HasColumnName("nome");

            entity.HasMany(d => d.Professores).WithMany(p => p.Corsos)
                .UsingEntity<Dictionary<string, object>>(
                    "CorsiProfessori",
                    r => r.HasOne<Professori>().WithMany()
                        .HasForeignKey("ProfessoreId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Corsi>().WithMany()
                        .HasForeignKey("CorsoId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CorsoId", "ProfessoreId");
                        j.ToTable("CorsiProfessori");
                        j.IndexerProperty<long>("CorsoId").HasColumnName("corso_id");
                        j.IndexerProperty<long>("ProfessoreId").HasColumnName("professore_id");
                    });

            entity.HasMany(d => d.Studentes).WithMany(p => p.Corsos)
                .UsingEntity<Dictionary<string, object>>(
                    "CorsiStudenti",
                    r => r.HasOne<Studenti>().WithMany()
                        .HasForeignKey("StudenteId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Corsi>().WithMany()
                        .HasForeignKey("CorsoId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CorsoId", "StudenteId");
                        j.ToTable("CorsiStudenti");
                        j.IndexerProperty<long>("CorsoId").HasColumnName("corso_id");
                        j.IndexerProperty<long>("StudenteId").HasColumnName("studente_id");
                    });
        });

        modelBuilder.Entity<Professori>(entity =>
        {
            entity.ToTable("Professori");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cognome).HasColumnName("cognome");
            entity.Property(e => e.Nome).HasColumnName("nome");
        });

        modelBuilder.Entity<Studenti>(entity =>
        {
            entity.ToTable("Studenti");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cognome).HasColumnName("cognome");
            entity.Property(e => e.Nome).HasColumnName("nome");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
