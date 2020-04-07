using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Models
{
    public class Material
    {
        private int idMaterial;
        private string claveMaterial;
        private string nombre;
        private string descripcion;
        private double costoDevolucion;
        private string fotografia;

        public int IdMaterial { get => idMaterial; set => idMaterial = value; }
        public int ClaveMaterial { get => claveMaterial; set => claveMaterial = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Descripcion { get => descripcion; set => descripcion = value; }
        public int CostoDevolucion { get => costoDevolucion; set => costoDevolucion = value; }
        public int Fotografia { get => fotografia; set => fotografia = value; }
    }
}