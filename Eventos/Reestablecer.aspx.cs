using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace Eventos
{
    public partial class Reestablecer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = Request.QueryString["token"];
                if (string.IsNullOrEmpty(token) || !ValidarToken(token))
                {
                    MostrarMensaje("Error", "El enlace de recuperación es inválido o ha expirado.", "error");
                    BtnReestablecer.Enabled = false;
                }
            }
        }

        private bool ValidarToken(string token)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                string query = "SELECT ExpirationDate FROM PasswordResetTokens WHERE Token = @Token";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Token", token);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        DateTime expirationDate = Convert.ToDateTime(result);
                        return expirationDate > DateTime.Now;
                    }
                }
            }
            return false;
        }

        protected void BtnReestablecer_Click(object sender, EventArgs e)
        {
            string nuevaContrasena = TxtNuevaContrasena.Text.Trim();
            string confirmarContrasena = TxtConfirmarContrasena.Text.Trim();
            string token = Request.QueryString["token"];

            if (string.IsNullOrEmpty(nuevaContrasena) || string.IsNullOrEmpty(confirmarContrasena))
            {
                MostrarMensaje("Error", "Por favor, llena todos los campos.", "error");
                return;
            }

            if (nuevaContrasena != confirmarContrasena)
            {
                MostrarMensaje("Error", "Las contraseñas no coinciden.", "error");
                return;
            }

            int userId = ObtenerIdUsuarioPorToken(token);
            if (userId > 0)
            {
                if (ActualizarContrasena(userId, nuevaContrasena))
                {
                    EliminarToken(token);
                    MostrarMensaje("¡Éxito!", "Tu contraseña se ha actualizado correctamente.", "success");
                }
                else
                {
                    MostrarMensaje("Error", "Ocurrió un problema al actualizar la contraseña.", "error");
                }
            }
            else
            {
                MostrarMensaje("Error", "El token no es válido.", "error");
            }
        }

        private int ObtenerIdUsuarioPorToken(string token)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                string query = "SELECT IdUsuario FROM PasswordResetTokens WHERE Token = @Token";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Token", token);
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        private bool ActualizarContrasena(int userId, string nuevaContrasena)
        {
            string hashedPassword = HashPassword(nuevaContrasena);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                string query = "UPDATE Usuarios SET Password = @Password WHERE IdUsuario = @IdUsuario";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@IdUsuario", userId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        private void EliminarToken(string token)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM PasswordResetTokens WHERE Token = @Token";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Token", token);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void MostrarMensaje(string titulo, string mensaje, string tipo)
        {
            string script = $"Swal.fire('{titulo}', '{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true);
        }
    }
}
