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
    public class GrupoController : Controller
    {
        // GET: Grupo
        public ActionResult Index() 
        {
            //Listamos todos los grupos reras en una tabla
            DataTable dt = DGrupo.ListarGrupos();
            return View(dt);
        }

        // GET: Grupo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Grupo/Create
        public ActionResult Create()
        {
            ViewBag.carreras = DCarrera.LlenarCmbCarreras();
            return View(new Grupo());
        }

        // POST: Grupo/Create
        [HttpPost]
        public ActionResult Create(Grupo grupo)
        {
            try
            {
                string carreraCombo = Request.Form["Carreras"].ToString();

                grupo.IdCarrera = DCarrera.ObtenerIdCarreraPNombre(carreraCombo);

                System.Diagnostics.Debug.WriteLine(DGrupo.InsertarGrupo(grupo));
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Grupo/Edit/5
        public ActionResult Edit(int id)
        {
            DataTable dt = DGrupo.ObtenerGrupo(id);
            ViewBag.carreras = DCarrera.LlenarCmbCarreras(); // este viewbag llenara el dropdown (combobox)
            if (dt.Rows.Count == 1)
            {
                Grupo grupo = new Grupo();

                grupo.IdGrupo = Convert.ToInt32(dt.Rows[0][0].ToString());
                grupo.ClaveGrupo = Convert.ToString(dt.Rows[0][1].ToString());
                grupo.Nombre = Convert.ToString(dt.Rows[0][2].ToString());
                grupo.estatus = Convert.ToInt32(dt.Rows[0][3].ToString());
                grupo.IdCarrera = Convert.ToInt32(dt.Rows[0][4].ToString());

                
                return View(grupo);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Grupo/Edit/5
        [HttpPost]
        public ActionResult Edit(Grupo grupo)
        {
            try
            {
                // TODO: Add update logic here
                string carreraCombo = Request.Form["Carreras"].ToString();

                grupo.IdCarrera = DCarrera.ObtenerIdCarreraPNombre(carreraCombo);

                Console.WriteLine(DGrupo.AcutalizarGrupo(grupo));
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }

        // GET: Grupo/Delete/5
        public ActionResult Delete(int id)
        {
            System.Diagnostics.Debug.WriteLine(DGrupo.BajaGrupo(id));
            return RedirectToAction("Index");
        }

    }
}
