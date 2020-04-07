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

        public int IdLaboratorio { get; set; }
        public int ClaveLaboratorio { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public int Nombre { get; set; }
        public int Estatus { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}