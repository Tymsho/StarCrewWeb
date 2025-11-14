using System;
using System.Collections.Generic;
using System.Text;
using Modelo;
using System.Data.SqlClient;

namespace Controlador
{
    /// <summary>
    /// Controlador para exponer la lista de roles (si se necesita separarlo)
    /// </summary>
    public class RolesController
    {
        private RolesService rolesService;

        public RolesController()
        {
            rolesService = new RolesService();
        }

        public List<Rol> ObtenerRoles()
        {
            return rolesService.ObtenerRoles();
        }
    }
}
