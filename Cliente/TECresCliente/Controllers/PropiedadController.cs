using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TECres.Datos;
using TECres.Datos.Model;

namespace TECresCliente.Controllers
{
    public class PropiedadController : ApiController
    {
        private TECresEntities db = new TECresEntities();
        private string path = @"D:\Documentos\Api\TECres\Propiedades\";

        // GET: api/Propiedad
        public IQueryable<Propiedad> GetPropiedad()
        {
            return db.Propiedad;
        }
        [HttpGet]
        [Route("api/Propiedad/Propiedades")]
        public object GetPropiedad_ID(int cedula)
        {
            AppPicture appPicture = new AppPicture();


            
            var Propiedades_Cliente = from pc in db.Propiedad
                                      where pc.id_dueno == cedula
                                      select new
                                      {
                                          pc.tamano_terreno,
                                          pc.niveles,
                                          pc.tipo_piso,
                                          pc.piscina,
                                          pc.gimnasio,
                                          pc.cantidad_banos,
                                          pc.cant_habitaciones,
                                          pc.espacios_parqueo,
                                          pc.parqueo_visitas,
                                          pc.titulo,
                                          pc.ubicacion,
                                          pc.id_dueno,
                                          pc.inmueble,
                                          pc.id_propiedad,
                                          pc.foto_principal
                                          
                                          
                                      };
            string foto_p = "";
            List<object> obj = new List<object>();
            
            foreach (var pc in Propiedades_Cliente)
            {
                
                var fotos = db.Fotos_Secundarias(pc.id_propiedad);
               
                
                foto_p = appPicture.convertir(path + pc.foto_principal);
                obj.Add(new
                {
                    pc.tamano_terreno,
                    pc.niveles,
                    pc.tipo_piso,
                    pc.piscina,
                    pc.gimnasio,
                    pc.cantidad_banos,
                    pc.cant_habitaciones,
                    pc.espacios_parqueo,
                    pc.parqueo_visitas,
                    pc.titulo,
                    pc.ubicacion,
                    pc.id_dueno,
                    pc.inmueble,
                    pc.id_propiedad,
                    foto_principal = foto_p
                   
                }); ;
            }
            return obj;
        }
        [HttpGet]
        [Route("api/Propiedad/Anuncios")]
        public object getanuncios(int cedula)
        {
            AppPicture appPicture = new AppPicture();



            var anuncios = from pc in db.Anuncio
                                      join c in db.Cliente
                                      on pc.id_cliente equals c.cedula
                                      where c.cedula == cedula
                                      select new
                                      {
                                          pc.modalidad,
                                          pc.inicio,
                                          pc.fin,
                                          pc.tarjeta_credito,
                                          pc.titulo,
                                          pc.visitas,
                                          pc.vendedor,
                                          pc.publico,
                                          pc.tipo_anuncio,
                                          pc.precio,
                                          pc.id_cliente,
                                          pc.id_propiedad

                                      };
            
            return anuncios;
        }
        // GET: api/Propiedad/5
        [ResponseType(typeof(Propiedad))]
        public IHttpActionResult GetPropiedad(int id)
        {
            Propiedad propiedad = db.Propiedad.Find(id);
            if (propiedad == null)
            {
                return NotFound();
            }

            return Ok(propiedad);
        }

        // PUT: api/Propiedad/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropiedad(int id, Propiedad propiedad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propiedad.id_propiedad)
            {
                return BadRequest();
            }

            db.Entry(propiedad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropiedadExists(id))
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

        // POST: api/Propiedad
        [ResponseType(typeof(Propiedad))]
        public IHttpActionResult PostPropiedad(Propiedad propiedad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Propiedad.Add(propiedad);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = propiedad.id_propiedad }, propiedad);
        }

        // DELETE: api/Propiedad/5
        [ResponseType(typeof(Propiedad))]
        public IHttpActionResult DeletePropiedad(int id)
        {
            Propiedad propiedad = db.Propiedad.Find(id);
            if (propiedad == null)
            {
                return NotFound();
            }

            db.Propiedad.Remove(propiedad);
            db.SaveChanges();

            return Ok(propiedad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropiedadExists(int id)
        {
            return db.Propiedad.Count(e => e.id_propiedad == id) > 0;
        }
    }
}