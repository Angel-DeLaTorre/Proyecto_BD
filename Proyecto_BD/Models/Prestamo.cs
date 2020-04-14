using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace Proyecto_BD.Models
{
    public class Prestamo
    {
        public int idPrestamo { get; set; }
        public string clavePresramo { get; set; }
        public Laboratorio laboratorio { get; set; }
        public Laboratorista Laboratorista { get; set; }
        public Alumno Alumno { get; set; }
        [Display(Name = "Fecha de préstamo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{YYYY-MM-dd}")]
        public DateTime fechaPrestamo { get; set; }

        [Display(Name = "Fecha límite de entrega")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{YYYY-MM-dd}")]
        public DateTime fechaLimite { get; set; }

        [Display(Name = "Fecha devolución")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{YYYY-MM-dd}")]
        public DateTime fechaDevolucion { get; set; }

        [Display(Name = "Fecha reposición")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{YYYY-MM-dd}")]
        public DateTime fechaCompensacion { get; set; }

        [Display(Name = "Pago multa")]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string pagoMulta { get; set; }

        [Display(Name = "Observaciones")]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string observaciones { get; set; }
        public int estatus { get; set; }
        
        

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
