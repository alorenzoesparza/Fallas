using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Falla.API.Models;
using Newtonsoft.Json.Linq;

namespace Falla.API.Controllers
{
    [RoutePrefix("api/AsistenciaEventos")]
    [Authorize(Roles = "Admin, Fallero")]
    public class AsistenciaEventosController : ApiController
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: api/AsistenciaEventos
        public IQueryable<AsistenciaEvento> GetAsistenciaEventos()
        {
            return db.AsistenciaEventos;
        }

        // GET: api/AsistenciaEventos/5
        // Borrar AsistenciaEventos
        [ResponseType(typeof(AsistenciaEvento))]
        public async Task<IHttpActionResult> GetAsistenciaEvento(int id)
        {
            AsistenciaEvento asistenciaEvento = await db.AsistenciaEventos.FindAsync(id);
            if (asistenciaEvento == null)
            {
                return NotFound();
            }

            db.AsistenciaEventos.Remove(asistenciaEvento);
            await db.SaveChangesAsync();

            return Ok(asistenciaEvento);
        }

        // PUT: api/AsistenciaEventos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAsistenciaEvento(int id, AsistenciaEvento asistenciaEvento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asistenciaEvento.AsistenciaEventoId)
            {
                return BadRequest();
            }

            db.Entry(asistenciaEvento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsistenciaEventoExists(id))
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

        //DELETE: api/AsistenciaEventos/5
        [ResponseType(typeof(AsistenciaEvento))]
        public async Task<IHttpActionResult> DeleteAsistenciaEventos(int id)
        {
            var asistenciaEvento = await db.AsistenciaEventos.FindAsync(id);

            db.AsistenciaEventos.Remove(asistenciaEvento);
            await db.SaveChangesAsync();

            return Ok(asistenciaEvento);
        }

        // DELETE: api/AsistenciaEventos/Borrar/5
        [HttpPost]
        [Route("Borrar")]
        [ResponseType(typeof(AsistenciaEvento))]
        public async Task<IHttpActionResult> Borrar(AsistenciaEvento asistenciaEvento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AsistenciaEventos.Remove(asistenciaEvento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = asistenciaEvento.AsistenciaEventoId }, asistenciaEvento);

            //var asistencia = await db.AsistenciaEventos.FindAsync(id);

            //if (asistencia == null)
            //{
            //    return NotFound();
            //}

            //db.AsistenciaEventos.Remove(asistenciaEvento);
            //await db.SaveChangesAsync();

            //return Ok(asistenciaEvento);
        }

        // POST: api/AsistenciaEventos
        [ResponseType(typeof(AsistenciaEvento))]
        public async Task<IHttpActionResult> PostAsistenciaEvento(AsistenciaEvento asistenciaEvento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AsistenciaEventos.Add(asistenciaEvento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = asistenciaEvento.AsistenciaEventoId }, asistenciaEvento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsistenciaEventoExists(int id)
        {
            return db.AsistenciaEventos.Count(e => e.AsistenciaEventoId == id) > 0;
        }
    }
}