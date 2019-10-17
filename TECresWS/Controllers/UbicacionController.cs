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
    public class UbicacionController : ApiController
    {
        private TECresEntities db = new TECresEntities();
        
        [Route("api/Ubicacion/Ubicaciones")]
        public object GetUbicaciones()
        {
            var cantones = from a in db.Ubicacion
                           select new { a.provincia, a.canton, a.distrito ,a.id};
            return cantones;

        }
        // GET: api/Ubicacion
        public List<string> GetCanton(string Provincia)
        {
            var cantones = db.Ubicacion
                            .Where(location => location.provincia == Provincia)
                            .Select(location => location.canton)
                            .Distinct()
                            .ToList();
            return cantones;

        }

        public List<string> GetDistritos(string Provincia, string Canton)
        {
            var distritos = db.Ubicacion
                            .Where(loca => loca.provincia == Provincia && loca.canton == Canton)
                            .Select(loca => loca.distrito)
                            .ToList();
            return distritos;
        }
        

        public List<string> GetProvincias()
        {
            var distritos = db.Ubicacion
                            .Select(loca => loca.provincia)
                            .Distinct()
                            .ToList();
            return distritos;
        }
        [HttpOptions]
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        // GET: api/Ubicacion/5
        [ResponseType(typeof(Ubicacion))]
        public IHttpActionResult GetUbicacion(int id)
        {
            Ubicacion ubicacion = db.Ubicacion.Find(id);
            if (ubicacion == null)
            {
                return NotFound();
            }

            return Ok(ubicacion);
        }

        // PUT: api/Ubicacion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUbicacion(int id, Ubicacion ubicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ubicacion.id)
            {
                return BadRequest();
            }

            db.Entry(ubicacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UbicacionExists(id))
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

        // POST: api/Ubicacion
        [ResponseType(typeof(Ubicacion))]
        public IHttpActionResult PostUbicacion(Ubicacion ubicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ubicacion.Add(ubicacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ubicacion.id }, ubicacion);
        }

        // DELETE: api/Ubicacion/5
        [ResponseType(typeof(Ubicacion))]
        public IHttpActionResult DeleteUbicacion(int id)
        {
            Ubicacion ubicacion = db.Ubicacion.Find(id);
            if (ubicacion == null)
            {
                return NotFound();
            }

            db.Ubicacion.Remove(ubicacion);
            db.SaveChanges();

            return Ok(ubicacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UbicacionExists(int id)
        {
            return db.Ubicacion.Count(e => e.id == id) > 0;
        }
    }
}