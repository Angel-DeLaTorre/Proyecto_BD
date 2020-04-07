using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Models
{
    public class Prestamo
    {
        public int idPrestamo { get; set; }
        public string clavePresramo { get; set; }
        public int idLaboratorista { get; set; }
        public int idAlumno { get; set; }
        public DateTime fechaPrestamo { get; set; }
        public DateTime fechaLimite { get; set; }
        public DateTime fechaDevolucion { get; set; }
        public DateTime fechaReposicion { get; set; }
        public string observaciones { get; set; }
        public int estatus { get; set; }
        
        

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
