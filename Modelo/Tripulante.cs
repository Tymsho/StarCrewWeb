using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Modelo
{
    [Serializable]
    public class Tripulante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int RolId { get; set; }
        public int NivelHabilidad { get; set; }
    }

    //Clase que maneja las consultas a la tabla Tripulantes
    public class TripulantesService
    {
        private string connectionString = Conexion.Local;

        //Inserta un nuevo tripulantes
        public void AgregarTripulante(Tripulante tripulante)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Tripulantes (Nombre, RolId) VALUES (@Nombre, @RolId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", tripulante.Nombre);
                command.Parameters.AddWithValue("@RolId", tripulante.RolId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //Modifica un tripulante por ID
        public void ActualizarTripulante(Tripulante tripulante)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Tripulantes SET Nombre = @Nombre, RolId = @RolId, NivelHabilidad = @NivelHabilidad WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", tripulante.Id);
                command.Parameters.AddWithValue("@Nombre", tripulante.Nombre);
                command.Parameters.AddWithValue("@RolId", tripulante.RolId);
                command.Parameters.AddWithValue("@NivelHabilidad", tripulante.NivelHabilidad);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //Elimina un tripulante por ID
        public void EliminarTripulante(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Tripulantes WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //Obtiene una lista de todos los tripulantes cargados
        public List<Tripulante> ObtenerTripulantes()
        {
            List<Tripulante> tripulantes = new List<Tripulante>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nombre, RolId, NivelHabilidad FROM Tripulantes";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tripulantes.Add(new Tripulante
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            RolId = reader.GetInt32(2),
                            NivelHabilidad = reader.GetInt32(3)
                        });
                    }
                }
                connection.Close();
            }

            return tripulantes;
        }

        //Obtiene un tripulante segun el ID
        public Tripulante ObtenerTripulantePorId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nombre, RolId, NivelHabilidad FROM Tripulantes WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Tripulante
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            RolId = reader.GetInt32(2),
                            NivelHabilidad = reader.IsDBNull(3) ? 0 : reader.GetInt32(3)
                        };
                    }
                }
                connection.Close();
            }
            return null; // Tripulante no encontrado
        }
    }
}
