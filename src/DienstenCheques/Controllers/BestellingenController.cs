using System;
using System.Linq;
using DienstenCheques.Filters;
using DienstenCheques.Models.Domain;
using DienstenCheques.Models.ViewModels.BestellingenViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DienstenCheques.Controllers
{
    [ServiceFilter(typeof(GebruikerFilter))]
    public class BestellingenController : Controller
    {

        private readonly IGebruikersRepository _gebruikersRepository;

        public BestellingenController(IGebruikersRepository gebruikersRepository)
        {
            _gebruikersRepository = gebruikersRepository;
        }

        public ActionResult Index(Gebruiker gebruiker, int aantalMaanden=6)
        {
            throw new NotImplementedException();
        }

        public ActionResult Nieuw(Gebruiker gebruiker)
        {
            NieuwViewModel vm = new NieuwViewModel(Bestelling.Bedragcheque);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Nieuw(Gebruiker gebruiker, NieuwViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
