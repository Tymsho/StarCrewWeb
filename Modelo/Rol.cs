using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Modelo
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    //Clase que maneja las consultas a la tabla Roles
    public class RolesService
    {
        private string connectionString = Conexion.Local;

        public List<Rol> ObtenerRoles()
        {
            List<Rol> roles = new List<Rol>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nombre FROM Roles";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Rol { Id = reader.GetInt32(0), Nombre = reader.GetString(1) });
                    }
                }
                connection.Close();
            }

            return roles;
        }
    }
}
