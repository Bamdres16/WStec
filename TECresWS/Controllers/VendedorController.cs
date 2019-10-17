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
    public class VendedorController : ApiController
    {
        private TECresEntities db = new TECresEntities();

        [Route("api/Vendedor/Vendedores")]
        public object GetVendedores()
        {
            var vendedores = from a in db.Vendedor
                             select new { a.fecha_ingreso, a.nombre, a.apellido1, a.apellido2, a.cedula, a.id_vendedor };
            return vendedores;
        }
        [ResponseType(typeof(IHttpActionResult))]
        public IHttpActionResult DeleteVendedor(int id)
        {
            Vendedor vendedor = db.Vendedor.Find(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            db.Vendedor.Remove(vendedor);
            db.SaveChanges();

            return Ok(vendedor);
        }
        // POST: api/Propiedad
        
        public IHttpActionResult PostVendedor(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vendedor.Add(vendedor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendedor.id_vendedor }, vendedor);
        }
        [HttpPut]
        public IHttpActionResult PutVendedor(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendedor.id_vendedor)
            {
                return BadRequest();
            }

            db.Entry(vendedor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendedorExist(id))
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
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        private bool VendedorExist(int id)
        {
            return db.Vendedor.Count(e => e.id_vendedor == id) > 0;
        }




    }     
}  