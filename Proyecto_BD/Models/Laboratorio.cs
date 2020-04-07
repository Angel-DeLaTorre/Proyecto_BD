using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Models
{
    public class Laboratorio
    {
        private int idLaboratorio;
        private string claveLaboratorio;
        private string nombre;
        private int estatus;

        public int IdLaboratorio { get => idLaboratorio; set => idLaboratorio => value; }
        public int ClaveLaboratorio { get => claveLaboratorio; set => claveLaboratorio => value; }
        public int Nombre { get => nombre; set => nombre => value; }
        public int Estatus { get => estatus; set => estatus => value; }
    }
}