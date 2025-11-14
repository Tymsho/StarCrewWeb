using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controlador;
using Modelo;

namespace StarCrewWeb
{
    public partial class Historial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMisionesActivas();
                CargarHistorial();
            }
        }

        private void CargarMisionesActivas()
        {
            
            MisionesController mController = new MisionesController();
            var misionesActivas = mController.ConsultarMisionesActivas();

            ddlMisionesActivas.DataSource = misionesActivas;
            ddlMisionesActivas.DataBind();

            // Si no hay misiones, lo indicamos y desactivamos el botón
            if (misionesActivas.Count == 0)
            {
                ddlMisionesActivas.Items.Clear(); // Limpiamos todo para asegurar el estado
                ddlMisionesActivas.Items.Add(new ListItem("No hay misiones activas pendientes", ""));
                btnFinalizar.Enabled = false;
            }
            else
            {
                btnFinalizar.Enabled = true;
            }
        }

        private void CargarHistorial()
        {
            MisionesController mController = new MisionesController();

            var historial = mController.ConsultarHistorialMisiones()
                            .Select(h => new
                            {
                                h.FechaFinalizacion,
                                h.Resultado,
                                h.MisionId,
                                h.Detalles
                            }).ToList();

            gvHistorial.DataSource = historial.OrderByDescending(h => h.FechaFinalizacion);
            gvHistorial.DataBind();
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlMisionesActivas.SelectedValue))
            {
                lblResultadoFinal.Text = "No hay una misión válida seleccionada.";
                lblResultadoFinal.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int misionId = Convert.ToInt32(ddlMisionesActivas.SelectedValue);

            //Instanciamos el controlador
            MisionesController mController = new MisionesController();

            // 2.Llamamos a la lógica de negocio principal
            // Esta funcion hace todo: valida requisitos, calcula exito/fracaso,
            // actualiza asignaciones y sube nivel a tripulantes.
            mController.FinalizarMision(misionId);

            // 3. Refrescamos ambas listas
            CargarMisionesActivas(); // La mision finalizada desaparecera de aquí
            CargarHistorial();      // Y aparecera aqui con su resultado

            lblResultadoFinal.Text = "¡Misión finalizada! El resultado se ha movido al historial.";
            lblResultadoFinal.ForeColor = System.Drawing.Color.Green;
        }
    }
}