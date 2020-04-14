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
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            DataTable dt = DAlumno.ListarAlumnos();
            return View(dt);
        }

        // GET: Alumno/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Alumno/Create
        public ActionResult Create()
        {
            LlenarViewBagCmb();
            return View(new Persona());
        }

        private void LlenarViewBagCmb()
        {
            ViewBag.carreras = DCarrera.LlenarCmbCarreras();
            ViewBag.grupos = DGrupo.LlenarCmbGrupos();
            ViewBag.sexo = new string[] { "Masculino", "Femenino" };
        }

        // POST: Alumno/Create
        [HttpPost]
        public ActionResult Create(Persona p)
        {
            try
            {
                // TODO: Add insert logic here
                string carreraCombo = Request.Form["Carreras"].ToString();
                string grupoCombo = Request.Form["Grupos"].ToString();
                string sexoCombo = Request.Form["Generos"].ToString();

                p.sexo = (sexoCombo.Equals("Masculino")) ? 'H' : 'M'; //asignamos el valor dependiendo del genero

                Alumno a = new Alumno();
                a.idCarrera = DCarrera.ObtenerIdCarreraPNombre(carreraCombo);
                a.idGrupo = DGrupo.ObtenerIdGrupoPNombre(grupoCombo);

                a.Persona = p;
                System.Diagnostics.Debug.WriteLine(DAlumno.InsertarAlumno(a));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                LlenarViewBagCmb();
                return View();
            }
        }

        // GET: Alumno/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {

            string genero = "";
            int idCarrera;
            int idGrupo;

            DataTable dt = DAlumno.ObtenerAlumno(id);

            if (dt.Rows.Count == 1)
            {
                Persona persona = new Persona();

                persona.nombre = Convert.ToString(dt.Rows[0][0].ToString());
                persona.apPaterno = Convert.ToString(dt.Rows[0][1].ToString());
                persona.apMaterno = Convert.ToString(dt.Rows[0][2].ToString());
                persona.direccion = Convert.ToString(dt.Rows[0][3].ToString());
                persona.codigoPostal = Convert.ToInt32(dt.Rows[0][4].ToString());
                persona.telefono = Convert.ToString(dt.Rows[0][5].ToString());

                genero = Convert.ToString(dt.Rows[0][6].ToString());
                idCarrera = Convert.ToInt32(dt.Rows[0][7].ToString());
                idGrupo = Convert.ToInt32(dt.Rows[0][8].ToString());

                DataTable dtCarrera = DCarrera.ObtenerCarreraPorId(idCarrera);
                DataTable dtGrupo = DGrupo.ObtenerGrupoPorId(idGrupo);
                string _genero = (genero.Equals("H")) ? "Masculino" : "Femenino";
                string nombreCarrera = Convert.ToString(dtCarrera.Rows[0][2]);
                string nombreGrupo = Convert.ToString(dtGrupo.Rows[0][2]); ;

                ViewBag.genero = _genero;
                ViewBag.idAlumno = Convert.ToString(dt.Rows[0][9].ToString());
                ViewBag.nombreCarrera = nombreCarrera;
                ViewBag.nombreGrupo = nombreGrupo;
                ViewBag.password = Convert.ToString(dt.Rows[0][10].ToString());


                LlenarViewBagCmb(); // este viewbag llenara el dropdown (combobox)
                return View(persona);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Alumno/Edit/5
        [HttpPost]
        public ActionResult Edit(Persona persona, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                string password = Convert.ToString(collection["password"]);

                string carreraCombo = Request.Form["Carreras"].ToString();
                string grupoCombo = Request.Form["Grupos"].ToString();
                string sexoCombo = Request.Form["Generos"].ToString();

                persona.sexo = (sexoCombo.Equals("Masculino")) ? 'H' : 'M'; //asignamos el valor dependiendo del genero
                Alumno a = new Alumno();
                a.idCarrera = DCarrera.ObtenerIdCarreraPNombre(carreraCombo);
                a.idGrupo = DGrupo.ObtenerIdGrupoPNombre(grupoCombo);
                a.idAlumno = Convert.ToInt32(collection["idAlumno"]);

                a.Persona = persona;

                System.Diagnostics.Debug.WriteLine(DAlumno.AcutalizarAlumno(a, password));

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                return RedirectToAction("Index");
            }
        }

        // GET: Alumno/Delete/5
        public ActionResult Delete(int id)
        {
            System.Diagnostics.Debug.WriteLine(DAlumno.BajaAlumno(id));
            return RedirectToAction("Index");
        }

    }
}
