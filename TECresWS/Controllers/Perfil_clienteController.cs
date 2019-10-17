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
    public class Perfil_clienteController : ApiController
    {
        private TECresEntities db = new TECresEntities();

        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        // GET: api/Perfil_cliente
        public object GetPerfil_cliente()
        {
            var perfiles = from p in db.Perfil_cliente
                           select new { p.descripcion, p.nombre, p.incorporado_por, p.id_perfil };
            return perfiles;
        }

        // GET: api/Perfil_cliente/5
        [ResponseType(typeof(Perfil_cliente))]
        public IHttpActionResult GetPerfil_cliente(string id)
        {
            Perfil_cliente perfil_cliente = db.Perfil_cliente.Find(id);
            if (perfil_cliente == null)
            {
                return NotFound();
            }

            return Ok(perfil_cliente);
        }

        // PUT: api/Perfil_cliente/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerfil_cliente(string id, Perfil_cliente perfil_cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != perfil_cliente.nombre)
            {
                return BadRequest();
            }

            db.Entry(perfil_cliente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Perfil_clienteExists(id))
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

        // POST: api/Perfil_cliente
        [ResponseType(typeof(Perfil_cliente))]
        public IHttpActionResult PostPerfil_cliente(Perfil_cliente perfil_cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Perfil_cliente.Add(perfil_cliente);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Perfil_clienteExists(perfil_cliente.nombre))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = perfil_cliente.nombre }, perfil_cliente);
        }

        // DELETE: api/Perfil_cliente/5
        [ResponseType(typeof(Perfil_cliente))]
        public IHttpActionResult DeletePerfil_cliente(string id)
        {
            Perfil_cliente perfil_cliente = db.Perfil_cliente.Find(id);
            if (perfil_cliente == null)
            {
                return NotFound();
            }

            db.Perfil_cliente.Remove(perfil_cliente);
            db.SaveChanges();

            return Ok(perfil_cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Perfil_clienteExists(string id)
        {
            return db.Perfil_cliente.Count(e => e.nombre == id) > 0;
        }
    }
}