using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Models
{
    public class Laboratorista
    {
        public int idLaboratorista { get; set; }
        public string claveLaboratorista { get; set; }
        public string turno { get; set; }
        public int idLaboratorio { get; set; }
        public int idPersona { get; set; }
        public int idUsuario { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}