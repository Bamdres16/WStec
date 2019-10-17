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

namespace TECresWS.Controllers
{
    public class OcupacionController : ApiController
    {
        private TECresEntities db = new TECresEntities();
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        // GET: api/Ocupacion
        public IQueryable<Ocupacion> GetOcupacion()
        {
            return db.Ocupacion;
        }

        // GET: api/Ocupacion/5
        [ResponseType(typeof(Ocupacion))]
        public IHttpActionResult GetOcupacion(int id)
        {
            Ocupacion ocupacion = db.Ocupacion.Find(id);
            if (ocupacion == null)
            {
                return NotFound();
            }

            return Ok(ocupacion);
        }

        // PUT: api/Ocupacion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOcupacion(int id, Ocupacion ocupacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ocupacion.identificador)
            {
                return BadRequest();
            }

            db.Entry(ocupacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OcupacionExists(id))
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

        // POST: api/Ocupacion
        [ResponseType(typeof(Ocupacion))]
        public IHttpActionResult PostOcupacion(Ocupacion ocupacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ocupacion.Add(ocupacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ocupacion.identificador }, ocupacion);
        }

        // DELETE: api/Ocupacion/5
        [ResponseType(typeof(Ocupacion))]
        public IHttpActionResult DeleteOcupacion(int id)
        {
            Ocupacion ocupacion = db.Ocupacion.Find(id);
            if (ocupacion == null)
            {
                return NotFound();
            }

            db.Ocupacion.Remove(ocupacion);
            db.SaveChanges();

            return Ok(ocupacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OcupacionExists(int id)
        {
            return db.Ocupacion.Count(e => e.identificador == id) > 0;
        }
    }
}