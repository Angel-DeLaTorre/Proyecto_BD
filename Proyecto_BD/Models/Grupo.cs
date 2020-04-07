using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Models
{
    public class Grupo
    {
        private int idGrupo;
        private string claveGrupo;
        private string nombre;

        public int IdGrupo { get => idGrupo; set => idGrupo = value; }
        public string ClaveGrupo { get => claveGrupo; set => claveGrupo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}