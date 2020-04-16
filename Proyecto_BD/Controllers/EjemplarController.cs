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
    public class EjemplarController : Controller
    {
        public ActionResult Index()
        {
            DataTable dt = DEjemplar.ListarEjemplares();
            return View(dt);
        }

        // GET: Ejemplar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ejemplar/Create
        public ActionResult Create()
        {
            ViewBag.cmbMateriales = DEjemplar.ObtenerMateriales();
            ViewBag.cmbLabs = DEjemplar.ObtenerLabs();
            return View(new Ejemplar());
        }

        // POST: Ejemplar/Create
        [HttpPost]
        public ActionResult Create(Ejemplar e)
        {
            try
            {
                // TODO: Add insert logic here
                List<Material> materiales = DEjemplar.ObtenerMateriales();
                List<Laboratorio> labs = DEjemplar.ObtenerLabs();

                int idLab = Convert.ToInt32(Request.Form["Laboratorio"]);
                int idMat = Convert.ToInt32(Request.Form["Material"]);

                //Obtenemos el objeto con el id de el value de nuestra vista
                e.Laboratorio = labs.Find(l => l.IdLaboratorio == idLab);
                e.Material = materiales.Find(m => m.IdMaterial == idMat);


                //System.Diagnostics.Debug.WriteLine(e.ToString());
                System.Diagnostics.Debug.WriteLine(DEjemplar.InsertarEjemplar(e));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ejemplar/Edit/5
        public ActionResult Edit(int id)
        {
            Ejemplar e = DEjemplar.ObtenerEjemplar(id);
            ViewBag.cmbMateriales = DEjemplar.ObtenerMateriales();
            ViewBag.cmbLabs = DEjemplar.ObtenerLabs();
            System.Diagnostics.Debug.WriteLine(e.ToString());
            return View(e);
        }

        // POST: Ejemplar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Ejemplar e = DEjemplar.ObtenerEjemplar(id);
                List<Material> materiales = DEjemplar.ObtenerMateriales();
                List<Laboratorio> labs = DEjemplar.ObtenerLabs();

                int idLab = Convert.ToInt32(Request.Form["Laboratorio"]);
                int idMat = Convert.ToInt32(Request.Form["Material"]);
                DateTime FechaCompra = Convert.ToDateTime(Request.Form["FechaCompra"].Substring(0, 10));

                e.Prestado = Convert.ToInt32(Request.Form["Prestado"]);
                e.FechaCompra = FechaCompra;

                //Obtenemos el objeto con el id de el value de nuestra vista
                e.Laboratorio = labs.Find(l => l.IdLaboratorio == idLab);
                e.Material = materiales.Find(m => m.IdMaterial == idMat);


                System.Diagnostics.Debug.WriteLine(e.ToString());
                System.Diagnostics.Debug.WriteLine(DEjemplar.ActualizarEjemplar(e));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ejemplar/Delete/5
        public ActionResult Delete(int id)
        {
            System.Diagnostics.Debug.WriteLine(DEjemplar.BajaEjemplar(id));
            return RedirectToAction("Index");
        }
    }
}
