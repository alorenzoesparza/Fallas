using Falla.API.Models;
using Fallas.Domain;
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
    public class ActsController : ApiController
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: api/Acts
        public IQueryable<Act> GetActs()
        {
            return db.Acts;
        }

        // GET: api/Acts/5
        [ResponseType(typeof(Act))]
        public async Task<IHttpActionResult> GetAct(int id)
        {
            var act = await db.Acts.FindAsync(id);

            if (act == null)
            {
                return NotFound();
            }

            return Ok(act);
        }

        // PUT: api/Acts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAct(int id, Act act)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != act.IdAct)
            {
                return BadRequest();
            }

            db.Entry(act).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActExists(id))
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

        // POST: api/Acts
        [ResponseType(typeof(Act))]
        public async Task<IHttpActionResult> PostAct(Act act)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Acts.Add(act);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = act.IdAct }, act);
        }

        // DELETE: api/Acts/5
        [ResponseType(typeof(Act))]
        public async Task<IHttpActionResult> DeleteAct(int id)
        {
            var act = await db.Acts.FindAsync(id);

            if (act == null)
            {
                return NotFound();
            }

            db.Acts.Remove(act);
            await db.SaveChangesAsync();

            return Ok(act);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActExists(int id)
        {
            return db.Acts.Count(e => e.IdAct == id) > 0;
        }
    }
}