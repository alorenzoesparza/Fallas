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
    public class ActsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Acts
        public async Task<ActionResult> Index()
        {
            return View(await db.Acts.ToListAsync());
        }

        // GET: Acts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var act = await db.Acts.FindAsync(id);

            if (act == null)
            {
                return HttpNotFound();
            }

            return View(act);
        }

        // GET: Acts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Act act)
        {
            if (ModelState.IsValid)
            {
                db.Acts.Add(act);
                await db.SaveChangesAsync();

                if (act.ImagenFile != null)
                {
                    var folder = "~/Content/Eventos";
                    var file = string.Format("Evento{0}", act.IdAct);
                    var file500 = string.Format("Evento{0}_{1}", act.IdAct, "500");

                    var respuesta = FilesHelper.UploadPhotoBackEnd(act.ImagenFile, folder, file, 200, 200);
                    var respuesta500 = FilesHelper.UploadPhotoBackEnd(act.ImagenFile, folder, file500, 500, 500);

                    var extension = Path.GetExtension(respuesta);
                    if (respuesta != null)
                    {
                        var pic = string.Format("{0}/{1}{2}", folder, file, extension);
                        var pic500 = string.Format("{0}/{1}{2}", folder, file500, extension);

                        act.Imagen = pic;
                        act.Imagen500 = pic500;

                        db.Entry(act).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");

            }

            return View(act);
        }

        // GET: Acts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var act = await db.Acts.FindAsync(id);

            if (act == null)
            {
                return HttpNotFound();
            }

            return View(act);
        }

        // POST: Acts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "IdAct,Titular,Descripcion,FechaActo,HoraActo,Precio,PrecioInfantiles,ActoOficial,Imagen,Imagen500,PagInicio,YaEfectuado")] Act act)
        public async Task<ActionResult> Edit(Act act)
        {
            if (ModelState.IsValid)
            {
                if (act.ImagenFile != null)
                {
                    var folder = "~/Content/Eventos";
                    var file = string.Format("Evento{0}", act.IdAct);
                    var file500 = string.Format("Evento{0}_{1}", act.IdAct, "500");

                    var respuesta = FilesHelper.UploadPhotoBackEnd(act.ImagenFile, folder, file, 200, 200);
                    var respuesta500 = FilesHelper.UploadPhotoBackEnd(act.ImagenFile, folder, file500, 500, 500);

                    var extension = Path.GetExtension(respuesta);
                    if (respuesta != null)
                    {
                        var pic = string.Format("{0}/{1}{2}", folder, file, extension);
                        var pic500 = string.Format("{0}/{1}{2}", folder, file500, extension);

                        act.Imagen = pic;
                        act.Imagen500 = pic500;
                    }
                }

                //act.Precio = string.Format("{0:##0.00}", act.Precio);
                db.Entry(act).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(act);
        }

        // GET: Acts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var act = await db.Acts.FindAsync(id);

            if (act == null)
            {
                return HttpNotFound();
            }

            return View(act);
        }

        // POST: Acts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var act = await db.Acts.FindAsync(id);
            db.Acts.Remove(act);
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
