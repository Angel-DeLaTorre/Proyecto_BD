﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_BD.Controllers;
using Proyecto_BD.Models;

namespace Proyecto_BD.Filters
{
    public class VerifySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var oUser = (Usuario)HttpContext.Current.Session["User"];

            if (oUser == null)
            {
                if(filterContext.Controller is AccessController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Access/Index");
                }
            }
            else
            {
                if (filterContext.Controller is AccessController == true)
                {    
                    if(oUser.rol == 1)
                    {
                        filterContext.HttpContext.Response.Redirect("~/Home/Index");
                    }
                     else if(oUser.rol == 2)
                    {
                        filterContext.HttpContext.Response.Redirect("~/HomeLaboratorista/Index");
                    }
                    else if(oUser.rol == 3)
                    {
                        filterContext.HttpContext.Response.Redirect("~/HomeAlumno/Index");
                    }
                    else
                    {
                        filterContext.HttpContext.Response.Redirect("~/Access/Index");
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}