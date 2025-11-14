using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Modelo
{
    public class HistorialMision
    {
        public int Id { get; set; }
        public int MisionId { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string Resultado { get; set; }
        public string Detalles { get; set; }
    }

    //Clase que maneja las consultas a la tabla HistorialDeMisiones
    public class HistorialMisionesService
    {
        private string connectionString = Conexion.Local;

        //Agrega una entrada al historial de misiones
        public void AgregarHistorialMision(HistorialMision historial)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO HistorialMisiones (MisionId, FechaFinalizacion, Resultado, Detalles) VALUES (@MisionId, @FechaFinalizacion, @Resultado, @Detalles)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MisionId", historial.MisionId);
                command.Parameters.AddWithValue("@FechaFinalizacion", historial.FechaFinalizacion);
                command.Parameters.AddWithValue("@Resultado", historial.Resultado);
                command.Parameters.AddWithValue("@Detalles", historial.Detalles ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //Obtiene una lista con todas las entradas del historial de misiones
        public List<HistorialMision> ObtenerHistorialMisiones()
        {
            List<HistorialMision> historialMisiones = new List<HistorialMision>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, MisionId, FechaFinalizacion, Resultado, Detalles FROM HistorialMisiones";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        historialMisiones.Add(new HistorialMision
                        {
                            Id = reader.GetInt32(0),
                            MisionId = reader.GetInt32(1),
                            FechaFinalizacion = reader.GetDateTime(2),
                            Resultado = reader.GetString(3),
                            Detalles = reader.IsDBNull(4) ? null : reader.GetString(4)
                        });
                    }
                }
                connection.Close();
            }

            return historialMisiones;
        }
    }
}
