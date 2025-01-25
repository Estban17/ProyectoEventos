using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.UI;

namespace Eventos
{
    public partial class Recuperar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnRecuperar_Click(object sender, EventArgs e)
        {
            string email = TxtCorreo.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MostrarMensaje("Error", "Por favor, ingresa un correo válido.", "error");
                return;
            }

            int userId = ObtenerIdUsuarioPorCorreo(email);
            if (userId > 0)
            {
                string token = GenerarToken();
                GuardarTokenEnBaseDeDatos(userId, token);

                if (EnviarCorreoConToken(email, token))
                {
                    MostrarMensaje("¡Éxito!", "Revisa tu correo para continuar con el restablecimiento de tu contraseña.", "success");
                }
                else
                {
                    MostrarMensaje("Error", "Ocurrió un problema al enviar el correo. Inténtalo de nuevo.", "error");
                }
            }
            else
            {
                MostrarMensaje("Error", "El correo no está registrado.", "error");
            }
        }

        private int ObtenerIdUsuarioPorCorreo(string email)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                string query = "SELECT IdUsuario FROM Usuarios WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        private string GenerarToken()
        {
            return Guid.NewGuid().ToString();
        }

        private void GuardarTokenEnBaseDeDatos(int userId, string token)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO PasswordResetTokens (IdUsuario, Token, ExpirationDate) VALUES (@IdUsuario, @Token, @ExpirationDate)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", userId);
                    cmd.Parameters.AddWithValue("@Token", token);
                    cmd.Parameters.AddWithValue("@ExpirationDate", DateTime.Now.AddMinutes(10));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private bool EnviarCorreoConToken(string email, string token)
        {
            try
            {
                string resetUrl = $"https://localhost:44319/Reestablecer.aspx?token={token}";
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("servicio.difh@gmail.com", "Eventos DIF Hidalgo"),
                    Subject = "Recuperación de Contraseña",
                    Body = $"Hola,<br/><br/>Recibimos una solicitud para restablecer tu contraseña.<br/>" +
                           $"Haz clic en el siguiente enlace para continuar:<br/><br/>" +
                           $"<a href='{resetUrl}'>Restablecer Contraseña</a><br/><br/>" +
                           "Si no solicitaste este cambio, puedes ignorar este mensaje.<br/><br/>" +
                           "Atentamente,<br/>Eventos DIF Hidalgo.",
                    IsBodyHtml = true
                };

                mail.To.Add(email);

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential("servicio.difh@gmail.com", "adwn vypo wcpy vcyb"),
                    EnableSsl = true
                };

                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                // Log del error
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void MostrarMensaje(string titulo, string mensaje, string tipo)
        {
            string script = $"Swal.fire('{titulo}', '{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
        }
    }
}
