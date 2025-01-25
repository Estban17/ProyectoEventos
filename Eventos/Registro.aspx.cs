using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace Eventos
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnRegistro_Click(object sender, EventArgs e)
        {
            // Verificar que todos los campos estén llenos
            if (string.IsNullOrWhiteSpace(TxtNombre.Text) ||
                string.IsNullOrWhiteSpace(TxtPaterno.Text) ||
                string.IsNullOrWhiteSpace(TxtMaterno.Text) ||
                string.IsNullOrWhiteSpace(TxtRFC.Text) ||
                string.IsNullOrWhiteSpace(TxtEmail.Text) ||
                string.IsNullOrWhiteSpace(TxtNumCelular.Text) ||
                string.IsNullOrWhiteSpace(TxtPassword.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "CamposVacios",
                    "Swal.fire({ title: 'Error', text: 'Todos los campos son obligatorios.', icon: 'error', confirmButtonText: 'Aceptar' });", true);
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    // Verificar si el RFC ya existe
                    SqlCommand checkRFC = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE RFC = @RFC", conn);
                    checkRFC.Parameters.AddWithValue("@RFC", TxtRFC.Text);
                    int rfcCount = (int)checkRFC.ExecuteScalar();

                    if (rfcCount > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error",
                            "Swal.fire({ title: 'Error', text: 'El RFC ya está registrado.', icon: 'error', confirmButtonText: 'Aceptar' });", true);
                        return;
                    }

                    // Verificar si el correo ya existe
                    SqlCommand checkEmail = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE Email = @Email", conn);
                    checkEmail.Parameters.AddWithValue("@Email", TxtEmail.Text);
                    int emailCount = (int)checkEmail.ExecuteScalar();

                    if (emailCount > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error",
                            "Swal.fire({ title: 'Error', text: 'El correo electrónico ya está registrado.', icon: 'error', confirmButtonText: 'Aceptar' });", true);
                        return;
                    }

                    // Verificar si el número de celular ya existe
                    SqlCommand checkPhone = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE NumCelular = @NumCelular", conn);
                    checkPhone.Parameters.AddWithValue("@NumCelular", TxtNumCelular.Text);
                    int phoneCount = (int)checkPhone.ExecuteScalar();

                    if (phoneCount > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Error",
                            "Swal.fire({ title: 'Error', text: 'El número de celular ya está registrado.', icon: 'error', confirmButtonText: 'Aceptar' });", true);
                        return;
                    }

                    // Generar sal (salt) y hash de la contraseña
                    string salt = GenerateSalt();
                    string passwordHash = HashPassword(TxtPassword.Text, salt);

                    // Insertar nuevo usuario
                    SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios (Nombre, Paterno, Materno, RFC, Email, NumCelular, Password, Salt, Rol) VALUES (@Nombre, @Paterno, @Materno, @RFC, @Email, @NumCelular, @Password, @Salt, @Rol)", conn);
                    cmd.Parameters.AddWithValue("@Nombre", TxtNombre.Text);
                    cmd.Parameters.AddWithValue("@Paterno", TxtPaterno.Text);
                    cmd.Parameters.AddWithValue("@Materno", TxtMaterno.Text);
                    cmd.Parameters.AddWithValue("@RFC", TxtRFC.Text);
                    cmd.Parameters.AddWithValue("@Email", TxtEmail.Text);
                    cmd.Parameters.AddWithValue("@NumCelular", TxtNumCelular.Text);
                    cmd.Parameters.AddWithValue("@Password", passwordHash);
                    cmd.Parameters.AddWithValue("@Salt", salt);  // Guardar el salt
                    cmd.Parameters.AddWithValue("@Rol", "Participante");

                    cmd.ExecuteNonQuery();

                    ClientScript.RegisterStartupScript(this.GetType(), "RegistroExitoso",
                        "Swal.fire({ title: 'Registro Exitoso', text: 'Te has registrado correctamente.', icon: 'success', confirmButtonText: 'Aceptar' }).then(() => { window.location = 'Login.aspx'; });", true);
                }
                catch (Exception ex)
                {
                    // Manejo de errores generales
                    ClientScript.RegisterStartupScript(this.GetType(), "Error",
                        $"Swal.fire({{ title: 'Error', text: 'Ocurrió un error: {ex.Message}', icon: 'error', confirmButtonText: 'Aceptar' }});", true);
                }
            }
        }

        private string GenerateSalt()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16]; // Generar un salt de 16 bytes
                rng.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
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
