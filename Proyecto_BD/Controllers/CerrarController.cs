using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_BD.Controllers
{
    public class CerrarController : Controller
    {
        // GET: Cerrar 
       public ActionResult Logoff()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Access");
        }
    }
}