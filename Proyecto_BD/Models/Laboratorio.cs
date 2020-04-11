using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_BD.Models
{
    public class Laboratorio
    {
        [Editable(false, AllowInitialValue = true)]
        [Display(Name = "id del laboratorio")]
        public int IdLaboratorio { get; set; }
        [Editable(false, AllowInitialValue = true)]
        [Display(Name = "clave del laboratorio")]
        public string ClaveLaboratorio { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [Required]
        public string Nombre { get; set; }
        public int Estatus { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}