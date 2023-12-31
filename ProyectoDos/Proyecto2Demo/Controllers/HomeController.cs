﻿using System;
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



        ////DBCONTEXT
        private SisEdutivaEntities2 db = new SisEdutivaEntities2();

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
            using (SisEdutivaEntities2 db = new SisEdutivaEntities2())
            {
                obdata = (from p in db.ActivaEvaluacionCRUD
                          select p).ToList();
            }
            return Json(new { data = obdata }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult InsertarDato(string btnNota, string btnActivo, string btnProceso, string btnUsuario, string btnVisible)
        {
            try
            {
                using (SisEdutivaEntities2 db = new SisEdutivaEntities2())
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



        [HttpPost]
        public JsonResult Actualizar(ActivaEvaluacionCRUD modelo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("ActualizarActivaEvaluacion @IdActiva, @NotaCodigo, @Activo, @ProcesoMatricula, @IdUsuario, @Visible",
                        new SqlParameter("@IdActiva", modelo.IdActiva),
                        new SqlParameter("@NotaCodigo", modelo.NotaCodigo),
                        new SqlParameter("@Activo", modelo.Activo),
                        new SqlParameter("@ProcesoMatricula", modelo.ProcesoMatricula),
                        new SqlParameter("@IdUsuario", modelo.IdUsuario),
                        new SqlParameter("@Visible", modelo.Visible));

                    return Json(new { success = true, message = "Datos Actualizados correctamente a la tabla." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = "Error al actualizar los datos: " + ex.Message });
                }
            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        [HttpPost]
        public JsonResult EliminarActivaEvaluacion(int idActiva)
        {
            try
            {
                // Encuentra el registro en la base de datos
                var entididadEliminar = db.ActivaEvaluacionCRUD.Find(idActiva);

                if (entididadEliminar != null)
                {
                    //Elimina el registro
                    db.ActivaEvaluacionCRUD.Remove(entididadEliminar);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Registro eliminado correctamente. " });
                }
                else
                {
                    return Json(new { success = false, message = "No se encontro datos para eliminar. " });
                }


            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = $"Error al eliminar el registro: {ex.Message}" });
            }

        }




    }




}