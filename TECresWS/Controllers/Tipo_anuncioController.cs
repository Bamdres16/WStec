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
    public class Tipo_anuncioController : ApiController
    {
        private TECresEntities db = new TECresEntities();

        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        // GET: api/Tipo_anuncio
        public object GetTipo_anuncio()
        {
            return from ta in db.Tipo_anuncio
                   select new { ta.descripcion, ta.nombre, ta.costo_diario, ta.id_anuncio };
        }

        // GET: api/Tipo_anuncio/5
        [ResponseType(typeof(Tipo_anuncio))]
        public IHttpActionResult GetTipo_anuncio(string id)
        {
            Tipo_anuncio tipo_anuncio = db.Tipo_anuncio.Find(id);
            if (tipo_anuncio == null)
            {
                return NotFound();
            }

            return Ok(tipo_anuncio);
        }

        // PUT: api/Tipo_anuncio/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipo_anuncio(string id, Tipo_anuncio tipo_anuncio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipo_anuncio.nombre)
            {
                return BadRequest();
            }

            db.Entry(tipo_anuncio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_anuncioExists(id))
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

        // POST: api/Tipo_anuncio
        [ResponseType(typeof(Tipo_anuncio))]
        public IHttpActionResult PostTipo_anuncio(Tipo_anuncio tipo_anuncio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tipo_anuncio.Add(tipo_anuncio);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Tipo_anuncioExists(tipo_anuncio.nombre))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tipo_anuncio.nombre }, tipo_anuncio);
        }

        // DELETE: api/Tipo_anuncio/5
        [ResponseType(typeof(Tipo_anuncio))]
        public IHttpActionResult DeleteTipo_anuncio(string id)
        {
            try
            {
                Tipo_anuncio tipo_anuncio = db.Tipo_anuncio.Find(id);
                if (tipo_anuncio == null)
                {
                    return NotFound();
                }


                db.Tipo_anuncio.Remove(tipo_anuncio);

                db.SaveChanges();

                return Ok();
            } catch (Exception ex)
            {

                return BadRequest(ex.InnerException.InnerException.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tipo_anuncioExists(string id)
        {
            return db.Tipo_anuncio.Count(e => e.nombre == id) > 0;
        }
    }
}