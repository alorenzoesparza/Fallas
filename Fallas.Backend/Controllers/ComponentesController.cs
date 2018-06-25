using Falla.Backend.Helpers;
using Fallas.Backend.Models;
using Fallas.Domain;
using System;
using System.Data.Entity;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fallas.Backend.Controllers
{
    [Authorize(Roles = "Admin, Fallero")]
    public class ComponentesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Componentes
        public async Task<ActionResult> Index()
        {
            //var components = await db.Components.ToListAsync();

            //foreach (var component in components)
            //{
            //    var componente = new Componente
            //    {
            //        Apellidos = component.LastName,
            //        ComponenteId = component.ComponentId,
            //        Email = component.Email,
            //        Foto = component.Foto,
            //        Foto500 = component.Foto500,
            //        Nombre = component.FirstName,
            //        Telefono = component.Telephone,
            //    };
            //    db.Componentes.Add(componente);
            //    await db.SaveChangesAsync();
            //}

            return View(await db.Componentes.ToListAsync());
        }

        // GET: Componentes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var componente = await db.Componentes.FindAsync(id);

            if (componente == null)
            {
                return HttpNotFound();
            }

            return View(componente);
        }

        // GET: Componentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Componentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ComponenteView view)
        {
            if (ModelState.IsValid)
            {
                var componente = this.ToComponente(view);
                db.Componentes.Add(componente);
                await db.SaveChangesAsync();
                UsersHelper.CreateUserASP(view.Email, "Fallero", view.Password);

                if (view.FotoFile != null)
                {
                    var folder = "~/Content/Componentes";
                    var file = string.Format("C{0}_{1}", componente.ComponenteId, DateTime.Now.ToString("ddMMyyyyHHmmss"));

                    var file500 = string.Format("C{0}_{1}_{2}", componente.ComponenteId, "500", DateTime.Now.ToString("ddMMyyyyHHmmss"));

                    var respuesta = FilesHelper.UploadPhotoBackEnd(view.FotoFile, folder, file, componente.Foto, 200, 200);
                    var respuesta500 = FilesHelper.UploadPhotoBackEnd(view.FotoFile, folder, file500, componente.Foto500, 500, 500);

                    var extension = Path.GetExtension(respuesta);
                    if (respuesta != null)
                    {
                        var pic = string.Format("{0}/{1}{2}", folder, file, extension);
                        var pic500 = string.Format("{0}/{1}{2}", folder, file500, extension);

                        componente.Foto = pic;
                        componente.Foto500 = pic500;

                        db.Entry(componente).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            return View(view);
        }
        private Componente ToComponente(ComponenteView view)
        {
            return new Componente
            {
                //ComponentId = view.ComponentId,
                Apellidos = view.Apellidos,
                ComponenteId = view.ComponenteId,
                Email = view.Email,
                Foto = view.Foto,
                Foto500 = view.Foto500,
                Nombre = view.Nombre,
                Telefono = view.Telefono,
            };
        }

        // GET: Componentes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var componente = await db.Componentes.FindAsync(id);

            if (componente == null)
            {
                return HttpNotFound();
            }

            return View(componente);
        }

        // POST: Componentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Componente componente)
        {
            if (ModelState.IsValid)
            {
                if (componente.FotoFile != null)
                {
                    var folder = "~/Content/Componentes";
                    var file = string.Format("C{0}_{1}", componente.ComponenteId,DateTime.Now.ToString("ddMMyyyyHHmmss"));

                    var file500 = string.Format("C{0}_{1}_{2}", componente.ComponenteId, "500", DateTime.Now.ToString("ddMMyyyyHHmmss"));

                    var respuesta = FilesHelper.UploadPhotoBackEnd(componente.FotoFile, folder, file, componente.Foto, 200, 200);
                    var respuesta500 = FilesHelper.UploadPhotoBackEnd(componente.FotoFile, folder, file500, componente.Foto500, 500, 500);

                    var extension = Path.GetExtension(respuesta);
                    if (respuesta != null)
                    {
                        var pic = string.Format("{0}/{1}{2}", folder, file, extension);
                        var pic500 = string.Format("{0}/{1}{2}", folder, file500, extension);

                        componente.Foto = pic;
                        componente.Foto500 = pic500;
                    }
                }

                db.Entry(componente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(componente);
        }

        // GET: Componentes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var componente = await db.Componentes.FindAsync(id);

            if (componente == null)
            {
                return HttpNotFound();
            }

            return View(componente);
        }

        // POST: Componentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var componente = await db.Componentes.FindAsync(id);
            db.Componentes.Remove(componente);
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
