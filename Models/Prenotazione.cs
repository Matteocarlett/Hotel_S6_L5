using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class Prenotazione
    {
        public int Id { get; set; }

        [Display(Name = "Data Prenotazione")]
        [DataType(DataType.Date)]
        public DateTime DataPrenotazione { get; set; }

        public int Anno { get; set; }

        [Display(Name = "Check-In")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Check-Out")]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        public decimal Caparra { get; set; }

        public decimal Tariffa { get; set; }

        [Display(Name = "ID Cliente")]
        public int IdCliente { get; set; }

        [Display(Name = "Selezione Pensione")]
        public bool SelezionePensione { get; set; }

        public bool Colazione { get; set; }

        [Display(Name = "Numero Stanza")]
        public int NumeroStanza { get; set; }

    }
}
