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
    public class LaboratorioController : Controller
    {
        // GET: Laboratorio
        [HttpGet]
        public ActionResult Index()
        {
            //Listamos todas los laboratorios en una tabla
            DataTable listLaboratorio = DLaboratorio.listarLaboratorios();
            return View(listLaboratorio);
        }

        // GET: Laboratorio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Laboratorio/Create
        public ActionResult Create()
        {
            return View(new Laboratorio());
        }

        // POST: Laboratorio/Create
        [HttpPost]
        public ActionResult Create(Laboratorio lab)
        {
            try
            {
                // TODO: Add insert logic here
                DLaboratorio.insertarLaboratorio(lab);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Laboratorio/Delete/5
        public ActionResult Edit(int id)
        {
            DataTable dtLab = DLaboratorio.obtenerLaboratorio(id);
            if(dtLab.Rows.Count == 1)
            {
                Laboratorio objLab = new Laboratorio();

                objLab.IdLaboratorio = Convert.ToInt32(dtLab.Rows[0][0].ToString());
                objLab.ClaveLaboratorio=Convert.ToString(dtLab.Rows[0][1].ToString());
                objLab.Nombre = Convert.ToString(dtLab.Rows[0][2].ToString());

                return View(objLab);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Laboratorio/Edit/5
        [HttpPost]
        public ActionResult Edit(Laboratorio lab)
        {
            try
            {
                DLaboratorio.acutalizarLaboratorio(lab);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            System.Diagnostics.Debug.WriteLine(DLaboratorio.bajaLaboratorio(id));
            return RedirectToAction("Index");
        }
    }
}