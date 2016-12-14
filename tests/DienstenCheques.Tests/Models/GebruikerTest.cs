using System;
using DienstenCheques.Tests.Data;
using Xunit;

namespace DienstenCheques.Tests.Models
{

    public class GebruikerTest
    {
        DummyApplicationDbContext _context;

        public  GebruikerTest()
        {
            _context = new DummyApplicationDbContext();
        }

        [Fact]
        public void AddBestellingShouldAddBestellingWhenSuccessfull()
        {
            _context.Jan.AddBestelling(20, true, DateTime.Today);
            Assert.Equal(4, _context.Jan.Bestellingen.Count);
        }

        [Fact]
        public void AddBestellingShouldAddDienstenChequesToPortefeuille()
        {
            int aantal = _context.Jan.Portefeuille.Count;
            _context.Jan.AddBestelling(20, true, DateTime.Today);
            Assert.Equal(aantal+20,_context.Jan.Portefeuille.Count);
        }

        [Fact]
        public void AddBestellingShouldUpdateOpenstaandePrestaties()
        {
            _context.Jan.AddBestelling(20, true, DateTime.Today);
            Assert.Equal(0, _context.Jan.AantalOpenstaandePrestatieUren);
        }

        [Fact]
        public void AddBestellingShouldUpdateBeschikbareDienstenCheques()
        {
            _context.Jan.AddBestelling(20, true, DateTime.Today);
            Assert.Equal(13, _context.Jan.AantalBeschikbareElektronischeCheques);
        }
    }
}
