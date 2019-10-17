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
    public class AdministradorController : ApiController
    {
        private TECresEntities db = new TECresEntities();
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        // GET: api/Administrador
        public object GetAdministrador()
        {
            var admins = from a in db.Administrador
                         select new { a.nacimiento, a.ingreso, a.nombre, a.apellido1, a.apellido2, a.cedula, a.id_admin};
            List<object> adminis = new List<object>();
            foreach (var admin in admins)
            {
                adminis.Add(new
                {
                    admin.nacimiento,
                    admin.ingreso,
                    admin.nombre,
                    admin.apellido1,
                    admin.apellido2,
                    admin.cedula,
                    admin.id_admin,
                    edad = DateTime.Today.AddTicks(-admin.nacimiento.Date.Ticks).Year - 1
                });
            }
            return adminis;
        }
        [HttpGet]
        [Route("api/Administrador/admins")]
        public object GetAdmins (int cedula)
        {
            var admins = from a in db.Administrador
                       where a.cedula == cedula
                       select new { a.password, a.id_admin };
            if (admins == null)
            {
                return NotFound();
            }
            return admins;
        }

        // GET: api/Administrador/5
        [ResponseType(typeof(Administrador))]
        public IHttpActionResult GetAdministrador(int id)
        {
            Administrador administrador = db.Administrador.Find(id);
            if (administrador == null)
            {
                return NotFound();
            }

            return Ok(administrador);
        }

        // PUT: api/Administrador/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdministrador(int id, Administrador administrador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != administrador.id_admin)
            {
                return BadRequest();
            }

            db.Entry(administrador).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministradorExists(id))
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

        // POST: api/Administrador
        [ResponseType(typeof(Administrador))]
        public IHttpActionResult PostAdministrador(Administrador administrador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Administrador.Add(administrador);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = administrador.id_admin }, administrador);
        }

        // DELETE: api/Administrador/5
        [ResponseType(typeof(Administrador))]
        public IHttpActionResult DeleteAdministrador(int id)
        {
            Administrador administrador = db.Administrador.Find(id);
            if (administrador == null)
            {
                return NotFound();
            }

            db.Administrador.Remove(administrador);
            db.SaveChanges();

            return Ok(administrador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdministradorExists(int id)
        {
            return db.Administrador.Count(e => e.id_admin == id) > 0;
        }
    }
}