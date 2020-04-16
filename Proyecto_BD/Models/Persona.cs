using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Proyecto_BD.Models
{
    public class Persona
    {
        public int idPersona { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(maximumLength: 50, MinimumLength =3)]
        public string nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string apPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string apMaterno { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(maximumLength: 255, MinimumLength = 10)]
        public string direccion { get; set; }

        [Display(Name = "Código Postal")]
        [StringLength(maximumLength: 5, MinimumLength = 0)]
        public int codigoPostal { get; set; }

        [Display(Name = "Teléfono")]
        [StringLength(maximumLength: 15, MinimumLength = 10)]
        public long telefono { get; set; }
        public char sexo { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}