using Falla.API.Helpers;
using Falla.API.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Description;

namespace Falla.API.Controllers
{
    public class ComponentesController : ApiController
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: api/Componentes
        public IQueryable<Componente> GetComponentes()
        {
            return db.Componentes;
        }

        // GET: api/Componentes/5
        [ResponseType(typeof(Componente))]
        public async Task<IHttpActionResult> GetComponente(int id)
        {
            var componente = await db.Componentes.FindAsync(id);

            if (componente == null)
            {
                return NotFound();
            }

            return Ok(componente);
        }

        // PUT: api/Componentes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComponente(int id, Componente componente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != componente.ComponenteId)
            {
                return BadRequest();
            }

            if (componente.ImageArray != null && componente.ImageArray.Length > 0)
            {

                var componenteFoto = WebConfigurationManager.AppSettings["RutaFotos"];
                var stream = new MemoryStream(componente.ImageArray);
                var folder = "~/Content/Componentes";
                var file = string.Format("Componente{0}_{1}.jpg", componente.ComponenteId, DateTime.Now.ToString("ddMMyyyyHHmmss"));
                var file500 = string.Format("Componente{0}_{1}_{2}.jpg", componente.ComponenteId, "500", DateTime.Now.ToString("ddMMyyyyHHmmss"));

                var respuesta = FilesHelper.UploadPhotoStream(stream, folder, file, componente.Foto);

                if (respuesta)
                {
                    var pic = string.Format("{0}/{1}", folder, file);
                    var pic500 = string.Format("{0}/{1}", folder, file500);

                    componente.Foto = pic;
                    componente.Foto500 = pic;

                    //db.Entry(componente).State = EntityState.Modified;
                    //await db.SaveChangesAsync();
                }
            }

            db.Entry(componente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponenteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Componentes
        [ResponseType(typeof(Componente))]
        public async Task<IHttpActionResult> PostComponente(Componente componente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Componentes.Add(componente);
            await db.SaveChangesAsync();
            UsersHelper.CreateUserASP(componente.Email, "Fallero", componente.Password);

            if (componente.ImageArray != null && componente.ImageArray.Length > 0)
            {

                var componenteEmail = await db.Componentes.Where(
                    c => c.Email == componente.Email)
                    .FirstOrDefaultAsync();

                var stream = new MemoryStream(componente.ImageArray);
                var folder = "~/Content/Componentes";
                var file = string.Format("Componente{0}_{1}.jpg", componente.ComponenteId, DateTime.Now.ToString("ddMMyyyyHHmmss"));
                var file500 = string.Format("Componente{0}_{1}_{2}.jpg", componente.ComponenteId, "500", DateTime.Now.ToString("ddMMyyyyHHmmss"));

                var respuesta = FilesHelper.UploadPhotoStream(stream, folder, file, null);

                if (respuesta)
                {
                    var pic = string.Format("{0}/{1}", folder, file);
                    var pic500 = string.Format("{0}/{1}", folder, file500);

                    componente.Foto = pic;
                    componente.Foto500 = pic;

                    db.Entry(componente).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = componente.ComponenteId }, componente);
        }

        // DELETE: api/Componentes/5
        [ResponseType(typeof(Componente))]
        public async Task<IHttpActionResult> DeleteComponente(int id)
        {
            var componente = await db.Componentes.FindAsync(id);

            if (componente == null)
            {
                return NotFound();
            }

            db.Componentes.Remove(componente);
            await db.SaveChangesAsync();

            return Ok(componente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComponenteExists(int id)
        {
            return db.Componentes.Count(e => e.ComponenteId == id) > 0;
        }
    }
}