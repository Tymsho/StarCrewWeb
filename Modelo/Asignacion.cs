using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Modelo
{
    public class Asignacion
    {
        public int Id { get; set; }
        public int TripulanteId { get; set; }
        public int MisionId { get; set; }
        public string Estado { get; set; }
    }

    //Clase que maneja las consultas a la tabla Asignaciones
    public class AsignacionesService
    {
        private string connectionString = Conexion.Local;

        //Inserta una asignacion a la tabla
        public void AgregarAsignacion(Asignacion asignacion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Asignaciones (TripulanteId, MisionId, Estado) VALUES (@TripulanteId, @MisionId, @Estado)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TripulanteId", asignacion.TripulanteId);
                command.Parameters.AddWithValue("@MisionId", asignacion.MisionId);
                command.Parameters.AddWithValue("@Estado", asignacion.Estado);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //Modifica una asignacion por ID
        public void ActualizarAsignacion(Asignacion asignacion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Asignaciones SET Estado = @Estado WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", asignacion.Id);
                command.Parameters.AddWithValue("@Estado", asignacion.Estado);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //Elimina una asignacion por ID
        public void EliminarAsignacion(Asignacion asignacion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Asignaciones WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", asignacion.Id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //Obtiene una lista de las asignaciones existentes
        public List<Asignacion> ObtenerAsignaciones()
        {
            List<Asignacion> asignaciones = new List<Asignacion>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, TripulanteId, MisionId, Estado FROM Asignaciones";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        asignaciones.Add(new Asignacion
                        {
                            Id = reader.GetInt32(0),
                            TripulanteId = reader.GetInt32(1),
                            MisionId = reader.GetInt32(2),
                            Estado = reader.GetString(3)
                        });
                    }
                }
                connection.Close();
            }

            return asignaciones;
        }
    }
}
