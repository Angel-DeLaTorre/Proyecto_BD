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
            List<string[]> listBajas = DHome.ListarBajasLab("Todos");
            ViewBag.listBajas = listBajas;

            List<string[]> listExistencias = DHome.ListarExistenciaLab("Todos");
            ViewBag.listExistencias = listExistencias;
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
        public ActionResult Index(string labor, string date, string value)
        {
            if (value.Equals("Generar"))
            {
                ViewBag.dateF = date;
                ViewBag.laborF = labor;
                ViewBag.listaReporte = DHome.ListarReporte(date, labor);
            }

            if (value.Equals("Ver bajas"))
            {
                ViewBag.listBajas = DHome.ListarBajasLab(labor);
            }
            if (value.Equals("Ver existencias"))
            {
                ViewBag.listExistencias = DHome.ListarExistenciaLab(labor);
                
            }

            cargarDatos();
            return View("Index");
        }

        private void cargarDatos() {
            ViewBag.ListaPres3 = DHome.ListarAlumnosPendientes3();
            ViewBag.countMaterial = DHome.countMateriales();
            ViewBag.countPrestamo = DHome.countPrestamosRetrasos();
            ViewBag.countBaja = DHome.countMaterialBaja();
            ViewBag.listLab = DHome.listLaboratorios();
        }

    }
}