using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Eventos
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string rfc = TxtRFC.Text;
            string password = TxtPassword.Text;

            // Obtener la respuesta del reCAPTCHA
            string recaptchaResponse = Request.Form["g-recaptcha-response"];

            // Validar el reCAPTCHA
            if (string.IsNullOrEmpty(recaptchaResponse) || !IsValidCaptcha(recaptchaResponse))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "Swal.fire({ title: 'Error', text: 'Por favor complete el reCAPTCHA.', icon: 'error', confirmButtonText: 'Aceptar' });", true);
                return;
            }

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios WHERE RFC = @RFC", conn);
                    cmd.Parameters.AddWithValue("@RFC", rfc);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string storedPasswordHash = reader["Password"].ToString();
                        string storedSalt = reader["Salt"].ToString();  // Suponiendo que se guarda un 'Salt' en la base de datos

                        // Verificamos la contraseña ingresada con la almacenada
                        if (VerifyPassword(password, storedPasswordHash, storedSalt))
                        {
                            string rol = reader["Rol"].ToString();
                            Session["IdUsuario"] = reader["IdUsuario"];
                            Session["Rol"] = rol;

                            if (rol == "Administrador")
                                Response.Redirect("AdminEventos.aspx");
                            else if (rol == "Participante")
                                Response.Redirect("EventosParticipante.aspx");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Error", "Swal.fire({ title: 'Error', text: 'Credenciales incorrectas', icon: 'error', confirmButtonText: 'Aceptar' });", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error", "Swal.fire({ title: 'Error', text: 'Credenciales incorrectas', icon: 'error', confirmButtonText: 'Aceptar' });", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", $"Swal.fire({{ title: 'Error', text: '{ex.Message}', icon: 'error', confirmButtonText: 'Aceptar' }});", true);
            }
        }

        private bool IsValidCaptcha(string captchaResponse)
        {
            string secretKey = "6Le5QLMqAAAAAFIvPxdItnGiSX9ehWdOq5D6j7al"; // Aquí debes poner tu clave secreta de reCAPTCHA
            string url = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captchaResponse}";

            using (WebClient client = new WebClient())
            {
                var jsonResponse = client.DownloadString(url);
                dynamic json = JsonConvert.DeserializeObject(jsonResponse);
                return json.success == "true"; // Verificamos si el reCAPTCHA fue exitoso
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash, string storedSalt)
        {
            // Primero, generamos el hash de la contraseña ingresada con la misma sal
            string enteredPasswordHash = HashPassword(enteredPassword, storedSalt);

            // Comparamos el hash de la contraseña ingresada con el hash almacenado
            return enteredPasswordHash == storedPasswordHash;
        }

        private string HashPassword(string password, string salt)
        {
            // Combine la contraseña con la sal
            string saltedPassword = password + salt;

            // Utiliza SHA256 para crear un hash
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }
    }
}
