using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProblemSolvingPlatform.Models;

public partial class ProblemSolvingPlatformContext : DbContext
{
    public ProblemSolvingPlatformContext()
    {
    }

    public ProblemSolvingPlatformContext(DbContextOptions<ProblemSolvingPlatformContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Classement> Classements { get; set; }
    public virtual DbSet<Commentaire> Commentaires { get; set; }
    public virtual DbSet<Probleme> Problemes { get; set; }
    public virtual DbSet<Soumission> Soumissions { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Classement>(entity =>
        {
            entity.HasKey(e => e.ClassementId).HasName("PK__Classeme__63F085FDB34D3832");
            entity.ToTable("Classement");
            entity.HasIndex(e => e.UserId, "UQ__Classeme__1788CC4DB87541A0").IsUnique();
            entity.Property(e => e.Rang).HasColumnName("rang");
            entity.Property(e => e.Score)
                .HasMaxLength(50)
                .HasColumnName("score");
            entity.HasOne(d => d.User).WithOne(p => p.Classement)
                .HasForeignKey<Classement>(d => d.UserId)
                .HasConstraintName("FK_classement_User");
        });

        modelBuilder.Entity<Commentaire>(entity =>
        {
            entity.HasKey(e => e.CommentaireId).HasName("PK__Commenta__4717D05A161A40CF");
            entity.ToTable("Commentaire");
            entity.Property(e => e.Contenu)
                .HasMaxLength(255)
                .HasColumnName("contenu");
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Probleme)
                .HasMaxLength(100)
                .HasColumnName("probleme");
            entity.HasOne(d => d.User).WithMany(p => p.Commentaires)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_commentaire_User");
        });

        modelBuilder.Entity<Probleme>(entity =>
        {
            entity.HasKey(e => e.ProbId).HasName("PK__Probleme__078036D7C651FF26");
            entity.ToTable("Probleme");
            entity.Property(e => e.Descr)
                .HasMaxLength(255)
                .HasColumnName("descr");
            entity.Property(e => e.Difficulte)
                .HasMaxLength(50)
                .HasColumnName("difficulte");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Soumission>(entity =>
        {
            entity.HasKey(e => e.SoumissionId).HasName("PK__Soumissi__8749F9FFA169F861");
            entity.ToTable("Soumission");
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .HasColumnName("code");
            entity.Property(e => e.Langage)
                .HasMaxLength(50)
                .HasColumnName("langage");
            entity.Property(e => e.Probleme)
                .HasMaxLength(255)
                .HasColumnName("probleme");
            entity.HasOne(d => d.User).WithMany(p => p.Soumissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_sommision_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C9A3C4F25");
            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053420F85CE3").IsUnique();
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.ProfilePicture).HasMaxLength(255);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}