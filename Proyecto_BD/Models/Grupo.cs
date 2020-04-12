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
        [Required]
        public string Nombre { get; set; }
        [Display(Name = "Id Carrera")]
        public int IdCarrera {get; set;}
        public int estatus { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}