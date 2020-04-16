using Proyecto_BD.Datos;
using Proyecto_BD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Index(string matricula, string[] value_Check, string action)
        {
            if (action.Equals("Buscar"))
            {
                List<Object> list = DDevolucion.getEjemplaresPrestados(matricula);
                if (list != null)
                {
                    foreach (Object item in list)
                    {
                        if (item is Prestamo)
                        {
                            ViewBag.prestamo = (Prestamo)item;
                        }
                        if (item is List<Ejemplar>)
                        {
                            ViewBag.listEjemplar = (List<Ejemplar>)item;
                        }
                        if (item is string)
                        {
                            ViewBag.mensaje = (string)item;
                        }
                    }
                }
            }
            if (action.Equals("Entregar"))
            {
                List<int> idEjemplares = new List<int>();
                foreach(var item in value_Check)
                {
                    idEjemplares.Add(Convert.ToInt32(item));
                    Console.WriteLine(Convert.ToInt32(item));
                }
                DDevolucion.realizarDevolucion(idEjemplares);
            }
            return View();
        }
    }
}