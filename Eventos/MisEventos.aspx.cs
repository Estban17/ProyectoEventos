using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Eventos
{
    public partial class MisEventos : System.Web.UI.Page
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

                CargarMisEventos();
            }
        }
        private void CargarMisEventos()
        {
            int idUsuario = int.Parse(Session["IdUsuario"].ToString());
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT E.IdEvento, E.Nombre, E.Descripcion, E.Lugar, E.FechaInicio, E.FechaFin, E.Imagen 
                      FROM Eventos E
                      INNER JOIN ParticipantesEventos PE ON E.IdEvento = PE.IdEvento
                      WHERE PE.IdUsuario = @IdUsuario", conn);

                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                RepeaterMisEventos.DataSource = dt;
                RepeaterMisEventos.DataBind();
            }
        }
    }
}