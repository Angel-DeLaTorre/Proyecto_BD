using Proyecto_BD.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_BD.Models;

namespace Proyecto_BD.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Enter(string user, string pass)
        {
            try
            {
                //LINQ
                using (DB_MaterialabEntities1 db = new DB_MaterialabEntities1())
                {
                    var list = from d in db.Usuario
                               where d.usuario1 == user && d.contrasenia == pass
                               select d;

                    if(list.Count() > 0)
                    {
                        Session["User"] = list.First();
                        return Content(list.First().rol.ToString());
                    }
                    else
                    {
                        return Content("Usuario invalido");
                    }
                }
                
               

            }catch(Exception ex)
            {
                return Content("ocurrio un error" + ex.Message);
            }
        }
    }
}