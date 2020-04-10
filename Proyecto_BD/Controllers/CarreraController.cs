using Proyecto_BD.Datos;
using Proyecto_BD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_BD.Controllers
{
    public class CarreraController : Controller
    {
        // GET: Carrera
        [HttpGet]
        public ActionResult Index()
        {
            //Listamos todas las carreras en una tabla
            DataTable CarrerList = DCarrera.ListarCarreras();
            return View(CarrerList);
        }

        // GET: Carrera/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Carrera/Create
        public ActionResult Create()
        {
            return View(new Carrera());
        }

        // POST: Carrera/Create
        [HttpPost]
        public ActionResult Create(Carrera carrera)
        {
            try
            {
                // TODO: Add insert logic here
                Console.WriteLine(DCarrera.InsertarCarrera(carrera));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Carrera/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Carrera/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Carrera/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Carrera/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
