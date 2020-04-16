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
            DataTable dtCarrera = DCarrera.ObtenerCarreraPorId(id);

            if (dtCarrera.Rows.Count == 1)
            {
                Carrera carrera = new Carrera();
                carrera.IdCarrera = Convert.ToInt32(dtCarrera.Rows[0][0].ToString());
                carrera.ClaveCarrera = Convert.ToString(dtCarrera.Rows[0][1].ToString());
                carrera.Nombre = Convert.ToString(dtCarrera.Rows[0][2].ToString());
                return View(carrera);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // POST: Carrera/Edit/N
        [HttpPost]
        public ActionResult Edit(Carrera carrera)
        {
            try
            {
                // TODO: Add update logic here
                Console.WriteLine(DCarrera.AcutalizarCarrera(carrera));
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        // GET: Carrera/Delete/5
        public ActionResult Delete(int id)
        {
            System.Diagnostics.Debug.WriteLine(DCarrera.BajaCarrera(id));
            return RedirectToAction("Index");
        }
    }
}
