using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_final.Models
{
    public class UsuarioRolViewModel
    {
        public string numero_documento { get; set; }
        public string tipo_documento { get; set; }
        public string contraseña { get; set; }
        public string imagen { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }

    }
}