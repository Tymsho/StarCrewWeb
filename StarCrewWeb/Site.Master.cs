using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StarCrewWeb
{
    // Esta es la logica de la Pagina Maestra (Site.Master).
    // Desde aquí se controlan los elementos comunes a todo el sitio,
    // como el menú de navegación principal.
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //  Eventos de Clic del Menu de Navegacion 
        protected void btnTripulantes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Tripulantes.aspx");
        }

        protected void btnMisiones_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Misiones.aspx");
        }

        protected void btnHistorial_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Historial.aspx");
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            // Navegacion: "Salir" nos lleva de vuelta a la pagina de inicio.
            Response.Redirect("~/Default.aspx");
        }
    }
}