using System;
using DienstenCheques.Models.Domain;
using Xunit;

namespace DienstenCheques.Tests.Models
{

    public class BestellingTest
    {
        [Fact]
        public void NewBestellingShouldCreateBestellingWhenSuccessfull()
        {
            Bestelling b = new Bestelling(10, true, DateTime.Today);
            Assert.Equal(10, b.AantalAangekochteCheques);
            Assert.True(b.Elektronisch);
            Assert.Equal(DateTime.Today, b.CreatieDatum);
        }

        [Fact(Skip="Not yet implemented")]
        public void NewBestellingGivenWrongAantalShouldThrowException()
        {

        }

        [Fact]
        public void NewBestellingGivenWrongDebiteerDatumShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Bestelling(-10, true, DateTime.Today.AddMonths(2)));
        }

        [Fact]
        public void TotaalBedragShouldReturnTheTotal()
        {
            Bestelling b = new Bestelling(10, true, DateTime.Today);
            Assert.Equal(90, b.TotaalBedrag);
        }
    }
}
