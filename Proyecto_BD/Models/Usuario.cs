using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Usuario
    {
        public int idUsuario { get; set; }
        public string usuario1 { get; set; }
        public string contrasenia { get; set; }
        public int rol { get; set; }
    }
}