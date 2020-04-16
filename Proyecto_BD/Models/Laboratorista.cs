using Newtonsoft.Json;
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
        public Laboratorio Laboratorio { get; set; }
        public Persona Persona { get; set; }
        public Usuario Usuario { get; set; }
        public int estatus { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}