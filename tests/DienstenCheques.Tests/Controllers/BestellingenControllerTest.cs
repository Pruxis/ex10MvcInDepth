using System;
using System.Linq;
using DienstenCheques.Controllers;
using DienstenCheques.Models.Domain;
using DienstenCheques.Models.ViewModels.BestellingenViewModels;
using DienstenCheques.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;

namespace DienstenCheques.Tests.Controllers
{
    public class BestellingenControllerTest
    {
        private BestellingenController _controller;
        private Gebruiker _jan;
        private Mock<IGebruikersRepository> _mockGebruikersRepository;
        private NieuwViewModel _model;
        private NieuwViewModel _modelMetFout;

        public BestellingenControllerTest()
        {
            DummyApplicationDbContext context = new DummyApplicationDbContext();
            _mockGebruikersRepository = new Mock<IGebruikersRepository>();
            _jan = context.Jan;
        //    _controller = new BestellingenController(_mockGebruikersRepository.Object);
            _controller.TempData = new Mock<ITempDataDictionary>().Object;
            _model = new NieuwViewModel(9.0M)
             {
                 Elektronisch = true,
                 AantalCheques = 20,
                 DebiteerDatum = DateTime.Today
             };
            _modelMetFout = new NieuwViewModel(9.0M)
            {
                Elektronisch = true,
                AantalCheques = 70,
                DebiteerDatum = DateTime.Today
            };
        }


        #region "Index"

        [Fact]
        public void IndexGiven6MonthsShouldReturnIndexViewModel()
        {
            ViewResult result = _controller.Index(_jan) as ViewResult;
            IndexViewModel indexViewModel = (IndexViewModel) result?.Model;
            Assert.Equal(1, indexViewModel?.AantalBeschikbareCheques);
            Assert.Equal(8, indexViewModel?.AantalOpenstaandePrestatieUren);
            Assert.Equal(1, indexViewModel?.Bestellingen.Count());
            Assert.Equal(DateTime.Today.AddMonths(-4), indexViewModel?.Bestellingen.ToArray()[0].CreatieDatum);
        }

        [Fact]
        public void IndexGiven12MonthsShouldReturnIndexViewModel()
        {
            ViewResult result = _controller.Index(_jan, 12) as ViewResult;
            IndexViewModel indexViewModel = (IndexViewModel) result?.Model;
            Assert.Equal(1, indexViewModel?.AantalBeschikbareCheques);
            Assert.Equal(8, indexViewModel?.AantalOpenstaandePrestatieUren);
            Assert.Equal(3, indexViewModel?.Bestellingen.Count());
            Assert.Equal(DateTime.Today.AddMonths(-12), indexViewModel?.Bestellingen.Last().CreatieDatum);
        }

        #endregion


        #region Nieuw
        [Fact]
        public void NieuwShouldReturnNieuwViewModel()
        {
            ViewResult result = _controller.Nieuw(_jan) as ViewResult;
            NieuwViewModel nieuwViewModel = result?.Model as NieuwViewModel;
            Assert.Equal(20, nieuwViewModel?.AantalCheques);
            Assert.Equal(9.0M, nieuwViewModel?.Zichtwaarde);
            Assert.Equal(DateTime.Today, nieuwViewModel?.DebiteerDatum);
            Assert.True(nieuwViewModel?.Elektronisch);
        }

      #endregion

        #region HttpPost Nieuw
        [Fact]
        public void NieuwPostShouldRedirectToIndexWhenUpdateSuccessfull()
        {
            RedirectToActionResult result = _controller.Nieuw(_jan,_model) as RedirectToActionResult;
            Assert.Equal("Index", result?.ActionName);
        }

        [Fact]
        public void NieuwPostShouldAddBestelling()
        {
            int aantal = _jan.Bestellingen.Count;
            _controller.Nieuw(_jan, _model);
            Assert.Equal(aantal+1, _jan.Bestellingen.Count);
            _mockGebruikersRepository.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact(Skip="Not yet implemented")]
        public void NieuwPostShouldNotAddBestellingWhenNotSuccessfull()
        {

        }

        [Fact(Skip = "Not yet implemented")]
        public void NieuwPostShouldPassNieuwViewModelToViewWhenNotSuccessfull()
        {

        }

        #endregion

     
    }
}
