using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace Eventos
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdUsuario"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                if (Session["Rol"] == null || Session["Rol"].ToString() != "Participante")
                {
                    Response.Redirect("AccesoDenegado.aspx");
                }
                CargarDatosPerfil();
            }
        }

        private void CargarDatosPerfil()
        {
            string userId = Session["IdUsuario"].ToString();
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Nombre, Paterno, Materno, RFC, Email, NumCelular, ImagenPerfil FROM Usuarios WHERE IdUsuario = @IdUsuario", conn);
                cmd.Parameters.AddWithValue("@IdUsuario", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    TxtNombre.Text = reader["Nombre"].ToString();
                    TxtPaterno.Text = reader["Paterno"].ToString();
                    TxtMaterno.Text = reader["Materno"].ToString();
                    TxtRFC.Text = reader["RFC"].ToString();
                    TxtEmail.Text = reader["Email"].ToString();
                    TxtNumCelular.Text = reader["NumCelular"].ToString();
                    if (reader["ImagenPerfil"] != DBNull.Value)
                    {
                        byte[] imageData = (byte[])reader["ImagenPerfil"];
                        string base64String = Convert.ToBase64String(imageData);
                        ImgPerfil.ImageUrl = "data:image/png;base64," + base64String; // Mostrar la imagen
                    }
                }
            }
        }

        protected void BtnActualizarPerfil_Click(object sender, EventArgs e)
        {
            string userId = Session["IdUsuario"].ToString();
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            byte[] imageBytes = null;

            if (FileUploadProfileImage.HasFile)
            {
                string fileExtension = Path.GetExtension(FileUploadProfileImage.PostedFile.FileName).ToLower();

                // Solo permitir imágenes en formato JPG o PNG
                if (fileExtension == ".jpg" || fileExtension == ".png")
                {
                    using (Stream fs = FileUploadProfileImage.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            imageBytes = br.ReadBytes((int)fs.Length);
                        }
                    }
                }
                else
                {
                    LblImagen.Text = "Solo se permiten imágenes en formato JPG o PNG.";
                    LblImagen.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET Nombre = @Nombre, Paterno = @Paterno, Materno = @Materno, RFC = @RFC, Email = @Email, NumCelular = @NumCelular, ImagenPerfil = @ImagenPerfil WHERE IdUsuario = @IdUsuario", conn);
                cmd.Parameters.AddWithValue("@Nombre", TxtNombre.Text);
                cmd.Parameters.AddWithValue("@Paterno", TxtPaterno.Text);
                cmd.Parameters.AddWithValue("@Materno", TxtMaterno.Text);
                cmd.Parameters.AddWithValue("@RFC", TxtRFC.Text);
                cmd.Parameters.AddWithValue("@Email", TxtEmail.Text);
                cmd.Parameters.AddWithValue("@NumCelular", TxtNumCelular.Text);
                cmd.Parameters.AddWithValue("@IdUsuario", userId);

                if (imageBytes != null)
                {
                    cmd.Parameters.AddWithValue("@ImagenPerfil", imageBytes);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImagenPerfil", DBNull.Value); // Si no se subió imagen, eliminar la imagen existente
                }

                cmd.ExecuteNonQuery();
                Response.Redirect("EventosParticipante.aspx");
            }
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("EventosParticipante.aspx");
        }
    }
}
