using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DienstenCheques.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DienstenCheques.Data.Repositories {
    public class GebruikersRepository : IGebruikersRepository {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Gebruiker> _gebruikers;

        public GebruikersRepository(ApplicationDbContext context) {
            _context = context;
            _gebruikers = context.Gebruikers;
        }

        public IEnumerable<Gebruiker> GetAll()
        {
            return _gebruikers.Include(g => g.Bestellingen).OrderBy(g => g.Naam).ToList();
        }

        public Gebruiker GetBy(int gebruikersNummer)
        {
            return _gebruikers.Include(g => g.Bestellingen).SingleOrDefault(g => g.GebruikersNummer == gebruikersNummer);
        }

        public Gebruiker GetByEmail(string email)
        {
            return _gebruikers.Include(g => g.Bestellingen).SingleOrDefault(g => g.Email == email);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
