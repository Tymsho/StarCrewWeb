using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Modelo
{
    public class TipoMision
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    //Clase que maneja las consultas a la tabla TiposDeMisiones
    public class TiposMisionesService
    {
        private string connectionString = Conexion.Local;
        public List<TipoMision> ObtenerTiposDeMision()
        {
            List<TipoMision> tipos = new List<TipoMision>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nombre FROM TiposDeMision";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tipos.Add(new TipoMision { Id = reader.GetInt32(0), Nombre = reader.GetString(1) });
                    }
                }
                connection.Close();
            }

            return tipos;
        }
    }
}
