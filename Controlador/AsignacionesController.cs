using System;
using System.Collections.Generic;
using System.Text;
using Modelo;
using System.Data.SqlClient;

namespace Controlador
{
    /// <summary>
    /// Controlador para gestionar asignaciones de tripulantes a misiones
    /// </summary>
    public class AsignacionesController
    {
        private AsignacionesService asignacionesService;

        public AsignacionesController()
        {
            asignacionesService = new AsignacionesService();
        }

        /// <summary>
        /// Crea una asignación con estado inicial "Pendiente"
        /// </summary>
        public void AsignarTripulanteAMision(int tripulanteId, int misionId)
        {
            Asignacion asignacion = new Asignacion
            {
                TripulanteId = tripulanteId,
                MisionId = misionId,
                Estado = "Pendiente"
            };

            asignacionesService.AgregarAsignacion(asignacion);
        }

        /// <summary>
        /// Devuelve todas las asignaciones registradas
        /// </summary>
        public List<Asignacion> ObtenerAsignaciones()
        {
            return asignacionesService.ObtenerAsignaciones();
        }

        /// <summary>
        /// Elimina una asignación específica por su ID
        /// </summary>
        public void EliminarAsignacion(int asignacionId)
        {
            var asignacion = new Asignacion { Id = asignacionId };
            asignacionesService.EliminarAsignacion(asignacion);
        }
    }
}
