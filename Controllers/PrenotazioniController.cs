using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class PrenotazioneController : Controller
    {
        public ActionResult ElencoPrenotazioni()
        {
            List<Prenotazione> prenotazioni = new List<Prenotazione>();

            string connString = ConfigurationManager.ConnectionStrings["DbHotel"].ToString();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var command = new SqlCommand("SELECT * FROM Prenotazione", conn);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var prenotazione = new Prenotazione
                    {
                        Id = (int)reader["Id"],
                        DataPrenotazione = (DateTime)reader["DataPrenotazione"],
                        Anno = (int)reader["Anno"],
                        CheckIn = (DateTime)reader["CheckIn"],
                        CheckOut = (DateTime)reader["CheckOut"],
                        Caparra = (decimal)reader["Caparra"],
                        Tariffa = (decimal)reader["Tariffa"],
                        IdCliente = (int)reader["IdCliente"],
                        SelezionePensione = (bool)reader["SelezionePensione"],
                        Colazione = (bool)reader["Colazione"],
                        NumeroStanza = (int)reader["NumeroStanza"]
                    };
                    prenotazioni.Add(prenotazione);
                }
            }

            return View(prenotazioni);
        }

        public ActionResult AggiungiPrenotazione()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AggiungiPrenotazione(Prenotazione prenotazione)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string connString = ConfigurationManager.ConnectionStrings["DbHotel"].ToString();
                    using (var conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        var command = new SqlCommand(@"
                            INSERT INTO Prenotazione
                            (DataPrenotazione, Anno, CheckIn, CheckOut, Caparra, Tariffa, IdCliente, SelezionePensione, Colazione, NumeroStanza)
                            VALUES (@dataPrenotazione, @anno, @checkIn, @checkOut, @caparra, @tariffa, @idCliente, @selezionePensione, @colazione, @numeroStanza)
                        ", conn);
                        command.Parameters.AddWithValue("@dataPrenotazione", prenotazione.DataPrenotazione);
                        command.Parameters.AddWithValue("@anno", prenotazione.Anno);
                        command.Parameters.AddWithValue("@checkIn", prenotazione.CheckIn);
                        command.Parameters.AddWithValue("@checkOut", prenotazione.CheckOut);
                        command.Parameters.AddWithValue("@caparra", prenotazione.Caparra);
                        command.Parameters.AddWithValue("@tariffa", prenotazione.Tariffa);
                        command.Parameters.AddWithValue("@idCliente", prenotazione.IdCliente);
                        command.Parameters.AddWithValue("@selezionePensione", prenotazione.SelezionePensione);
                        command.Parameters.AddWithValue("@colazione", prenotazione.Colazione);
                        command.Parameters.AddWithValue("@numeroStanza", prenotazione.NumeroStanza);

                        command.ExecuteNonQuery();
                    }

                    return RedirectToAction("ElencoPrenotazioni");
                }
                catch (Exception)
                {
                }
            }

            return View(prenotazione);
        }
    }
}
