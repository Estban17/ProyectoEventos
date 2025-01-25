using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Eventos
{
    public partial class CrearEvento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (Session["Rol"] == null || Session["Rol"].ToString() != "Administrador")
            {
                Response.Redirect("AccesoDenegado.aspx");
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Leer las fechas y horas seleccionadas
                DateTime fechaInicio = DateTime.Parse($"{TxtFechaInicio.Value} {TxtHoraInicio.Value}");
                DateTime fechaFin = DateTime.Parse($"{TxtFechaFin.Value} {TxtHoraFin.Value}");

                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Eventos (Nombre, Descripcion, Imagen, TipoEvento, EstadoEvento, Lugar, FechaInicio, FechaFin, RangoEdad, Genero, FechaCreacion) VALUES (@Nombre, @Descripcion, @Imagen, @TipoEvento, @EstadoEvento, @Lugar, @FechaInicio, @FechaFin, @RangoEdad, @Genero, @FechaCreacion)", conn);

                    cmd.Parameters.AddWithValue("@Nombre", TxtNombre.Text);
                    cmd.Parameters.AddWithValue("@Descripcion", TxtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@Imagen", TxtImagen.Text);
                    cmd.Parameters.AddWithValue("@TipoEvento", DdlTipoEvento.SelectedValue);
                    cmd.Parameters.AddWithValue("@EstadoEvento", DdlEstadoEvento.SelectedValue);
                    cmd.Parameters.AddWithValue("@Lugar", TxtLugar.Text);
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("@RangoEdad", TxtRangoEdad.Text);
                    cmd.Parameters.AddWithValue("@Genero", DdlGenero.SelectedValue);
                    cmd.Parameters.AddWithValue("@FechaCreacion", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Success", "Swal.fire({ title: 'Éxito', text: 'Evento creado correctamente.', icon: 'success', confirmButtonText: 'Aceptar' }).then(() => { window.location = 'AdminEventos.aspx'; });", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", $"Swal.fire({{ title: 'Error', text: '{ex.Message}', icon: 'error', confirmButtonText: 'Aceptar' }});", true);
            }
        }


        // Elimina estos métodos si no son necesarios
        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminEventos.aspx");
        }
    }
}
