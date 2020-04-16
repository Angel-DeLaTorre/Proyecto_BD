using Proyecto_BD.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_BD.Datos;

namespace Proyecto_BD.Controllers
{
    public class DevolucionController : Controller
    {
        public ActionResult Index()
        { 
            ViewBag.listPrestamo = DDevolucion.getEjemplaresPrestados("20000003");
            return View();
        }

        [HttpPost]
        public ActionResult Index(String matricula)
        {
            ViewBag.listPrestamo = DDevolucion.getEjemplaresPrestados(matricula);
            return View();
        }
    }
}