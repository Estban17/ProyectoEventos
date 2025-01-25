using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eventos
{
    public partial class EditarEvento : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                string idEvento = Request.QueryString["IdEvento"];
                CargarEvento(idEvento);
            }
        }

        private void CargarEvento(string idEvento)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Eventos WHERE IdEvento = @IdEvento", conn);
                cmd.Parameters.AddWithValue("@IdEvento", idEvento);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    HiddenIdEvento.Value = idEvento;
                    TxtNombre.Text = reader["Nombre"].ToString();
                    TxtDescripcion.Text = reader["Descripcion"].ToString();
                    TxtImagen.Text = reader["Imagen"].ToString();
                    DdlTipoEvento.SelectedValue = reader["TipoEvento"].ToString();
                    DdlEstadoEvento.SelectedValue = reader["EstadoEvento"].ToString();
                    TxtLugar.Text = reader["Lugar"].ToString();

                    // Dividir fecha y hora para controles separados
                    DateTime fechaInicio = Convert.ToDateTime(reader["FechaInicio"]);
                    TxtFechaInicio.Value = fechaInicio.ToString("yyyy-MM-dd");
                    TxtHoraInicio.Value = fechaInicio.ToString("HH:mm");

                    DateTime fechaFin = Convert.ToDateTime(reader["FechaFin"]);
                    TxtFechaFin.Value = fechaFin.ToString("yyyy-MM-dd");
                    TxtHoraFin.Value = fechaFin.ToString("HH:mm");

                    TxtRangoEdad.Text = reader["RangoEdad"].ToString();
                    DdlGenero.SelectedValue = reader["Genero"].ToString();
                }
            }
        }


        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Combinar las fechas y horas seleccionadas
                DateTime fechaInicio = DateTime.Parse($"{TxtFechaInicio.Value} {TxtHoraInicio.Value}");
                DateTime fechaFin = DateTime.Parse($"{TxtFechaFin.Value} {TxtHoraFin.Value}");

                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Eventos SET Nombre = @Nombre, Descripcion = @Descripcion, Imagen = @Imagen, TipoEvento = @TipoEvento, EstadoEvento = @EstadoEvento, Lugar = @Lugar, FechaInicio = @FechaInicio, FechaFin = @FechaFin, RangoEdad = @RangoEdad, Genero = @Genero, FechaModificacion = @FechaModificacion WHERE IdEvento = @IdEvento", conn);

                    cmd.Parameters.AddWithValue("@IdEvento", HiddenIdEvento.Value);
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
                    cmd.Parameters.AddWithValue("@FechaModificacion", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Actualizado", "Swal.fire({ title: 'Éxito', text: 'Evento actualizado correctamente.', icon: 'success', confirmButtonText: 'Aceptar' }).then(() => { window.location = 'AdminEventos.aspx'; });", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", $"Swal.fire({{ title: 'Error', text: '{ex.Message}', icon: 'error', confirmButtonText: 'Aceptar' }});", true);
            }
        }
        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminEventos.aspx");
        }
    }
}