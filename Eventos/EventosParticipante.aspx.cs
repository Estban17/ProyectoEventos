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
    public partial class EventosParticipante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUsuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (Session["Rol"] == null || Session["Rol"].ToString() != "Participante")
            {
                Response.Redirect("AccesoDenegado.aspx");
            }

            if (!IsPostBack)
            {
                CargarEventosDisponibles();
            }
        }

        private void CargarEventosDisponibles()
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Eventos WHERE EstadoEvento = 'Activo'", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                RepeaterEventos.DataSource = reader;
                RepeaterEventos.DataBind();
            }
        }

        protected void BtnRegistrar_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int idEvento = int.Parse(button.CommandArgument);
            int idUsuario = int.Parse(Session["IdUsuario"].ToString());

            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM ParticipantesEventos WHERE IdUsuario = @IdUsuario AND IdEvento = @IdEvento", conn);
                checkCmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                checkCmd.Parameters.AddWithValue("@IdEvento", idEvento);

                conn.Open();
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO ParticipantesEventos (IdUsuario, IdEvento, FechaRegistro) VALUES (@IdUsuario, @IdEvento, GETDATE())", conn);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@IdEvento", idEvento);

                    cmd.ExecuteNonQuery();

                    // Mostrar un mensaje de éxito
                    ClientScript.RegisterStartupScript(this.GetType(), "RegistroExitoso", "Swal.fire({ title: 'Registro Exitoso', text: 'Te has registrado en el evento.', icon: 'success', confirmButtonText: 'Aceptar' });", true);
                }
                else
                {
                    // Mostrar un mensaje de error
                    ClientScript.RegisterStartupScript(this.GetType(), "YaRegistrado", "Swal.fire({ title: 'Advertencia', text: 'Ya estás registrado en este evento.', icon: 'warning', confirmButtonText: 'Aceptar' });", true);
                }
            }
        }
        protected void BtnMisEventos_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisEventos.aspx");
        }
        protected void BtnMiPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("MiPerfil.aspx");
        }
    }
}