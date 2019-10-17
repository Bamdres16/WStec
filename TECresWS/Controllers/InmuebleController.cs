using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TECres.Datos.Model;

namespace TECresWS.Controllers
{
    public class InmuebleController : ApiController
    {
        private TECresEntities db = new TECresEntities();
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        [HttpGet]
        public object getInmuebles()
        {
            var inmuebles = from i in db.Tipo_Inmueble
                            select new { i.Tipo, i.definido_por };
            return inmuebles;
        }
        [ResponseType(typeof(Tipo_Inmueble))]
        public IHttpActionResult PostAdministrador(Tipo_Inmueble tipo_Inmueble)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tipo_Inmueble.Add(tipo_Inmueble);
            db.SaveChanges();

            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult DeleteInmueble(string id)
        {
            Tipo_Inmueble inmueble = db.Tipo_Inmueble.Find(id);
            if (inmueble == null)
            {
                return NotFound();
            }

            db.Tipo_Inmueble.Remove(inmueble);
            db.SaveChanges();

            return Ok(inmueble);
        }
    }
}
