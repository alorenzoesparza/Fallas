using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fallas.Backend.Models;
using Fallas.Domain;
using Falla.Backend.Helpers;
using System.IO;

namespace Fallas.Backend.Controllers
{
    [Authorize(Roles = "Admin, Fallero")]
    public class EventosController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Eventos
        public async Task<ActionResult> Index()
        {
            //var actList = await db.Evento.ToListAsync();

            //foreach (var act in actList)
            //{
            //    var eventos = new Eventos
            //    {
            //        Descripcion = act.Descripcion,
            //        EventoOficial = act.EventoOficial,
            //        FechaEvento = act.FechaEvento,
            //        HoraEvento = act.HoraEvento,
            //        IdEvento = act.IdEvento,
            //        Imagen = act.Imagen,
            //        Imagen500 = act.Imagen500,
            //        PagInicio = act.PagInicio,
            //        Precio = act.Precio,
            //        PrecioInfantiles = act.PrecioInfantiles,
            //        Titular = act.Titular,
            //        YaEfectuado = act.YaEfectuado,
            //    };

            //    db.Eventos.Add(eventos);
            //    await db.SaveChangesAsync();

            //}

            return View(await db.Eventos.ToListAsync());
        }

        // GET: Eventos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var evento = await db.Eventos.FindAsync(id);

            if (evento == null)
            {
                return HttpNotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Evento evento)
        {
            if (ModelState.IsValid)
            {
                if (evento.ImagenFile != null)
                {
                    var folder = "~/Content/Eventos";
                    var file = string.Format("Evento{0}", evento.IdEvento);
                    var file500 = string.Format("Evento{0}_{1}", evento.IdEvento, "500");

                    var respuesta = FilesHelper.UploadPhotoBackEnd(evento.ImagenFile, folder, file, evento.Imagen, 200, 200);
                    var respuesta500 = FilesHelper.UploadPhotoBackEnd(evento.ImagenFile, folder, file500, evento.Imagen500, 500, 500);

                    var extension = Path.GetExtension(respuesta);
                    if (respuesta != null)
                    {
                        var pic = string.Format("{0}/{1}{2}", folder, file, extension);
                        var pic500 = string.Format("{0}/{1}{2}", folder, file500, extension);

                        evento.Imagen = pic;
                        evento.Imagen500 = pic500;

                        //db.Entry(evento).State = EntityState.Modified;
                        //db.SaveChanges();
                    }
                }

                db.Eventos.Add(evento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var evento = await db.Eventos.FindAsync(id);

            if (evento == null)
            {
                return HttpNotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Evento evento)
        {
            if (ModelState.IsValid)
            {
                if (evento.ImagenFile != null)
                {
                    var folder = "~/Content/Eventos";
                    var file = string.Format("Evento{0}", evento.IdEvento);
                    var file500 = string.Format("Evento{0}_{1}", evento.IdEvento, "500");

                    var respuesta = FilesHelper.UploadPhotoBackEnd(evento.ImagenFile, folder, file, evento.Imagen, 200, 200);
                    var respuesta500 = FilesHelper.UploadPhotoBackEnd(evento.ImagenFile, folder, file500, evento.Imagen500, 500, 500);

                    var extension = Path.GetExtension(respuesta);
                    if (respuesta != null)
                    {
                        var pic = string.Format("{0}/{1}{2}", folder, file, extension);
                        var pic500 = string.Format("{0}/{1}{2}", folder, file500, extension);

                        evento.Imagen = pic;
                        evento.Imagen500 = pic500;
                    }
                }

                db.Entry(evento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var evento = await db.Eventos.FindAsync(id);

            if (evento == null)
            {
                return HttpNotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var evento = await db.Eventos.FindAsync(id);

            db.Eventos.Remove(evento);
            await db.SaveChangesAsync();
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
    }
}
