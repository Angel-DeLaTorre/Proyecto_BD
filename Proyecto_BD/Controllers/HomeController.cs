using Proyecto_BD.Datos;
using Proyecto_BD.Models;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Proyecto_BD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            cargarDatos();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Index(string date, string labor)
        {
            // Aquí cualquier uso de las variables 'usr', 'pwd' y 'rme'
            ViewBag.dateF = date;
            ViewBag.laborF = labor;
            List<string[]> listaReporte = DHome.ListarReporte(date, labor);
            ViewBag.listaReporte = listaReporte;
            cargarDatos();
            return View("Index");
        }

        private void cargarDatos() {
            List<Prestamo> listPrestamos = DHome.ListarAlumnosPendientes3();
            ViewBag.ListaPres3 = listPrestamos;
            int countMaterial = DHome.countMateriales();
            ViewBag.countMaterial = countMaterial;
            int countPrestamo = DHome.countPrestamosRetrasos();
            ViewBag.countPrestamo = countPrestamo;
            List<string[]> listExistencias = DHome.ListarExistenciaLab();
            ViewBag.listExistencias = listExistencias;
            List<Laboratorio> listLab = DHome.listLaboratorios();
            ViewBag.listLab = listLab;
        }

    }
}