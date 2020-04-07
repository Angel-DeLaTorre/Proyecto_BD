using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Models
{
    public class Carrera
    {
        private int idCarrera;
        private string claveCarrera;
        private string nombre;

        public int IdCarrera { get => idCarrera; set => idCarrera = value; }
        public string ClaveCarrera { get => claveCarrera; set => claveCarrera = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}