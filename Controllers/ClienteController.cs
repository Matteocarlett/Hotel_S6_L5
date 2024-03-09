using Hotel.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System;

public class ClienteController : Controller
{
    public ActionResult ElencoClienti()
    {
        string connString = ConfigurationManager.ConnectionStrings["DbHotel"].ToString();
        using (var conn = new SqlConnection(connString))
        {
            conn.Open();
            var command = new SqlCommand("SELECT * FROM Customers", conn);
            var reader = command.ExecuteReader();

            var clienti = new List<Cliente>();
            while (reader.Read())
            {
                var cliente = new Cliente
                {
                    Id = (int)reader["Id"],
                    Nome = (string)reader["Nome"],
                    Cognome = (string)reader["Cognome"],
                    CodiceFiscale = (string)reader["CodiceFiscale"],
                    Città = (string)reader["Città"],
                    Provincia = (string)reader["Provincia"],
                    Email = (string)reader["Email"],
                    Cellulare = (int)reader["Cellulare"]
                };
                clienti.Add(cliente);
            }
            return View(clienti);
        }
    }

    public ActionResult AggiungiCliente()
    {
        return View();
    }

    [HttpPost]
    public ActionResult AggiungiCliente(Cliente cliente)
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
                        INSERT INTO Customers
                        (Nome, Cognome, CodiceFiscale, Città, Provincia, Email, Cellulare)
                        VALUES (@nome, @cognome, @codiceFiscale, @città, @provincia, @email, @cellulare)
                    ", conn);
                    command.Parameters.AddWithValue("@nome", cliente.Nome);
                    command.Parameters.AddWithValue("@cognome", cliente.Cognome);
                    command.Parameters.AddWithValue("@codiceFiscale", cliente.CodiceFiscale);
                    command.Parameters.AddWithValue("@città", cliente.Città);
                    command.Parameters.AddWithValue("@provincia", cliente.Provincia);
                    command.Parameters.AddWithValue("@email", cliente.Email);
                    command.Parameters.AddWithValue("@cellulare", cliente.Cellulare);

                    command.ExecuteNonQuery();
                }

                return RedirectToAction("ElencoClienti");
            }
            catch (Exception)
            {

            }
        }

        return View(cliente);
    }
}
