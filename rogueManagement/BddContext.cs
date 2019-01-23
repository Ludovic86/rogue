
using System;
using System.Collections.Generic;
using System.Configuration;
//using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using rogue.models;

namespace rogueManagement
{
    public class BddContext : DbContext
    {
        public BddContext()
        {
        }

        //public BddContext(DbContextOptions<BddContext> options)
        //    : base(options)
        //{
        //}

        public virtual DbSet<Appartient> Appartient { get; set; }
        public virtual DbSet<Donjon> Donjon { get; set; }
        public virtual DbSet<Ennemi> Ennemi { get; set; }
        public virtual DbSet<EstDe> EstDe { get; set; }
        public virtual DbSet<GagnerObjet> GagnerObjet { get; set; }
        public virtual DbSet<HistoEnnemi> HistoEnnemi { get; set; }
        public virtual DbSet<Historique> Historique { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Joueur> Joueur { get; set; }
        public virtual DbSet<Participe> Participe { get; set; }
        public virtual DbSet<Partie> Partie { get; set; }
        public virtual DbSet<Personnage> Personnage { get; set; }
        public virtual DbSet<Salle> Salle { get; set; }
        public virtual DbSet<TypeItem> TypeItem { get; set; }
        public virtual DbSet<EffetItem> EffetItems { get; set; }
        public virtual DbSet<ValeurItem> ValeurItems { get; set; }

        // Unable to generate entity type for table 'dbo.EFFET_ITEM'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.VALEUR'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["BddContext"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appartient>(entity =>
            {
                entity.HasKey(e => new { e.IdDonjon, e.IdSalle, e.IdItem });

                entity.ToTable("APPARTIENT");

                entity.HasIndex(e => e.IdDonjon)
                    .HasName("APPARTIENT_FK");

                entity.HasIndex(e => e.IdItem)
                    .HasName("APPARTIENT3_FK");

                entity.HasIndex(e => e.IdSalle)
                    .HasName("APPARTIENT2_FK");

                entity.Property(e => e.IdDonjon).HasColumnName("ID_DONJON");

                entity.Property(e => e.IdSalle).HasColumnName("ID_SALLE");

                entity.Property(e => e.IdItem).HasColumnName("ID_ITEM");

                entity.Property(e => e.Etage).HasColumnName("ETAGE");

                entity.Property(e => e.NumeroSalle).HasColumnName("NUMERO_SALLE");

                entity.HasOne(d => d.IdDonjonNavigation)
                    .WithMany(p => p.Appartient)
                    .HasForeignKey(d => d.IdDonjon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APPARTIE_APPARTIEN_DONJON");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.Appartient)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APPARTIE_APPARTIEN_ITEM");

                entity.HasOne(d => d.IdSalleNavigation)
                    .WithMany(p => p.Appartient)
                    .HasForeignKey(d => d.IdSalle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APPARTIE_APPARTIEN_SALLE");
            });

            modelBuilder.Entity<Donjon>(entity =>
            {
                entity.HasKey(e => e.IdDonjon)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("DONJON");

                entity.Property(e => e.IdDonjon)
                    .HasColumnName("ID_DONJON")
                    .ValueGeneratedNever();

                entity.Property(e => e.ImageDonjon)
                    .HasColumnName("IMAGE_DONJON")
                    .HasColumnType("image");

                entity.Property(e => e.NomDonjon)
                    .HasColumnName("NOM_DONJON")
                    .HasMaxLength(1024)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ennemi>(entity =>
            {
                entity.HasKey(e => e.IdEnnemi)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ENNEMI");

                entity.Property(e => e.IdEnnemi)
                    .HasColumnName("ID_ENNEMI")
                    .ValueGeneratedNever();

                entity.Property(e => e.AtkEnnemi).HasColumnName("ATK_ENNEMI");

                entity.Property(e => e.Isboss).HasColumnName("ISBOSS");

                entity.Property(e => e.NomEnemi)
                    .HasColumnName("NOM_ENEMI")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.PvEnnemi).HasColumnName("PV_ENNEMI");

                entity.Property(e => e.SpeedEnnemi).HasColumnName("SPEED_ENNEMI");
            });

            modelBuilder.Entity<EstDe>(entity =>
            {
                entity.HasKey(e => new { e.IdItem, e.IdType });

                entity.ToTable("EST_DE");

                entity.HasIndex(e => e.IdItem)
                    .HasName("EST_DE_FK");

                entity.HasIndex(e => e.IdType)
                    .HasName("EST_DE2_FK");

                entity.Property(e => e.IdItem).HasColumnName("ID_ITEM");

                entity.Property(e => e.IdType).HasColumnName("ID_TYPE");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.EstDe)
                    .HasForeignKey(d => d.IdItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EST_DE_EST_DE_ITEM");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.EstDe)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EST_DE_EST_DE2_TYPE_ITE");
            });

            modelBuilder.Entity<GagnerObjet>(entity =>
            {
                entity.HasKey(e => new { e.IdPartie, e.IdSalle, e.IdItem });

                entity.ToTable("GAGNER_OBJET");

                entity.HasIndex(e => e.IdItem)
                    .HasName("GAGNER_OBJET3_FK");

                entity.HasIndex(e => e.IdPartie)
                    .HasName("GAGNER_OBJET_FK");

                entity.HasIndex(e => e.IdSalle)
                    .HasName("GAGNER_OBJET2_FK");

                entity.Property(e => e.IdPartie).HasColumnName("ID_PARTIE");

                entity.Property(e => e.IdSalle).HasColumnName("ID_SALLE");

                entity.Property(e => e.IdItem).HasColumnName("ID_ITEM");

                entity.HasOne(d => d.IdPartieNavigation)
                    .WithMany(p => p.GagnerObjet)
                    .HasForeignKey(d => d.IdPartie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GAGNER_O_GAGNER_OB_PARTIE");

                entity.HasOne(d => d.IdSalleNavigation)
                    .WithMany(p => p.GagnerObjet)
                    .HasForeignKey(d => d.IdSalle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GAGNER_O_GAGNER_OB_SALLE");
            });

            modelBuilder.Entity<HistoEnnemi>(entity =>
            {
                entity.HasKey(e => new { e.IdSalle, e.IdEnnemi, e.IdPartie });

                entity.ToTable("HISTO_ENNEMI");

                entity.HasIndex(e => e.IdEnnemi)
                    .HasName("HISTO_ENNEMI2_FK");

                entity.HasIndex(e => e.IdPartie)
                    .HasName("HISTO_ENNEMI3_FK");

                entity.HasIndex(e => e.IdSalle)
                    .HasName("HISTO_ENNEMI_FK");

                entity.Property(e => e.IdSalle).HasColumnName("ID_SALLE");

                entity.Property(e => e.IdEnnemi).HasColumnName("ID_ENNEMI");

                entity.Property(e => e.IdPartie).HasColumnName("ID_PARTIE");

                entity.HasOne(d => d.IdEnnemiNavigation)
                    .WithMany(p => p.HistoEnnemi)
                    .HasForeignKey(d => d.IdEnnemi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HISTO_EN_HISTO_ENN_ENNEMI");

                entity.HasOne(d => d.IdPartieNavigation)
                    .WithMany(p => p.HistoEnnemi)
                    .HasForeignKey(d => d.IdPartie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HISTO_EN_HISTO_ENN_PARTIE");

                entity.HasOne(d => d.IdSalleNavigation)
                    .WithMany(p => p.HistoEnnemi)
                    .HasForeignKey(d => d.IdSalle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HISTO_EN_HISTO_ENN_SALLE");
            });

            modelBuilder.Entity<Historique>(entity =>
            {
                entity.HasKey(e => new { e.IdPartie, e.IdSalle });

                entity.ToTable("HISTORIQUE");

                entity.HasIndex(e => e.IdPartie)
                    .HasName("HISTORIQUE_FK");

                entity.HasIndex(e => e.IdSalle)
                    .HasName("HISTORIQUE2_FK");

                entity.Property(e => e.IdPartie).HasColumnName("ID_PARTIE");

                entity.Property(e => e.IdSalle).HasColumnName("ID_SALLE");

                entity.Property(e => e.Ordre).HasColumnName("ORDRE");

                entity.HasOne(d => d.IdPartieNavigation)
                    .WithMany(p => p.Historique)
                    .HasForeignKey(d => d.IdPartie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HISTORIQ_HISTORIQU_PARTIE");

                entity.HasOne(d => d.IdSalleNavigation)
                    .WithMany(p => p.Historique)
                    .HasForeignKey(d => d.IdSalle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HISTORIQ_HISTORIQU_SALLE");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.IdItem)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("ITEM");

                entity.Property(e => e.IdItem)
                    .HasColumnName("ID_ITEM")
                    .ValueGeneratedNever();

                entity.Property(e => e.NomItem)
                    .HasColumnName("NOM_ITEM")
                    .HasMaxLength(1024)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Joueur>(entity =>
            {
                entity.HasKey(e => e.IdJoueur)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("JOUEUR");

                entity.Property(e => e.IdJoueur)
                    .HasColumnName("ID_JOUEUR")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.MotDePasse)
                    .HasColumnName("MOT_DE_PASSE")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.NomJoueur)
                    .HasColumnName("NOM_JOUEUR")
                    .HasMaxLength(1024)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Participe>(entity =>
            {
                entity.HasKey(e => new { e.IdJoueur, e.IdDonjon, e.IdPersonnage, e.IdPartie });

                entity.ToTable("PARTICIPE");

                entity.HasIndex(e => e.IdDonjon)
                    .HasName("PARTICIPE2_FK");

                entity.HasIndex(e => e.IdJoueur)
                    .HasName("PARTICIPE_FK");

                entity.HasIndex(e => e.IdPartie)
                    .HasName("PARTICIPE4_FK");

                entity.HasIndex(e => e.IdPersonnage)
                    .HasName("PARTICIPE3_FK");

                entity.Property(e => e.IdJoueur).HasColumnName("ID_JOUEUR");

                entity.Property(e => e.IdDonjon).HasColumnName("ID_DONJON");

                entity.Property(e => e.IdPersonnage).HasColumnName("ID_PERSONNAGE");

                entity.Property(e => e.IdPartie).HasColumnName("ID_PARTIE");

                entity.HasOne(d => d.IdDonjonNavigation)
                    .WithMany(p => p.Participe)
                    .HasForeignKey(d => d.IdDonjon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PARTICIP_PARTICIPE_DONJON");

                entity.HasOne(d => d.IdJoueurNavigation)
                    .WithMany(p => p.Participe)
                    .HasForeignKey(d => d.IdJoueur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PARTICIPE_JOUEUR");

                entity.HasOne(d => d.IdPartieNavigation)
                    .WithMany(p => p.Participe)
                    .HasForeignKey(d => d.IdPartie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PARTICIP_PARTICIPE_PARTIE");

                entity.HasOne(d => d.IdPersonnageNavigation)
                    .WithMany(p => p.Participe)
                    .HasForeignKey(d => d.IdPersonnage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PARTICIP_PARTICIPE_PERSONNA");
            });

            modelBuilder.Entity<Partie>(entity =>
            {
                entity.HasKey(e => e.IdPartie)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("PARTIE");

                entity.Property(e => e.IdPartie)
                    .HasColumnName("ID_PARTIE")
                    .ValueGeneratedNever();

                entity.Property(e => e.EnCours)
                    .HasColumnName("EN_CROURS");
            });

            modelBuilder.Entity<Personnage>(entity =>
            {
                entity.HasKey(e => e.IdPersonnage)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("PERSONNAGE");

                entity.Property(e => e.IdPersonnage)
                    .HasColumnName("ID_PERSONNAGE")
                    .ValueGeneratedNever();

                entity.Property(e => e.AtkPerso).HasColumnName("ATK_PERSO");

                entity.Property(e => e.Classe)
                    .HasColumnName("CLASSE")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.DescriptionPerso)
                    .HasColumnName("DESCRIPTION_PERSO")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.HpPeso).HasColumnName("HP_PESO");

                entity.Property(e => e.NomPersonnage)
                    .HasColumnName("NOM_PERSONNAGE")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.SpeedPerso).HasColumnName("SPEED_PERSO");
            });

            modelBuilder.Entity<Salle>(entity =>
            {
                entity.HasKey(e => e.IdSalle)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("SALLE");

                entity.Property(e => e.IdSalle)
                    .HasColumnName("ID_SALLE")
                    .ValueGeneratedNever();

                entity.Property(e => e.NomSalle)
                    .HasColumnName("NOM_SALLE")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.TexteSalle)
                    .HasColumnName("TEXTE_SALLE")
                    .HasColumnType("text");

                entity.Property(e => e.TypeSalle).HasColumnName("TYPE_SALLE");
            });

            modelBuilder.Entity<TypeItem>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("TYPE_ITEM");

                entity.Property(e => e.IdType)
                    .HasColumnName("ID_TYPE")
                    .ValueGeneratedNever();

                entity.Property(e => e.NomType)
                    .IsRequired()
                    .HasColumnName("NOM_TYPE")
                    .HasMaxLength(1024)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EffetItem>(entity =>
            {
                entity.HasKey(e => e.Iditem)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("EFFET_ITEM");

                entity.Property(e => e.Iditem)
                    .HasColumnName("ID_ITEM")
                    .ValueGeneratedNever();

                entity.Property(e => e.AtkItem)
                    .HasColumnName("ATK_ITEM");

                entity.Property(e => e.SpeedItem)
                    .HasColumnName("SPEED_ITEM");

                entity.Property(e => e.HpItem)
                    .HasColumnName("HP_ITEM");
            });

            modelBuilder.Entity<ValeurItem>(entity =>
            {
                entity.HasKey(e => e.IdItem)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("VALEUR");

                entity.Property(e => e.valeurItem)
                    .HasColumnName("MONTANT")
                    .ValueGeneratedNever();
            });
        }
    }
}