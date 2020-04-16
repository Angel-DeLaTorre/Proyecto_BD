﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace Proyecto_BD.Models
{
    public class Prestamo
    {
        private int idPrestamo;
        private string clavePrestamo;
        private Laboratorio laboratorio;
        private Laboratorista laboratorista;
        private Alumno alumno;
        private string fechaPrestamo;
        private string fechaLimite;
        private string fechaDevolucion;
        private string fechaCompensacion;
        private int pagoMulta;
        private string observaciones;
        private int estatus;

        public int IdPrestamo { get => idPrestamo; set => idPrestamo = value; }
        public string ClavePrestamo { get => clavePrestamo; set => clavePrestamo = value; }


        [Required]
        [Display(Name = "ID Laboratorio")]
        [Editable(false, AllowInitialValue = true)]
        public Laboratorio Laboratorio { get => laboratorio; set => laboratorio = value; }


        [Required]
        [Display(Name = "ID Laboratorista")]
        [Editable(false, AllowInitialValue = true)]
        public Laboratorista Laboratorista { get => laboratorista; set => laboratorista = value; }

        [Required]
        [Display(Name = "ID Alumno")]
        public Alumno Alumno { get => alumno; set => alumno = value; }

        public string FechaPrestamo { get => fechaPrestamo; set => fechaPrestamo = value; }

        [Required]
        [Display(Name = "Fecha Límite")]
        [Editable(false, AllowInitialValue = true)]

        public string FechaLimite { get => fechaLimite; set => fechaLimite = value; }
        public string FechaDevolucion { get => fechaDevolucion; set => fechaDevolucion = value; }
        public string FechaCompensacion { get => fechaCompensacion; set => fechaCompensacion = value; }
        public int PagoMulta { get => pagoMulta; set => pagoMulta = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public int Estatus { get => estatus; set => estatus = value; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
