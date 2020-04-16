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
        public int idMaterial;
        public string claveMaterial;
        public string nombre;
        public string descripcion;
        public float costoDevolucion;
        public string fotografia;

        public int IdMaterial { get => idMaterial; set => idMaterial = value; }
        
        public string ClaveMaterial { get => claveMaterial; set => claveMaterial = value; }

        [Required(ErrorMessage = "Please enter student name.")]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Nombre { get => nombre; set => nombre = value; }

        [Required]
        [StringLength(maximumLength: 200, MinimumLength = 3)]
        public string Descripcion { get => descripcion; set => descripcion = value; }

        [Required]
        [Display(Name = "Costo de devolución")]
        public float CostoDevolucion { get => costoDevolucion; set => costoDevolucion = value; }

        [Required]
        [Url]
        public string Fotografia { get => fotografia; set => fotografia = value; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}