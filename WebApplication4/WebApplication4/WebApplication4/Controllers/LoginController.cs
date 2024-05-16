using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using System.Data.SqlClient;
using ClassLibrary1;

namespace Login.Controllers
{
    public class LoginController : Controller
    {
        private GestionAcademicaEntities db = new GestionAcademicaEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UsuarioRolViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Llamar al nuevo procedimiento almacenado sp_IniciarSesion
                var resultado = db.Database.SqlQuery<string>(
                    "EXEC sp_IniciarSesion @numero_documento, @tipo_documento, @contraseña",
                    new SqlParameter("@numero_documento", model.numero_documento),
                    new SqlParameter("@tipo_documento", model.tipo_documento),
                    new SqlParameter("@contraseña", model.contraseña)
                ).FirstOrDefault();

                if (resultado != null)
                {
                    // Inicio de sesión exitoso
                    switch (resultado)
                    {
                        case "Administrador":
                            // Redirigir a la vista correspondiente para los administradores
                            return RedirectToAction("Index", "lovi");

                        case "Aprendiz":
                            // Redirigir a la vista correspondiente para los aprendices
                            return RedirectToAction("Index", "Aprendices");

                        case "Instructor":
                            // Redirigir a la vista correspondiente para los instructores
                            return RedirectToAction("Index", "Instructores");

                        default:
                            ViewBag.Error = "Rol no reconocido";
                            return View("Index");
                    }
                }
                else
                {
                    ViewBag.Error = "Credenciales incorrectas";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}

