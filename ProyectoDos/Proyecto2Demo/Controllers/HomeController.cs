using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Proyecto2Demo.Models.DB;

namespace Proyecto2Demo.Controllers
{
    public class HomeController : Controller
    {
        List<ActivaEvaluacion> olistapersonas;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Formulario()
        {
            return View();
        }

        public ActionResult Table()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GuardarDatos(string username, string password)
        {
            var user = "abc";
            var pass = "dfg";

            if (user == username  && pass == password)
            {
                //Session["Proteger"] = "2";
                return Json(1);

            }
            else
            {
                return Json(2);
            }        
        }
                                
        public JsonResult Listar()
        {
            List<ActivaEvaluacion> obdata = new List<ActivaEvaluacion>();
            using (SisEdutivaEntities db = new SisEdutivaEntities())
            {
                obdata = (from p in db.ActivaEvaluacion
                          select p).ToList();
            }
                return Json(new { data = obdata }, JsonRequestBehavior.AllowGet);
            }
                    

            

            //if (Session["Proteger"] != "2")
            //{
            //    RedirectToAction("Index", "Home");

            //}

            //else
            //{
            //   olistapersonas = new List<ActivaEvaluacion>();
            //    using (SisEdutivaEntities db = new SisEdutivaEntities())
            //    {
            //        olistapersonas = (from p in db.ActivaEvaluacion
            //                          select p).ToList();

            //    }
            //}
            

            //return Json( olistapersonas , JsonRequestBehavior.AllowGet);
        }




    }
