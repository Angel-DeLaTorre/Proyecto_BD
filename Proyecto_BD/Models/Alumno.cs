using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Proyecto_BD.Models
{
    public class Alumno
    {
        public int idAlumno { get; set; }
        public string matricula { get; set; }
        public Carrera Carrera{ get; set; }
        public Grupo Grupo { get; set; }
        public Persona Persona { get; set; }
        public Usuario Usuario { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}