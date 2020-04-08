using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_BD.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }

        [StringLength(maximumLength: 60, MinimumLength = 1)]
        public string usuario { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 1)]
        public string contrasenia { get; set; }
        public int rol { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}