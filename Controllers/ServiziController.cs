using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    public class ServiziController : Controller
    {
        
        public ActionResult Index()
        {
            {
                string connString = ConfigurationManager.ConnectionStrings["DbHotel"].ToString();
                var conn = new SqlConnection(connString);
                conn.Open();
                var command = new SqlCommand(@"
                SELECT *
                FROM ServiziAggiuntivi"
                , conn);
                var reader = command.ExecuteReader();

                List<Servizi> servizi = new List<Servizi>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var servizio = new Servizi();
                        servizio.Id = (int)reader["Id"];
                        servizio.IdPrenotazione = (int)reader["IdPrenotazione"];
                        servizio.Servizio = (string)reader["Servizio"];
                        servizio.Data = (DateTime)reader["Data"];
                        servizio.Quantità = (int)reader["Quantità"];
                        servizio.Prezzo = (decimal)reader["Prezzo"];
                        servizi.Add(servizio);
                    }
                }
                return View(servizi);
            }
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "IdPrenotazione, Servizio, Data, Quantità, Prezzo")] Servizi servizi)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["DbHotel"].ToString();
                var conn = new SqlConnection(connString);
                conn.Open();
                var command = new SqlCommand(@"
                    INSERT INTO ServiziAggiuntivi
                    (IdPrenotazione, Servizio, Data, Quantità, Prezzo)
                    OUTPUT INSERTED.Id
                    VALUES (@IdPrenotazione, @Servizio, @Data, @Quantità, @Prezzo)
                ", conn);

                command.Parameters.AddWithValue("@Servizio", servizi.Servizio);
                command.Parameters.AddWithValue("@IdPrenotazione", servizi.IdPrenotazione);
                command.Parameters.AddWithValue("@Data", servizi.Data);
                command.Parameters.AddWithValue("@Quantità", servizi.Quantità);
                command.Parameters.AddWithValue("@Prezzo", servizi.Prezzo);

                var serviziId = command.ExecuteScalar();

                return RedirectToAction("Index", "Servizi", new { id = serviziId });
            }

            return View(servizi);
        }
    }
}