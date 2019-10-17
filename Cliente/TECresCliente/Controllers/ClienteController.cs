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

namespace TECresCliente.Controllers
{
    public class ClienteController : ApiController
    {
        private TECresEntities db = new TECresEntities();

        // GET: api/Cliente
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        [HttpGet]
        [Route("api/Cliente/login")]
        public HttpResponseMessage login(string correo, string pass)
        {

            var clienteexist = from a in db.Cliente
                                where a.correo == correo
                                select new { a.pass, a.correo, a.cedula, a.usuario };
            if (!clienteexist.Any())
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ese correo no existe");
            }
            foreach (var cliente in clienteexist)
            {
                if (cliente.pass != pass)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Password Incorrecto");
                }
                
            }
            return Request.CreateResponse(HttpStatusCode.OK, clienteexist);

        }
        public IQueryable<Cliente> GetCliente()
        {
            return db.Cliente;
        }

        // GET: api/Cliente/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult GetCliente(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Cliente/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCliente(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.cedula)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Cliente
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult PostCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cliente.Add(cliente);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(cliente.cedula))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cliente.cedula }, cliente);
        }

        // DELETE: api/Cliente/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult DeleteCliente(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.Cliente.Remove(cliente);
            db.SaveChanges();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return db.Cliente.Count(e => e.cedula == id) > 0;
        }
    }
}