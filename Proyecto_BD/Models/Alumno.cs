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
        public int idCarrera { get; set; }
        public int idGrupo { get; set; }
        public int idUsuario { get; set; }
        public Persona Persona { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}