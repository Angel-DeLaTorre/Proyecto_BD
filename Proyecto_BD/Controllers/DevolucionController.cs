using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_BD.Controllers
{
    public class DevolucionController : Controller
    {
        // GET: Devolucion
        public ActionResult Index()
        { 
            ViewBag.listPrestamo = DDevolucion.getEjemplaresPrestados("20000003");
            return View();
        }
    }
}