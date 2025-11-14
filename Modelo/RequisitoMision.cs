using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class RequisitoMision
    {
        public int Id { get; set; }
        public int MisionId { get; set; }
        public int RolId { get; set; }
        public int CantidadMinima { get; set; }
        public int HabilidadMinima { get; set; }
    }

    //Clase que maneja las consultas a la tabla RequisitosMisiones
    public class RequisitosMisionesService
    {
        private string connectionString = Conexion.Local;

        public List<RequisitoMision> ObtenerRequisitosMision()
        {
            List<RequisitoMision> requisitosMision = new List<RequisitoMision>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, MisionId, RolId, CantidadMinima FROM RequisitosMision";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requisitosMision.Add(new RequisitoMision
                        {
                            Id = reader.GetInt32(0),
                            MisionId = reader.GetInt32(1),
                            RolId = reader.GetInt32(2),
                            CantidadMinima = reader.GetInt32(3)
                        });
                    }
                }
                connection.Close();
            }

            return requisitosMision;
        }

        public List<RequisitoMision> ObtenerRequisitosMision(int misionId)
        {
            var todosLosRequisitos = ObtenerRequisitosMision();
            return todosLosRequisitos.Where(r => r.MisionId == misionId).ToList();
        }
    }
}
