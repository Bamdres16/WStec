using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TECres.Datos.Model;

namespace TECresWS.Controllers
{
    public class AprobarAnunciosController : ApiController
    {
        private TECresEntities db = new TECresEntities();
        [HttpGet]
        public object getAprobar()
        {
            var anuncios = db.Anunciosporaprobar();
            return anuncios;
        }
        [HttpPut]
        public IHttpActionResult PutAnuncio(int id, Anuncio anuncio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != anuncio.id_propiedad)
            {
                return BadRequest();
            }

            db.Entry(anuncio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnuncioExists(id))
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
        private bool AnuncioExists(int id)
        {
            return db.Anuncio.Count(e => e.id_propiedad == id) > 0;
        }
    }
}
