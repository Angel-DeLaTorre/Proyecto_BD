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
    public class PrestamoController : Controller
    {
        // GET: Prestamo
        [HttpGet]
        public ActionResult Index(string clavePrestamo)
        {

            if (clavePrestamo != null && clavePrestamo != "") {
                ViewBag.clavePrestamo = clavePrestamo;
            }
            
            //Listamos todas las carreras en una tabla
            DataTable PrestamosList = DPrestamo.ListarPrestamos();

            return View(PrestamosList);
        }


        // GET: Prestamo/Create
        public ActionResult Create()
        {
            List<Alumno> alumnos = DPrestamo.ListarAlumnos();
            List<Laboratorista> laboratoristas = DPrestamo.ListarLaboratoristas();
            List<Ejemplar> ejemplares = DPrestamo.ListarMaterialesEjemplares();

            ViewBag.Alumnos = alumnos;
            ViewBag.Laboratoristas = laboratoristas;
            ViewBag.Ejemplares = ejemplares;


            return View(new Prestamo());
        }

        // POST: Prestamo/Create
        [HttpPost]
        public ActionResult Create(int idLaboratorio, int idLaboratorista, int idAlumno, string fechaLimite, List<int> idEjemplares)
        {

            System.Diagnostics.Debug.WriteLine("\n idLaboratorio" + idLaboratorio);
            System.Diagnostics.Debug.WriteLine("\n idLaboratorista " + idLaboratorista);
            System.Diagnostics.Debug.WriteLine("\n idAlumno " + idAlumno);
            System.Diagnostics.Debug.WriteLine("\n fechaLimite " + fechaLimite);
            System.Diagnostics.Debug.WriteLine("\n Primer ejemplar " + idEjemplares[0]);

            Laboratorio l = new Laboratorio();

            Laboratorista e = new Laboratorista();

            Alumno a = new Alumno();

            Prestamo p = new Prestamo();

            l.IdLaboratorio = idLaboratorio;
            e.idLaboratorista = idLaboratorista;
            a.idAlumno = idAlumno;

            p.Laboratorio = l;
            p.Laboratorista = e;
            p.Alumno = a;
            p.FechaLimite = fechaLimite;


            
            string [] respuesta = DPrestamo.realizarPrestamo(p, idEjemplares);

            int validacion = Convert.ToInt32(respuesta[0]);

            if (validacion == 1)
            {

                return Json(new { result = "Redirect", url = Url.Action("Index", "Prestamo"), clavePrestamo = respuesta[1] });
            }
            


            return Json("'validacion':'" + respuesta[0] + "'", JsonRequestBehavior.AllowGet);



        }

        // GET: Prestamo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Prestamo/Edit/5
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

        // GET: Prestamo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Prestamo/Delete/5
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
