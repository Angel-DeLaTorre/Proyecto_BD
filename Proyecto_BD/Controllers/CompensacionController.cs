using Proyecto_BD.Datos;
using Proyecto_BD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_BD.Controllers
{
    public class CompensacionController : Controller 
    {
        // GET: Compensacion
        [HttpGet]
        public ActionResult Index()
        {
            //Listamos todas los compensacion en una tabla
            DataTable listCompensacion = DCompensacion.listarCompensaciones();
            return View(listCompensacion);
        }

        // GET: Compensacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Compensacion/Create
        public ActionResult Create()
        {
            return View(new Prestamo());
        }

        // POST: Compensacion/Create
        [HttpPost]
        public ActionResult Create(Prestamo prestamo)
        {
            try
            {
                // TODO: Add insert logic here
                DCompensacion.insertarCompensacion(prestamo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
