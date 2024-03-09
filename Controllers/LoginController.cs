using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;


namespace Hotel.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated) return RedirectToAction("Logged");
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Username, string Password)
        {
            string connString = ConfigurationManager.ConnectionStrings["DbHotel"].ToString();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var command = new SqlCommand("SELECT * FROM Admin WHERE Username = @Username AND Password = @Password", conn);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    FormsAuthentication.SetAuthCookie(reader["Id"].ToString(), true);
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index");
        }




        [Authorize]
        public ActionResult Logged()
        {
            var Id = HttpContext.User.Identity.Name;
            ViewBag.Id = Id;
            return View();
        }

        [Authorize, HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Login");

        }
    }
}