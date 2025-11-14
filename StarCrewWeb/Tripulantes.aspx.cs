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
    public partial class Tripulantes : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
                CargarTripulantes();
            }
        }

        private void CargarRoles()
        {
            
            RolesController rolesController = new RolesController();
            cmbRoles.DataSource = rolesController.ObtenerRoles();
            cmbRoles.DataTextField = "Nombre";
            cmbRoles.DataValueField = "Id";
            cmbRoles.DataBind();
        }

        private void CargarTripulantes()
        {
            
            TripulantesController tripController = new TripulantesController();
            var listaRoles = tripController.ObtenerRoles();
            var lista = tripController.ObtenerTripulantes();

            var datosParaGrilla = lista.Select(t => new
            {
                t.Id,
                t.Nombre,
                Rol = listaRoles.FirstOrDefault(r => r.Id == t.RolId)?.Nombre,
                Nivel = t.NivelHabilidad
            }).ToList();

            
            rptTripulantes.DataSource = datosParaGrilla;
            rptTripulantes.DataBind();
        }

        // Este método maneja CREAR y ACTUALIZAR
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            
            TripulantesController tripController = new TripulantesController();

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblAgregar.Text = "Por favor, ingresá un nombre valido.";
                lblAgregar.ForeColor = System.Drawing.Color.Red;
                return;
            }

            int rolId = Convert.ToInt32(cmbRoles.SelectedValue);

            // MANEJO DE ESTADO: Revisa si estamos en "Modo Edición"
            if (ViewState["EditTripulanteId"] != null)
            {
                // Estamos EDITANDO
                int id = (int)ViewState["EditTripulanteId"];
                tripController.ActualizarTripulante(id, txtNombre.Text.Trim(), rolId);

                // Limpiamos el estado
                ViewState["EditTripulanteId"] = null;
                btnAgregar.Text = "Agregar";
                lblAgregar.Text = "Tripulante actualizado correctamente.";
                lblAgregar.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                // Estamos CREANDO
                tripController.CrearTripulante(txtNombre.Text.Trim(), rolId);
                lblAgregar.Text = "Tripulante creado correctamente.";
                lblAgregar.ForeColor = System.Drawing.Color.Green;
            }

            // Limpiar y refrescar
            txtNombre.Text = "";
            CargarTripulantes();
        }

        // Este método es el "ROUTER" para los botones de la grilla/repeater
        protected void rptTripulantes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Obtenemos el ID del tripulante desde el CommandArgument
            int idTripulante = Convert.ToInt32(e.CommandArgument);

            // Verificamos qué botón se presionó
            if (e.CommandName == "Editar")
            {
                EditarTripulante(idTripulante);
            }

            if (e.CommandName == "Eliminar")
            {
                EliminarTripulante(idTripulante);
            }
        }

        // --- MÉTODOS AUXILIARES ---

        private void EditarTripulante(int idTripulante)
        {
            
            TripulantesController tripController = new TripulantesController();

            var tripulante = tripController.ObtenerTripulantePorId(idTripulante);
            if (tripulante != null)
            {
                // 1. Cargamos los datos en los controles del formulario
                txtNombre.Text = tripulante.Nombre;
                cmbRoles.SelectedValue = tripulante.RolId.ToString(); // El Value es un string

                // 2. Cambiamos el botón
                btnAgregar.Text = "Actualizar";

                // 3. Guardamos el ID en el ViewState para que btnAgregar_Click sepa qué hacer
                ViewState["EditTripulanteId"] = idTripulante;

                lblAgregar.Text = $"Editando a {tripulante.Nombre}...";
                lblAgregar.ForeColor = System.Drawing.Color.Blue;
            }
        }

        private void EliminarTripulante(int idTripulante)
        {
            
            AsignacionesController asignController = new AsignacionesController();
            TripulantesController tripController = new TripulantesController();

            // 1. Validar asignaciones 
            var asignaciones = asignController.ObtenerAsignaciones()
                                    .Where(a => a.TripulanteId == idTripulante)
                                    .ToList();

            if (asignaciones.Any())
            {
                // Mostramos el error en el Label.
                lblAgregar.Text = "No se puede eliminar: el tripulante tiene asignaciones.";
                lblAgregar.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 2. Eliminar
            try
            {
                tripController.EliminarTripulante(idTripulante);
                CargarTripulantes(); // Refrescamos la grilla
                lblAgregar.Text = "Tripulante eliminado correctamente.";
                lblAgregar.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblAgregar.Text = $"Error al eliminar: {ex.Message}";
                lblAgregar.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}