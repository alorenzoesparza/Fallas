using Falla.API.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Falla.API.Controllers
{
    [Authorize(Roles = "Admin, Fallero")]
    public class EventosController : ApiController
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: api/Eventos
        public IQueryable<Evento> GetEventos()
        {
            return db.Eventos;
        }

        // GET: api/Eventos/5
        [ResponseType(typeof(Evento))]
        public async Task<IHttpActionResult> GetEvento(int id)
        {
            var evento = await db.Eventos.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        // PUT: api/Eventos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEvento(int id, Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != evento.IdEvento)
            {
                return BadRequest();
            }

            db.Entry(evento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
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

        // POST: api/Eventos
        [ResponseType(typeof(Evento))]
        public async Task<IHttpActionResult> PostEvento(Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Eventos.Add(evento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = evento.IdEvento }, evento);
        }

        // DELETE: api/Eventos/5
        [ResponseType(typeof(Evento))]
        public async Task<IHttpActionResult> DeleteEvento(int id)
        {
            var evento = await db.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            db.Eventos.Remove(evento);
            await db.SaveChangesAsync();

            return Ok(evento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventoExists(int id)
        {
            return db.Eventos.Count(e => e.IdEvento == id) > 0;
        }
    }
}