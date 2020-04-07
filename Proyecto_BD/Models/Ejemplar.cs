using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Models
{
    public class Ejemplar
    {
        private int idEjemplar;
        private string claveEjemplar;
        private DateTime fechaCompra;
        private string estatus;
        private int prestado; //1-Prestado, 2-Dispo 3-NoDispo
        private Laboratorio laboratorio;
        private Material material;

        public int IdEjemplar { get => idEjemplar; set => idEjemplar = value; }
        public string ClaveEjemplar { get => claveEjemplar; set => claveEjemplar = value; }
        public DateTime FechaCompra { get => fechaCompra; set => fechaCompra = value; }
        public int Prestado { get => prestado; set => prestado = value; }
        public Laboratorio Laboratorio { get => laboratorio; set => laboratorio = value; }
        public Material Material { get => material; set => material = value; }
    }
}