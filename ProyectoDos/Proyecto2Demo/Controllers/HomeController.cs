using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto2Demo.Models;
using Proyecto2Demo.Models.DB;

namespace Proyecto2Demo.Controllers
{
    public class HomeController : Controller
    {
        private SisEdutivaEntities db = new SisEdutivaEntities();
        Registro nuevoRegistro;
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

            if (user == username && pass == password)
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

        //public JsonResult agregar(string Evaluacion)
        //{

        //    using (SisEdutivaEntities db = new SisEdutivaEntities())
        //    {
        //        var ActivaEvaluacion = new ActivaEvaluacion();

        //        ActivaEvaluacion.NotaCodigo = Evaluacion;
        //        ActivaEvaluacion.Activo = bool.Parse(Evaluacion);
        //        ActivaEvaluacion.ProcesoMatricula = int.Parse(Evaluacion);
        //        ActivaEvaluacion.IdUsuario = int.Parse(Evaluacion);
        //        ActivaEvaluacion.Visible = bool.Parse(Evaluacion);

        //        db.ActivaEvaluacion.Add(ActivaEvaluacion);
        //        db.SaveChanges();                                                                                                                                               

        //    }

        //    return Json(1, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult InsertarDato(string ntCredito)
        {
            try
            {
                using (SisEdutivaEntities db = new SisEdutivaEntities())
                {
                    DateTime dt = DateTime.Now;

                    // Crear un objeto ActivaEvaluacion y establecer sus propiedades
                    ActivaEvaluacion dr = new ActivaEvaluacion
                    {
                        NotaCodigo = ntCredito,
                        Activo = Convert.ToBoolean(ntCredito),
                        ProcesoMatricula = Convert.ToInt32(ntCredito),
                        IdUsuario = Convert.ToInt32(ntCredito),
                        FechaCreacion = dt,
                        Visible = Convert.ToBoolean(ntCredito)
                    };

                    // Llamar al procedimiento almacenado mediante Entity Framework
                    db.sp_RegistrarRegistro22(
                        dr.NotaCodigo,
                        dr.Activo,
                        dr.ProcesoMatricula,
                        dr.IdUsuario,
                        dr.FechaCreacion,
                        dr.Visible
                    );
                }

                return Json(new { success = true, message = "Datos ingresados correctamente a la tabla." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al insertar datos: " + ex.Message });
            }
        }


    }

}
