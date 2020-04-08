using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Proyecto_BD.Models
{
    public class Material
    {
        private int idMaterial;
        private string claveMaterial;
        private string nombre;
        private string descripcion;
        private float costoDevolucion;
        private string fotografia;

        public int IdMaterial { get => idMaterial; set => idMaterial = value; }
        public string ClaveMaterial { get => claveMaterial; set => claveMaterial = value; }

        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Nombre { get => nombre; set => nombre = value; }

        [StringLength(maximumLength: 200, MinimumLength = 3)]
        public string Descripcion { get => descripcion; set => descripcion = value; }


        [Display(Name = "Costo de devolución")]
        public float CostoDevolucion { get => costoDevolucion; set => costoDevolucion = value; }
        public string Fotografia { get => fotografia; set => fotografia = value; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}