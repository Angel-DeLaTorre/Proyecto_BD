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
    public class LaboratoristaController : Controller
    {
        // GET: Laboratorista
        [HttpGet]
        public ActionResult Index()
        {
            //Listamos todos los laboratoristas en una tabla
            DataTable listaLaboratoristas = DLaboratorista.listarAltaLaboratoristas();
            return View(listaLaboratoristas);
        }

        // GET: Laboratorista/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Laboratorista/Create
        public ActionResult Create()
        {
            cargarDatos();
            return View(new Laboratorista());
        }

        // POST: Laboratorista/Create
        [HttpPost]
        public ActionResult Create(Laboratorista lab)
        {
            try
            {
                // TODO: Add insert logic here
                var conf = DLaboratorista.insertarLaboratorista(lab);
                ViewBag.confirmacion = conf;
                cargarDatos();
                return View();
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Laboratorista/Edit/5
        public ActionResult Edit(int id)
        {
            DataTable dtLab = DLaboratorista.obtenerLaboratorista(id);
            if (dtLab.Rows.Count == 1)
            {
                Laboratorista objLab = new Laboratorista();
                Persona objPersona = new Persona();
                Usuario objUsuario = new Usuario();
                Laboratorio objLaboratorio = new Laboratorio();

                objLab.idLaboratorista = Convert.ToInt32(dtLab.Rows[0][0].ToString());
                objLab.claveLaboratorista = Convert.ToString(dtLab.Rows[0][1].ToString());
                objPersona.nombre = Convert.ToString(dtLab.Rows[0][2].ToString());
                objPersona.apMaterno = Convert.ToString(dtLab.Rows[0][3].ToString());
                objPersona.apPaterno = Convert.ToString(dtLab.Rows[0][4].ToString());
                objPersona.direccion = Convert.ToString(dtLab.Rows[0][5].ToString());
                objPersona.codigoPostal = Convert.ToInt32(Convert.ToString(dtLab.Rows[0][6].ToString()));
                objPersona.telefono = Convert.ToString(dtLab.Rows[0][7].ToString());
                objPersona.sexo = Convert.ToChar(Convert.ToString(dtLab.Rows[0][8].ToString()));
                objUsuario.contrasenia = Convert.ToString(dtLab.Rows[0][9].ToString());
                objUsuario.rol = Convert.ToInt32(dtLab.Rows[0][10].ToString());
                objLab.turno = Convert.ToString(dtLab.Rows[0][11].ToString());
                objLaboratorio.IdLaboratorio = Convert.ToInt32(dtLab.Rows[0][12].ToString());

                objLab.Persona = objPersona;
                objLab.Usuario = objUsuario;
                objLab.Laboratorio = objLaboratorio;
                cargarDatos();
                return View(objLab);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Laboratorista/Edit/5
        [HttpPost]
        public ActionResult Edit(Laboratorista lab)
        {
            try
            {
                // TODO: Add update logic here
                var conf1 =  DLaboratorista.acutalizarLaboratorista(lab);
                ViewBag.confirmacionA = conf1;
                cargarDatos();
                return View();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return View();
            }
        }

        // GET: Laboratorista/Delete/5
        public ActionResult Delete(int id)
        {
            System.Diagnostics.Debug.WriteLine(DLaboratorista.bajaLaboratorista(id));
            return RedirectToAction("Index");
        }

        public ActionResult _Bajas()
        {
            DataTable listaLaboratoristas = DLaboratorista.listarBajaLaboratoristas();

            return PartialView(listaLaboratoristas);
        }

        private void cargarDatos()
        {
            List<Laboratorio> listLab = DLaboratorista.listarLaboratoriosDisponibles();
            ViewBag.listLab = listLab;
        }
    
    }
}