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
    public class MaterialController : Controller
    {
        // GET: Material
        [HttpGet]
        public ActionResult Index()
        {
            DataTable listMaterial = DMaterial.listarMateriales();
            return View(listMaterial);
        }

        // GET: Material/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Material/Create
        public ActionResult Create()
        {
            return View(new Material());
        }

        // POST: Material/Create
        [HttpPost]
        public ActionResult Create(Material mat)
        {
            try
            {
                // TODO: Add insert logic here
                
                var a = DMaterial.insertarMaterial(mat);
                ViewBag.Respuesta = a;
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Material/Edit/5
        public ActionResult Edit(int id)
        {

            DataTable dtMat = DMaterial.obtenerMaterial(id);

            if (dtMat.Rows.Count == 1)
            {
                Material objMat = new Material();

                objMat.IdMaterial = Convert.ToInt32(dtMat.Rows[0][0].ToString());
                objMat.ClaveMaterial = Convert.ToString(dtMat.Rows[0][1].ToString());
                objMat.Nombre = Convert.ToString(dtMat.Rows[0][2].ToString());
                objMat.Descripcion = Convert.ToString(dtMat.Rows[0][3].ToString());
                objMat.CostoDevolucion = Convert.ToInt32(dtMat.Rows[0][4].ToString());
                objMat.Fotografia = Convert.ToString(dtMat.Rows[0][5].ToString());


                return View(objMat);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Material/Edit/5
        [HttpPost]
        public ActionResult Edit(Material Mat)
        {
            try
            {
                var a = DMaterial.acutalizarMaterial(Mat);
                ViewBag.Respuesta = a;
                return View(); 
            }
            catch
            {
                return View();
            }
        }

        // GET: Material/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Material/Delete/5
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
