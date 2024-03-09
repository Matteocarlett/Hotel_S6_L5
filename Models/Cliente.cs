namespace Hotel.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CodiceFiscale { get; set; }
        public string Città { get; set; }
        public string Provincia { get; set; }
        public string Email { get; set; }
        public int Cellulare { get; set; }
    }
}
