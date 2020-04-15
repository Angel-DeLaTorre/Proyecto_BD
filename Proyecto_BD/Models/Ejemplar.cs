using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Proyecto_BD.Models
{
    public class Ejemplar
    {
        public int IdEjemplar { get; set; }
        public string ClaveEjemplar { get; set; }
        [Display(Name = "Fecha de compra")]
        [DataType (DataType.Date)]
        [DisplayFormat(DataFormatString = "{YYYY-MM-dd}")]
        public DateTime FechaCompra { get; set; }
        public int Prestado { get; set; }
        public Laboratorio Laboratorio { get; set; }
        public Material Material { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}