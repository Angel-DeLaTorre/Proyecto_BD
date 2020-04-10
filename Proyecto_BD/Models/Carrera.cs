using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Proyecto_BD.Models
{
    public class Carrera
    {
        [Editable(false, AllowInitialValue = true)]
        [Display(Name = "Id de la carrera")]
        public int IdCarrera { get; set; }
        [Editable(false, AllowInitialValue = true)]
        [Display(Name = "Clave de la carrera")]
        public string ClaveCarrera { get; set; }
        [StringLength (maximumLength: 60, MinimumLength =3)]
        [Required]
        public string Nombre { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}