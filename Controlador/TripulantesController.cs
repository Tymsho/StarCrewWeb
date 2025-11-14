using System;
using System.Collections.Generic;
using System.Text;
using Modelo;
using System.Data.SqlClient;

namespace Controlador
{
    /// <summary>
    /// Controlador para gestionar operaciones sobre Tripulantes
    /// </summary>
    public class TripulantesController
    {
        private readonly TripulantesService tripulantesService;
        private readonly RolesService rolesService;

        public TripulantesController()
        {
            // Inicializa los servicios que comunican con la base de datos
            tripulantesService = new TripulantesService();
            rolesService = new RolesService();
        }

        /// <summary>
        /// Crea un nuevo tripulante en la base de datos
        /// </summary>
        public void CrearTripulante(string nombre, int rolId)
        {
            // Construye el objeto Tripulante
            Tripulante nuevoTripulante = new Tripulante
            {
                Nombre = nombre,
                RolId = rolId,
            };

            // Lo persiste usando el servicio
            tripulantesService.AgregarTripulante(nuevoTripulante);
        }

        /// <summary>
        /// Actualiza datos básicos de un tripulante existente
        /// </summary>
        public void ActualizarTripulante(int id, string nombre, int rolId)
        {
            // Obtiene el tripulante actual para no sobreescribir otros campos
            var tripulante = tripulantesService.ObtenerTripulantePorId(id);
            if (tripulante == null)
                throw new Exception("No se encontró el tripulante con ID " + id);

            // Modifica solo las propiedades deseadas
            tripulante.Nombre = nombre;
            tripulante.RolId = rolId;

            // Persiste los cambios
            tripulantesService.ActualizarTripulante(tripulante);
        }

        /// <summary>
        /// Devuelve la lista completa de tripulantes
        /// </summary>
        public List<Tripulante> ObtenerTripulantes()
        {
            return tripulantesService.ObtenerTripulantes();
        }

        /// <summary>
        /// Devuelve un solo tripulante por su ID
        /// </summary>
        public Tripulante ObtenerTripulantePorId(int id)
        {
            return tripulantesService.ObtenerTripulantePorId(id);
        }

        /// <summary>
        /// Elimina un tripulante especificado por ID
        /// </summary>
        public void EliminarTripulante(int tripulanteId)
        {
            tripulantesService.EliminarTripulante(tripulanteId);
        }

        /// <summary>
        /// Devuelve la lista de roles disponibles
        /// </summary>
        public List<Rol> ObtenerRoles()
        {
            return rolesService.ObtenerRoles();
        }
    }
}
