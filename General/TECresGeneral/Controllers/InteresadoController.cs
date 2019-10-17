using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TECres.Datos.Model;

namespace TECresGeneral.Controllers
{
    public class InteresadoController : ApiController
    {
        private TECresEntities db = new TECresEntities();

        // GET: api/Interesado
        public IQueryable<Interesado> GetInteresado()
        {
            return db.Interesado;
        }
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }

        // GET: api/Interesado/5
        [ResponseType(typeof(Interesado))]
        public IHttpActionResult GetInteresado(int id)
        {
            Interesado interesado = db.Interesado.Find(id);
            if (interesado == null)
            {
                return NotFound();
            }

            return Ok(interesado);
        }

        // PUT: api/Interesado/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInteresado(int id, Interesado interesado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != interesado.id)
            {
                return BadRequest();
            }

            db.Entry(interesado).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InteresadoExists(id))
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

        // POST: api/Interesado
        [ResponseType(typeof(Interesado))]
        public IHttpActionResult PostInteresado(Interesado interesado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Interesado.Add(interesado);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = interesado.id }, interesado);
        }

        // DELETE: api/Interesado/5
        [ResponseType(typeof(Interesado))]
        public IHttpActionResult DeleteInteresado(int id)
        {
            Interesado interesado = db.Interesado.Find(id);
            if (interesado == null)
            {
                return NotFound();
            }

            db.Interesado.Remove(interesado);
            db.SaveChanges();

            return Ok(interesado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InteresadoExists(int id)
        {
            return db.Interesado.Count(e => e.id == id) > 0;
        }
    }
}