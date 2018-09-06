using Falla.API.Helpers;
using Falla.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
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
    //[RoutePrefix("api/Componentes")]
    public class ComponentesController : ApiController
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: api/Componentes
        public IQueryable<Componente> GetComponentes()
        {
            return db.Componentes;
        }

        [Authorize]
        [HttpPost]
        [Route("GetComponentePorEmail")]
        public async Task<IHttpActionResult> GetComponentePorEmail(JObject form)
        {
            var email = string.Empty;
            dynamic jsonObject = form;

            try
            {
                email = jsonObject.Email.Value;
            }
            catch 
            {

                return BadRequest("Acceso a GetComponentePorEmail incorrecto.");
            }

            var componente = await db.Componentes.
                Where(c => c.Email.ToLower() == email.ToLower()).
                FirstOrDefaultAsync();

            if (componente == null)
            {
                return NotFound();
            }

            return Ok(componente);
        }

        // PUT: api/Componentes/5
        [Authorize]
        [HttpPost]
        [ResponseType(typeof(Componente))]
        [Route("ModificarComponente")]
        public async Task<IHttpActionResult> ModificarComponente(Componente componente)
        {
            if (componente.ImageArray != null && componente.ImageArray.Length > 0)
            {

                var componenteFoto = WebConfigurationManager.AppSettings["RutaFotos"];
                var stream = new MemoryStream(componente.ImageArray);
                var folder = "~/Content/Componentes";
                var file = string.Format("C{0}_{1}.jpg", componente.ComponenteId, DateTime.Now.ToString("ddMMyyyyHHmmss"));
                var file500 = string.Format("C{0}_{1}_{2}.jpg", componente.ComponenteId, "500", DateTime.Now.ToString("ddMMyyyyHHmmss"));

                var respuesta = FilesHelper.UploadPhotoStream(stream, folder, file, componente.Foto, 200, 200);
                var respuesta500 = FilesHelper.UploadPhotoStream(stream, folder, file500, componente.Foto, 500, 500);

                if (respuesta)
                {
                    var pic = string.Format("{0}/{1}", folder, file);
                    var pic500 = string.Format("{0}/{1}", folder, file500);

                    componente.Foto = pic;
                    componente.Foto500 = pic;
                }
            }

            db.Entry(componente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponenteExists(componente.ComponenteId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(componente);
        }

        [HttpPost]
        [Route("CambiarPassword")]
        public async Task<IHttpActionResult> CambiarPassword(JObject form)
        {
            var email = string.Empty;
            var actualPassword = string.Empty;
            var nuevoPassword = string.Empty;
            dynamic jsonObject = form;

            try
            {
                email = jsonObject.Email.Value;
                actualPassword = jsonObject.ActualPassword.Value;
                nuevoPassword = jsonObject.NuevoPassword.Value;
            }
            catch
            {
                return BadRequest("Incorrect call");
            }

            var userContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.FindByEmail(email);

            if (userASP == null)
            {
                return BadRequest("Incorrect call");
            }

            var response = await userManager.ChangePasswordAsync(userASP.Id, actualPassword, nuevoPassword);
            if (!response.Succeeded)
            {
                return BadRequest(response.Errors.FirstOrDefault());
            }

            return Ok("ok");
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
                var file = string.Format("C{0}_{1}.jpg", componente.ComponenteId, DateTime.Now.ToString("ddMMyyyyHHmmss"));
                var file500 = string.Format("C{0}_{1}_{2}.jpg", componente.ComponenteId, "500", DateTime.Now.ToString("ddMMyyyyHHmmss"));

                //var respuesta = FilesHelper.UploadPhotoStream(stream, folder, file, null);
                var respuesta = FilesHelper.UploadPhotoStream(stream, folder, file, null, 200, 200);
                var respuesta500 = FilesHelper.UploadPhotoStream(stream, folder, file500, null, 500, 500);

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

        // PUT: api/Componentes/5
        [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComponente(int id, Componente componente)
        {
            if (componente.ImageArray != null && componente.ImageArray.Length > 0)
            {

                var componenteEmail = await db.Componentes.Where(
                    c => c.Email == componente.Email)
                    .FirstOrDefaultAsync();

                var stream = new MemoryStream(componente.ImageArray);
                var folder = "~/Content/Componentes";
                var file = string.Format("C{0}_{1}.jpg", componente.ComponenteId, DateTime.Now.ToString("ddMMyyyyHHmmss"));
                var file500 = string.Format("C{0}_{1}_{2}.jpg", componente.ComponenteId, "500", DateTime.Now.ToString("ddMMyyyyHHmmss"));

                //var respuesta = FilesHelper.UploadPhotoStream(stream, folder, file, null);
                var respuesta = FilesHelper.UploadPhotoStream(stream, folder, file, null, 200, 200);
                var respuesta500 = FilesHelper.UploadPhotoStream(stream, folder, file500, null, 500, 500);

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
                throw;
            }
            return Ok(componente);
        }

        // DELETE: api/Componentes/5
        [HttpDelete]
        [Authorize]
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