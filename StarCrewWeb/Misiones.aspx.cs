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
    public partial class Misiones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMisiones();
                CargarTripulantesDisponibles();
            }
        }

        private void CargarMisiones()
        {
            MisionesController mController = new MisionesController();

            // Obtenemos la lista completa
            var misiones = mController.ObtenerMisiones();

            //Guardamos la lista en ViewState para usarla en el PostBack
            ViewState["ListaDeMisiones"] = misiones;

            ddlMisiones.DataSource = misiones;
            ddlMisiones.DataTextField = "Titulo";
            ddlMisiones.DataValueField = "Id";
            ddlMisiones.DataBind();

            //Añadimos un ítem por defecto
            ddlMisiones.Items.Insert(0, new ListItem("Seleccione una misión...", "0"));
        }

        private void CargarTripulantesDisponibles()
        {
            // Instanciamos los controladores necesarios
            TripulantesController tController = new TripulantesController();
            AsignacionesController aController = new AsignacionesController();

            // 1. Obtenemos TODOS los tripulantes
            var todosLosTripulantes = tController.ObtenerTripulantes();

            // Obtenemos la lista de roles para poder mostrar el nombre
            var listaRoles = tController.ObtenerRoles();

            // 2. Obtenemos los IDs de los tripulantes que ya estan en una mision "Pendiente"
            var idsOcupados = aController.ObtenerAsignaciones()
                                .Where(a => a.Estado == "Pendiente")
                                .Select(a => a.TripulanteId)
                                .Distinct();

            // 3. Filtramos la lista
            var tripulantesDisponibles = todosLosTripulantes
                                .Where(t => !idsOcupados.Contains(t.Id));

            // 4. Mostramos en el ListBox
            lstDisponibles.DataSource = tripulantesDisponibles.Select(t => new
            {
                Id = t.Id,
                Texto = $"{t.Nombre} ({listaRoles.FirstOrDefault(r => r.Id == t.RolId)?.Nombre}, Nivel: {t.NivelHabilidad})"
            }).ToList(); 

            lstDisponibles.DataTextField = "Texto";
            lstDisponibles.DataValueField = "Id";
            lstDisponibles.DataBind();
        }

        protected void ddlMisiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            int misionId = Convert.ToInt32(ddlMisiones.SelectedValue);

            // Recuperamos la lista completa del ViewState
            var misiones = ViewState["ListaDeMisiones"] as List<Mision>;
            if (misiones == null) return; // Si algo falla, salimos

            if (misionId == 0) // Si el usuario selecciona "Seleccione..."
            {
                pnlMisionDetails.Visible = false; // Ocultamos el panel
            }
            else
            {
                // Buscamos la mision en nuestra lista guardada
                var misionSeleccionada = misiones.FirstOrDefault(m => m.Id == misionId);

                if (misionSeleccionada != null)
                {
                    // Rellenamos los labels y mostramos el panel
                    lblDificultad.Text = $"Dificultad: {misionSeleccionada.Dificultad}";
                    lblRequisitos.Text = $"Requisitos: {misionSeleccionada.Requisitos}";
                    pnlMisionDetails.Visible = true;
                }
            }
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            AsignacionesController aController = new AsignacionesController();

            int misionId = Convert.ToInt32(ddlMisiones.SelectedValue);

            // Pequeña validacion extra
            if (misionId == 0)
            {
                lblResultado.Text = "ERROR: SELECCION DE MISION REQUERIDA";
                lblResultado.CssClass = "msg-error";
                return;
            }

            List<int> idsTripulantesSeleccionados = new List<int>();
            foreach (ListItem item in lstDisponibles.Items)
            {
                if (item.Selected)
                {
                    idsTripulantesSeleccionados.Add(Convert.ToInt32(item.Value));
                }
            }

            if (idsTripulantesSeleccionados.Count == 0)
            {
                lblResultado.Text = "ERROR: SELECCION DE TRIPULANTE REQUERIDA";
                lblResultado.CssClass = "msg-error";
                return;
            }

            foreach (int tripulanteId in idsTripulantesSeleccionados)
            {
                aController.AsignarTripulanteAMision(tripulanteId, misionId);
            }

            // Refrescamos la lista de disponibles y reseteamos el combo de misiones
            CargarTripulantesDisponibles();
            CargarMisiones(); // Para que vuelva a "Seleccione..."
            pnlMisionDetails.Visible = false; // Ocultamos el panel

            lblResultado.Text = "ASIGNACION EXITOSA :: MISION ACTIVA";
            lblResultado.CssClass = "msg-success";
        }
    }
}