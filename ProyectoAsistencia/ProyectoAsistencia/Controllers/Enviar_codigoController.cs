using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace ProyectoAsistencia.Controllers
{
    public class Enviar_codigoController : Controller
    {
        // GET: Enviar_codigo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EnviarCodigo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnviarCodigo(string receptor, string codigoIngresado = null)
        {
            if (string.IsNullOrEmpty(receptor))
            {
                ViewBag.Error = "El campo receptor es obligatorio.";
                return View();
            }

            string emisor = "ir4397148@gmail.com";
            string password = "mwht hetw llbj dkjf";

            // Enviar el código de verificación
            try
            {
                int codigoVerificacion = Enviar(emisor, password, receptor);
                TempData["CodigoVerificacion"] = codigoVerificacion;
                ViewBag.Mensaje = "El código de verificación ha sido enviado exitosamente.";
                return RedirectToAction("Index", "Asistencias");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Ocurrió un error al enviar el correo: {ex.Message}";
                return View();
            }

            TempData.Keep("CodigoVerificacion"); // Mantener el valor en TempData después de la redirección
        }

        private int Enviar(string emisor, string password, string receptor)
        {
            Random r = new Random();
            int numero = r.Next(100000, 1000000);
            MailMessage msg = new MailMessage();
            msg.To.Add(receptor);
            msg.Subject = "Correo de verificación";
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = $"Su código de verificación es {numero}. Por favor, ingréselo para continuar.";
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(emisor);

            SmtpClient client = new SmtpClient
            {
                Credentials = new NetworkCredential(emisor, password),
                Port = 587,
                EnableSsl = true,
                Host = "smtp.gmail.com"
            };

            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el correo electrónico: " + ex.Message);
            }

            return numero;
        }
    }

}
