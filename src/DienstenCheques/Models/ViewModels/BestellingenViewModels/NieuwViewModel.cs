using System;
using System.ComponentModel.DataAnnotations;


namespace DienstenCheques.Models.ViewModels.BestellingenViewModels
{
    public class NieuwViewModel
    {
        public bool Elektronisch { get; set; }

        public int AantalCheques { get; set; }

        public DateTime DebiteerDatum { get; set; }

        public decimal Zichtwaarde { get; set; }
        
        public NieuwViewModel(decimal zichtwaarde)
        {
            DebiteerDatum = DateTime.Today;
            Elektronisch = true;
            AantalCheques = 20;
            Zichtwaarde = zichtwaarde;
        }

        public NieuwViewModel() : this(0)
        {

        }

    }
}
