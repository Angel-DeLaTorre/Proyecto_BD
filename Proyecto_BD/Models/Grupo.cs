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
        private int idGrupo;
        private string claveGrupo;
        private string nombre;

        public int IdGrupo { get => idGrupo; set => idGrupo = value; }
        public string ClaveGrupo { get => claveGrupo; set => claveGrupo = value; }
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Nombre { get => nombre; set => nombre = value; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}