using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Modelo
{
    [Serializable]
    public class Mision
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int TipoMisionId { get; set; }
        public int Dificultad { get; set; }
        public string Requisitos { get; set; }
    }

    //Clase que maneja las consultas a la tabla Misiones
    public class MisionesService
    {
        private string connectionString = Conexion.Local;
        public List<Mision> ObtenerMisiones()
        {
            List<Mision> misiones = new List<Mision>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Titulo, TipoMisionId, Dificultad, Requisitos FROM Misiones";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        misiones.Add(new Mision
                        {
                            Id = reader.GetInt32(0),
                            Titulo = reader.GetString(1),
                            TipoMisionId = reader.GetInt32(2),
                            Dificultad = reader.GetInt32(3),
                            Requisitos = reader.GetString(4)
                        });
                    }
                }
                connection.Close();
            }
            return misiones;

        }

    }
}
