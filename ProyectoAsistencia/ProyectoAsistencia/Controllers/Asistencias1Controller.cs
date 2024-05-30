using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary1;

using System.Net.Mail;
using System.Text;


namespace ProyectoAsistencia.Controllers
{
    public class Asistencias1Controller : Controller
    {
        private GestionAcademicaEntities db = new GestionAcademicaEntities();

        // GET: Asistencias1
        public ActionResult Index()
        {
            var asistencias = db.Asistencias.Include(a => a.Aprendices);
            return View(asistencias.ToList());
        }

        // GET: Asistencias1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistencias asistencias = db.Asistencias.Find(id);
            if (asistencias == null)
            {
                return HttpNotFound();
            }
            return View(asistencias);
        }

        // GET: Asistencias1/Create
        public ActionResult Create()
        {
            ViewBag.aprendiz_id = new SelectList(db.Aprendices, "aprendiz_id", "nombre");
            return View();
        }

        // POST: Asistencias1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "asistencia_id,aprendiz_id,fecha,hora,tipo")] Asistencias asistencias)
        {
            if (ModelState.IsValid)
            {
                db.Asistencias.Add(asistencias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.aprendiz_id = new SelectList(db.Aprendices, "aprendiz_id", "nombre", asistencias.aprendiz_id);
            return View(asistencias);
        }

        // GET: Asistencias1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistencias asistencias = db.Asistencias.Find(id);
            if (asistencias == null)
            {
                return HttpNotFound();
            }
            ViewBag.aprendiz_id = new SelectList(db.Aprendices, "aprendiz_id", "nombre", asistencias.aprendiz_id);
            return View(asistencias);
        }

        // POST: Asistencias1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "asistencia_id,aprendiz_id,fecha,hora,tipo")] Asistencias asistencias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asistencias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.aprendiz_id = new SelectList(db.Aprendices, "aprendiz_id", "nombre", asistencias.aprendiz_id);
            return View(asistencias);
        }

        // GET: Asistencias1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistencias asistencias = db.Asistencias.Find(id);
            if (asistencias == null)
            {
                return HttpNotFound();
            }
            return View(asistencias);
        }

        // POST: Asistencias1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asistencias asistencias = db.Asistencias.Find(id);
            db.Asistencias.Remove(asistencias);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult EnviarCodigo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnviarCodigo(string codigoIngresado = null)
        {
            string emisor = "ir4397148@gmail.com";
            string password = "mwht hetw llbj dkjf";

            // Lista de correos a los cuales se enviará el código
            List<string> receptores = new List<string>
            {

                "luiseljaiek17@gmail.com"
                
                
        // Agrega aquí los correos adicionales
            };

            try
            {
                foreach (var receptor in receptores)
                {
                    Enviar(emisor, password, receptor);
                }

                ViewBag.Mensaje = "El código de verificación ha sido enviado exitosamente.";
                return RedirectToAction("Index", "Asistencias");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Ocurrió un error al enviar el correo: {ex.Message}";
                return View();
            }
        }

        private void Enviar(string emisor, string password, string receptor)
        {
            Random r = new Random();
            int numero = r.Next(100000, 1000000);

            // Definir la URL de redirección (puede ser cualquier URL que desees)
            string redireccionUrl = "https://localhost:44374/Aprendices/Index";

            // Definir el contenido HTML con CSS embebido y el botón de redirección
            string htmlContent = @"
    <!DOCTYPE html>
    <html>
    <head>
        <style>
            body {
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                margin: 0;
                padding: 0;
            }
            .container {
                max-width: 600px;
                margin: 50px auto;
                padding: 20px;
                background: #fff;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                border-radius: 8px;
                text-align: center;
            }
            h1 {
                color: #333;
            }
            p {
                font-size: 16px;
                line-height: 1.5;
                color: #666;
            }
            .code {
                font-size: 24px;
                font-weight: bold;
                color: #333;
                background: #e9ecef;
                border: 1px solid #ccc;
                display: inline-block;
                padding: 10px 20px;
                margin: 20px 0;
                border-radius: 4px;
            }
            .button {
                display: inline-block;
                padding: 10px 20px;
                font-size: 16px;
                font-weight: bold;
                color: #fff;
                background-color: #007bff;
                text-decoration: none;
                border-radius: 4px;
                margin-top: 20px;
            }
            .button:hover {
                background-color: #0056b3;
            }
            .footer {
                margin-top: 20px;
                font-size: 12px;
                color: #999;
            }
        </style>
    </head>
    <body>
        <div class='container'>
            <h1>Correo de Verificación</h1>
            <p>Estimado usuario,</p>
            <p>Para confirmar su asistencia, por favor ingrese el siguiente código de verificación en el sistema:</p>
            <div class='code'>" + numero + @"</div>
            <p>Este código es válido por los próximos 10 minutos.</p>
            <p>Si no solicitó este código, por favor ignore este correo.</p>
            <a href='" + redireccionUrl + @"' class='button'>Confirmar Asistencia</a>
            <div class='footer'>
                <p>Gracias,<br/>Equipo de Soporte</p>
            </div>
        </div>
    </body>
    </html>";

            MailMessage msg = new MailMessage();
            msg.To.Add(receptor);
            msg.Subject = "Correo de verificación";
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = htmlContent;
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
        }

    }
}
