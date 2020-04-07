using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Models
{
    public class DetallePrestamo
    {
        public int idPrestamo { get; set; }
        public int idEjemplar { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}