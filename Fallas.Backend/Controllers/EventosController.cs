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
            Evento evento = await db.Eventos.FindAsync(id);
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
        public async Task<ActionResult> Create([Bind(Include = "IdEvento,Titular,Descripcion,FechaEvento,HoraEvento,Precio,PrecioInfantiles,EventoOficial,Imagen,Imagen500,PagInicio,YaEfectuado")] Evento evento)
        {
            if (ModelState.IsValid)
            {
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
            Evento evento = await db.Eventos.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "IdEvento,Titular,Descripcion,FechaEvento,HoraEvento,Precio,PrecioInfantiles,EventoOficial,Imagen,Imagen500,PagInicio,YaEfectuado")] Evento evento)
        {
            if (ModelState.IsValid)
            {
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
            Evento evento = await db.Eventos.FindAsync(id);
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
            Evento evento = await db.Eventos.FindAsync(id);
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
