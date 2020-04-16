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
                //link- iu
                using (DB_MaterialabEntities db = new DB_MaterialabEntities())
                {
                    var list = from d in db.Usuario
                               where d.usuario1 == user && d.contrasenia == pass
                               select d;

                    if(list.Count() > 0)
                    {
                        Session["User"] = list.First();
                        return Content("1");
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