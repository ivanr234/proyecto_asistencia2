using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary11;
using Proyecto_final.Models;
using Proyecto_final.Filters;
using System.Data.SqlClient;

namespace Proyecto_final.Controllers
{
    public class LoginController : Controller
    {
        private GestionAcademicaEntities db = new GestionAcademicaEntities();

        // GET: Login
        [NoCache]
        public ActionResult Index()
        {
            // Si el usuario ya está autenticado, redirige a su página correspondiente
            if (Session["usuarioAutenticado"] != null && (bool)Session["usuarioAutenticado"])
            {
                return RedirectToRol((string)Session["rolUsuario"]);
            }
            return View();
        }

        [NoCache]
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
                    Session["usuarioAutenticado"] = true;
                    Session["rolUsuario"] = resultado;

                    var aprendiz = ObtenerAprendizSesion(model.numero_documento);
                    if (aprendiz != null)
                    {
                        Session["nombreUsuario"] = aprendiz.nombre;
                        Session["apellidoUsuario"] = aprendiz.apellido;
                        Session["imagenUsuario"] = aprendiz.imagen;
                    }

                    return RedirectToRol(resultado);
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

        private Aprendices ObtenerAprendizSesion(string numeroDocumento)
        {
            // Buscar el aprendiz correspondiente al número de documento en la base de datos
            var aprendiz = db.Aprendices.FirstOrDefault(a => a.numero_documento == numeroDocumento);

            return aprendiz;
        }

        [NoCache]
        public ActionResult CerrarSesion()
        {
            // Limpiar la sesión
            Session.Clear();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            // Redirigir a la página de inicio de sesión
            return RedirectToAction("Index");
        }

        private ActionResult RedirectToRol(string rol)
        {
            switch (rol)
            {
                case "Administrador":
                    return RedirectToAction("Index", "Administrador");
                case "Aprendiz":
                    return RedirectToAction("Index", "Aprendices");
                case "Instructor":
                    return RedirectToAction("Index", "Instructores");
                default:
                    ViewBag.Error = "Rol no reconocido";
                    return View("Index");
            }
        }
    }
}