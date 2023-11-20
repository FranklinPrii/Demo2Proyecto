using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto2Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Formulario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GuardarDatos(string username, string password)
        {
            var user = "123";
            var pass = "456";

            if (user == username && pass == password)
            {
                //Session["admin"] = username;
                return Json(1);

            }
            else
            {
                return Json(0);
            }

            
        }

        //[HttpPost]
        //public ActionResult GuardarDatos(string username, string password)
        //{
        //    var user = "123";
        //    var pass = "456";

        //    if (user == username && pass == password)
        //    {
        //        Session["admin"] = username;
        //        return Json(1);
        //    }
        //    else
        //    {
        //        return Json(0);
        //    }


        //}


        //[HttpPost]
        //public JsonResult GuardarDatos(string username, string password)
        //{
        //    var user = "123";
        //    var pass = "456";
        //    var response = "";
        //    if (user == username && pass == password)
        //    {
        //        response = "Datos recibidos con éxito. Usuario: " + username + ", Contraseña: " + password;
        //    }
        //    else
        //    {
        //        response = "Datos incorrectos: " + username + ", Contraseña: " + password;
        //    }

        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}
    }
}