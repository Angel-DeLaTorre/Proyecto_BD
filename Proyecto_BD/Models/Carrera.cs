using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Proyecto_BD.Models
{
    public class Carrera
    {
        private int idCarrera;
        private string claveCarrera;
        private string nombre;
        [Display(Name = "Id de la carrera")]
        public int IdCarrera { get => idCarrera; set => idCarrera = value; }
        [Display(Name = "Clave de la carrera")]
        public string ClaveCarrera { get => claveCarrera; set => claveCarrera = value; }
        [StringLength (maximumLength: 60, MinimumLength =3)]
        public string Nombre { get => nombre; set => nombre = value; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}