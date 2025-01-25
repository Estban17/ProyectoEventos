using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eventos
{
    public partial class VerParticipantes : Page
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
                int idEvento = Convert.ToInt32(Request.QueryString["IdEvento"]);
                CargarParticipantes(idEvento);
            }
        }

        // Carga los participantes en el GridView
        private void CargarParticipantes(int idEvento)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT 
                        PE.IdUsuario,
                        CONCAT(U.Nombre, ' ', U.Paterno, ' ', U.Materno) AS NombreCompleto,
                        U.NumCelular,
                        U.Email,
                        PE.AsistenciaConfirmada,
                        PE.FechaConfirmacion
                    FROM ParticipantesEventos PE
                    INNER JOIN Usuarios U ON PE.IdUsuario = U.IdUsuario
                    WHERE PE.IdEvento = @IdEvento", conn);

                cmd.Parameters.AddWithValue("@IdEvento", idEvento);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                GvParticipantes.DataSource = dt;
                GvParticipantes.DataBind();
            }
        }

        // Maneja el evento de regresar
        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminEventos.aspx");
        }

        // Maneja el cambio en la casilla de confirmación
        protected void ChkConfirmar_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;

            int IdUsuario = Convert.ToInt32(GvParticipantes.DataKeys[row.RowIndex].Value);
            bool asistenciaConfirmada = chk.Checked;

            // Actualiza la confirmación de asistencia en la base de datos
            ActualizarAsistencia(IdUsuario, asistenciaConfirmada);
        }

        // Actualiza la asistencia del participante en la base de datos
        private void ActualizarAsistencia(int IdUsuario, bool asistenciaConfirmada)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(
                    @"UPDATE ParticipantesEventos 
                      SET AsistenciaConfirmada = @AsistenciaConfirmada,
                          FechaConfirmacion = @FechaConfirmacion
                      WHERE IdUsuario = @IdUsuario", conn);

                cmd.Parameters.AddWithValue("@AsistenciaConfirmada", asistenciaConfirmada);

                // Si la asistencia es confirmada, registramos la fecha de confirmación, sino se establece como NULL
                cmd.Parameters.AddWithValue("@FechaConfirmacion", asistenciaConfirmada ? (object)DateTime.Now : DBNull.Value);
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        protected void GvParticipantes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ConfirmarAsistencia")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GvParticipantes.Rows[index];

                if (row != null && row.RowIndex >= 0)
                {
                    int IdUsuario = Convert.ToInt32(GvParticipantes.DataKeys[row.RowIndex].Value);
                    // Continuar con el procesamiento...
                }
            }
        }

    }
}
