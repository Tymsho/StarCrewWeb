using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelo;
using System.Data.SqlClient;

namespace Controlador
{
    /// <summary>
    /// Controlador principal para la lógica de misiones
    /// </summary>
    public class MisionesController
    {
        private readonly TripulantesService tripulantesService;
        private readonly AsignacionesService asignacionesService;
        private readonly RequisitosMisionesService requisitosService;
        private readonly HistorialMisionesService historialService;
        private readonly MisionesService misionesService;
        private readonly TiposMisionesService tiposMisionesService;

        public MisionesController()
        {
            // Inicializa todos los servicios necesarios
            tripulantesService = new TripulantesService();
            asignacionesService = new AsignacionesService();
            requisitosService = new RequisitosMisionesService();
            historialService = new HistorialMisionesService();
            misionesService = new MisionesService();
            tiposMisionesService = new TiposMisionesService();
        }

        /// <summary>
        /// Devuelve la lista de tipos de misión
        /// </summary>
        public List<TipoMision> ObtenerTipoMision()
        {
            return tiposMisionesService.ObtenerTiposDeMision();
        }

        /// <summary>
        /// Devuelve todas las misiones
        /// </summary>
        public List<Mision> ObtenerMisiones()
        {
            return misionesService.ObtenerMisiones();
        }

        /// <summary>
        /// Devuelve los requisitos de una misión concreta
        /// </summary>
        public List<RequisitoMision> ObtenerRequisitosMision(int misionId)
        {
            return requisitosService.ObtenerRequisitosMision(misionId);
        }

        /// <summary>
        /// Finaliza una misión: valida requisitos, registra en historial,
        /// actualiza estado de asignaciones y sube nivel de tripulantes si hay éxito.
        /// </summary>
        public void FinalizarMision(int misionId)
        {
            // 1. Obtener asignaciones pendientes de esta misión
            List<Asignacion> asignaciones = asignacionesService.ObtenerAsignaciones()
                .Where(a => a.MisionId == misionId && a.Estado == "Pendiente")
                .ToList();

            // 2. Obtener los tripulantes vinculados a esas asignaciones
            List<Tripulante> tripulantesAsignados = tripulantesService.ObtenerTripulantes()
                .Where(t => asignaciones.Any(a => a.TripulanteId == t.Id))
                .ToList();

            // 3. Cargar los requisitos de la misión
            List<RequisitoMision> requisitos = requisitosService.ObtenerRequisitosMision(misionId);

            // 4. Validar cumplimiento de requisitos
            bool cumple = ValidarCumplimientoRequisitos(requisitos, tripulantesAsignados);
            string resultado = cumple ? "Exitosa" : "Fallida";

            // 5. Guardar el resultado en el historial
            HistorialMision historial = new HistorialMision
            {
                MisionId = misionId,
                FechaFinalizacion = DateTime.Now,
                Resultado = resultado,
                Detalles = cumple
                                    ? "Misión completada exitosamente."
                                    : "No se cumplieron los requisitos de la misión."
            };
            historialService.AgregarHistorialMision(historial);

            // 6. Marcar las asignaciones como procesadas (exitosa o fallida)
            foreach (var asignacion in asignaciones)
            {
                asignacion.Estado = resultado;  // "Exitosa" o "Fallida"
                asignacionesService.ActualizarAsignacion(asignacion);
            }

            // 7. Si fue exitosa, subir nivel de habilidad de cada tripulante (máximo nivel 3)
            if (cumple)
            {
                foreach (Tripulante t in tripulantesAsignados)
                {
                    if (t.NivelHabilidad < 3)
                        t.NivelHabilidad += 1;

                    tripulantesService.ActualizarTripulante(t);
                }
            }
        }

        /// <summary>
        /// Devuelve todas las entradas de historial de misiones
        /// </summary>
        public List<HistorialMision> ConsultarHistorialMisiones()
        {
            return historialService.ObtenerHistorialMisiones();
        }

        /// <summary>
        /// Comprueba que se cumplan todos los requisitos (cantidad y habilidad mínima)
        /// </summary>
        public bool ValidarCumplimientoRequisitos(
            List<RequisitoMision> requisitos,
            List<Tripulante> tripulantesAsignados)
        {
            foreach (var requisito in requisitos)
            {
                // Filtra los tripulantes que tienen el rol requerido
                var tripConRol = tripulantesAsignados
                                   .Where(t => t.RolId == requisito.RolId)
                                   .ToList();

                // Verifica cantidad mínima
                if (tripConRol.Count < requisito.CantidadMinima)
                    return false;

                // Verifica habilidad mínima
                if (!tripConRol.All(t => t.NivelHabilidad >= requisito.HabilidadMinima))
                    return false;
            }
            return true; // Todos los requisitos cumplidos
        }

        /// <summary>
        /// Devuelve las misiones que aún tienen asignaciones pendientes
        /// </summary>
        public List<Mision> ConsultarMisionesActivas()
        {
            // 1. Filtra las asignaciones con estado "Pendiente"
            var asigns = asignacionesService.ObtenerAsignaciones()
                .Where(a => a.Estado == "Pendiente")
                .ToList();

            // 2. Obtiene los IDs únicos de misión
            var idsAct = asigns.Select(a => a.MisionId).Distinct();

            // 3. Filtra las misiones que coinciden con esos IDs
            var todas = misionesService.ObtenerMisiones();
            return todas.Where(m => idsAct.Contains(m.Id)).ToList();
        }
    }
}
