using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DienstenCheques.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationUser = DienstenCheques.Models.Domain.ApplicationUser;

namespace DienstenCheques.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Onderneming> Ondernemingen { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Bestelling>(MapBestelling);
            builder.Entity<DienstenCheque>(MapDienstenCheque);
            builder.Entity<Gebruiker>(MapGebruiker);
            builder.Entity<Prestatie>(MapPrestatie);
        }

        private static void MapPrestatie(EntityTypeBuilder<Prestatie> p)
        {
            //Table
            p.ToTable("Prestatie");

            //Relationships
            p.HasOne(t => t.Onderneming)
               .WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
        }

        private static void MapGebruiker(EntityTypeBuilder<Gebruiker> g)
        {
           throw new NotImplementedException();
        }

        private static void MapDienstenCheque(EntityTypeBuilder<DienstenCheque> d)
        {
            //Table
            d.ToTable("DienstenCheque");
            d.HasKey(t => t.DienstenChequeNummer);

            //relationships
            d.HasOne(t => t.Prestatie)
                .WithMany()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }


        private static void MapBestelling(EntityTypeBuilder<Bestelling> b)
        {
            b.ToTable("Bestelling");
        }
    }
}
