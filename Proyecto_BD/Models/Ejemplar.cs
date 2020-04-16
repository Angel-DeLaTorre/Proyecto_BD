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
        public int idEjemplar;
        public string claveEjemplar;
        public DateTime fechaCompra;
        public string estatus;
        public int prestado; //1-Prestado, 2-Dispo 3-NoDispo
        public Laboratorio laboratorio;
        public Material material;

        public int IdEjemplar { get => idEjemplar; set => idEjemplar = value; }
        public string ClaveEjemplar { get => claveEjemplar; set => claveEjemplar = value; }
        [Display(Name = "Fecha de compra")]
        [DataType (DataType.Date)]
        [DisplayFormat(DataFormatString = "{YYYY-MM-dd}")]
        public DateTime FechaCompra { get => fechaCompra; set => fechaCompra = value; }
        public int Prestado { get => prestado; set => prestado = value; }
        public Laboratorio Laboratorio { get => laboratorio; set => laboratorio = value; }
        public Material Material { get => material; set => material = value; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}