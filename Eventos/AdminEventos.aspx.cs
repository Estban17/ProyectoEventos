using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eventos
{
    public partial class AdminEventos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario ha iniciado sesión
            if (Session["IdUsuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                // Cargar la lista de eventos al inicio
                CargarEve();
            }

            // Verificar si hay un parámetro para eliminar un evento
            if (Request.QueryString["EliminarId"] != null)
            {
                string idEvento = Request.QueryString["EliminarId"];
                if (int.TryParse(idEvento, out int id))
                {
                    EliminarEve(idEvento);
                }
                else
                {
                    MostrarMensajeError("El ID del evento es inválido.");
                }
            }
        }

        private void CargarEve()
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Eventos", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                RptEventos.DataSource = dt;
                RptEventos.DataBind();
            }
        }

        //private void CargarEventos()
        //{
        //    string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SELECT * FROM Eventos", conn);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        RptEventos.DataSource = dt;
        //        RptEventos.DataBind();
        //    }
        //}

        protected void BtnNuevoEvento_Click(object sender, EventArgs e)
        {
            Response.Redirect("CrearEvento.aspx");
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            string idEvento = ((Button)sender).CommandArgument;
            Response.Redirect($"EditarEvento.aspx?IdEvento={idEvento}");
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            // Obtener el ID del evento a eliminar
            string idEvento = ((Button)sender).CommandArgument;

            // Mostrar la alerta de confirmación en el cliente
            string script = $@"
                Swal.fire({{
                    title: '¿Estás seguro?',
                    text: '¡No podrás revertir esta acción!',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Sí, eliminar',
                    cancelButtonText: 'Cancelar'
                }}).then((result) => {{
                    if (result.isConfirmed) {{
                        window.location.href = 'AdminEventos.aspx?EliminarId={idEvento}';
                    }}
                }});
            ";

            // Registrar el script para ejecutarlo en el cliente
            ClientScript.RegisterStartupScript(this.GetType(), "ConfirmDelete", script, true);
        }

        private void EliminarEve(string idEvento)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd1 = new SqlCommand("DELETE FROM ParticipantesEventos WHERE IdEvento = @IdEvento", conn);
                    SqlCommand cmd = new SqlCommand("DELETE FROM Eventos WHERE IdEvento = @IdEvento", conn);

                    cmd1.Parameters.AddWithValue("@IdEvento", idEvento);
                    cmd.Parameters.AddWithValue("@IdEvento", idEvento);

                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                }
                CargarEve();

                MostrarMensajeExito("Evento eliminado correctamente.");
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Ocurrió un error al eliminar el evento: {ex.Message}");
            }
        }

        //private void EliminarEvento(string idEvento)
        //{
        //    try
        //    {
        //        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //        using (SqlConnection conn = new SqlConnection(connString))
        //        {
        //            // Eliminar participantes relacionados con el evento
        //            SqlCommand cmd1 = new SqlCommand("DELETE FROM ParticipantesEventos WHERE IdEvento = @IdEvento", conn);

        //            // Eliminar el evento
        //            SqlCommand cmd = new SqlCommand("DELETE FROM Eventos WHERE IdEvento = @IdEvento", conn);

        //            cmd1.Parameters.AddWithValue("@IdEvento", idEvento);
        //            cmd.Parameters.AddWithValue("@IdEvento", idEvento);

        //            conn.Open();
        //            cmd1.ExecuteNonQuery();
        //            cmd.ExecuteNonQuery();
        //        }

        //        // Recargar la lista de eventos después de la eliminación
        //        CargarEve();

        //        // Mostrar el mensaje de éxito
        //        MostrarMensajeExito("Evento eliminado correctamente.");
        //    }
        //    catch (Exception ex)
        //    {
        //        MostrarMensajeError($"Ocurrió un error al eliminar el evento: {ex.Message}");
        //    }
        //}

        private void MostrarMensajeExito(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Exito", $"Swal.fire('Exito!', '{mensaje}', 'success');", true);
        }

        private void MostrarMensajeError(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Error", $"Swal.fire('Error', '{mensaje}', 'error');", true);
        }

        protected void BtnVerParticipantes_Click(object sender, CommandEventArgs e)
        {
            int idEvento = Convert.ToInt32(e.CommandArgument);
            Response.Redirect($"VerParticipantes.aspx?IdEvento={idEvento}");
        }
    }
}
