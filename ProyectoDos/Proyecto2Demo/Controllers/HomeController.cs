using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto2Demo.Models;
using Proyecto2Demo.Models.DB;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

using System.Data.Entity;

namespace Proyecto2Demo.Controllers
{
    public class HomeController : Controller
    {

        //DBCONTEXT
        private SisEdutivaEntities1 db = new SisEdutivaEntities1();

        Registro nuevoRegistro;
        List<ActivaEvaluacionCRUD> olistapersonas;



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
            List<ActivaEvaluacionCRUD> obdata = new List<ActivaEvaluacionCRUD>();
            using (SisEdutivaEntities1 db = new SisEdutivaEntities1())
            {
                obdata = (from p in db.ActivaEvaluacionCRUD
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
        public JsonResult InsertarDato(string btnNota, string btnActivo, string btnProceso, string btnUsuario, string btnVisible)
        {
            try
            {
                using (SisEdutivaEntities1 db = new SisEdutivaEntities1())
                {
                    DateTime dt = DateTime.Now;

                    var fla1 = false;
                    var fla2 = false;

                    if (btnActivo == "1")
                    {
                        fla1 = true;
                    }

                    if (btnVisible == "1")
                    {
                        fla2 = true;
                    }

                    // Crear un objeto ActivaEvaluacion y establecer sus propiedades
                    ActivaEvaluacionCRUD dr = new ActivaEvaluacionCRUD
                    {
                        NotaCodigo = btnNota,
                        Activo = fla1,
                        ProcesoMatricula = Convert.ToInt32(btnProceso),
                        IdUsuario = Convert.ToInt32(btnUsuario),
                        FechaCreacion = dt,
                        Visible = fla2
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

        //DBCONTEXT db

        [HttpPost]
        public ActionResult Actualizar(ActivaEvaluacionCRUD modelo)
        {
            if (ModelState.IsValid)
            {
                // Obtener el objeto existente desde la base de datos
                var entidadExistente = db.ActivaEvaluacionCRUD.Find(modelo.IdActiva);

                if (entidadExistente != null)
                {
                    // Actualizar propiedades con los nuevos valores
                    entidadExistente.NotaCodigo = modelo.NotaCodigo;
                    entidadExistente.Activo = modelo.Activo;
                    entidadExistente.ProcesoMatricula = modelo.ProcesoMatricula;
                    entidadExistente.IdUsuario = modelo.IdUsuario;
                    entidadExistente.Visible = modelo.Visible;

                    // Guardar cambios en la base de datos
                    db.SaveChanges();

                    return Json(new { success = true, message = "Datos Actualizados correctamente a la tabla." });
                }
                else
                {
                    // Manejar el caso en que la entidad no existe
                    return Json(new { success = false, error = "La entidad no existe." });
                }
            }

            // Manejar errores de validación si es necesario
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }


        //METODO GET QUE A LA HORA DE HACER CLICK ME TRAIGA LOS DATOS
        [HttpGet]
        public ActionResult ObtenerDatos(int idActiva)
        {
            // Obtener datos del registro con el IdActiva proporcionado
            var datos = db.ActivaEvaluacionCRUD.Find(idActiva);

            if (datos != null)
            {
                // Devolver los datos como JSON
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Manejar el caso en que el registro no existe
                return HttpNotFound("El registro no fue encontrado");
            }
        }
    }

}