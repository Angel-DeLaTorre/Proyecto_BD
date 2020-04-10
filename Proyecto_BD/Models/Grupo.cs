using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Proyecto_BD.Models
{
    public class Grupo
    {

        public int IdGrupo { get; set; }
        public string ClaveGrupo { get; set; }
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Nombre { get; set; }
        public Carrera carrera {get; set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}