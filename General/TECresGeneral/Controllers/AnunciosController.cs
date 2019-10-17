using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TECres.Datos;
using TECres.Datos.Model;

namespace TECresGeneral.Controllers
{
    public class AnunciosController : ApiController
    {
        private TECresEntities db = new TECresEntities();
        private string path = @"D:\Documentos\Api\TECres\Propiedades\";
        [ResponseType(typeof(object))]
        [Route("api/Anuncios/Filtrar")]
        [HttpGet]
        public object Filtro(int habitaciones=0, int preciomin=0, int preciomax=0, int id_ubicacion=0)
        {
            AppPicture appPicture = new AppPicture();
            var anuncios = db.Filtros(habitaciones,preciomin, preciomax,id_ubicacion);
            string foto_p = "";
            List<object> obj = new List<object>();
            
            foreach (var anuncio in anuncios)
            {

                foto_p = appPicture.convertir(path + anuncio.foto_principal);
                obj.Add(new
                {
                    anuncio.provincia,
                    anuncio.canton,
                    anuncio.distrito,
                    anuncio.precio,
                    anuncio.tamano_construccion,
                    anuncio.tamano_terreno,
                    anuncio.id_propiedad,
                    anuncio.titulo,
                    foto_principal = foto_p
                });
            }
            return Ok(obj);
        }

        // GET: api/Anuncios
        [ResponseType(typeof(Anuncio))]
        public object GetAnuncios ()
        {
            AppPicture appPicture = new AppPicture();
            var anuncios =
                from a in db.Anuncio 
                join p in db.Propiedad
                on a.id_propiedad equals p.id_propiedad
                join u in db.Ubicacion
                on p.ubicacion equals u.id
                select new { u.provincia, u.canton, u.distrito, a.precio, p.tamano_construccion, p.tamano_terreno,
                    p.foto_principal, a.id_propiedad,a.titulo,p.id_dueno};
            string foto_p = "";
            List<object> obj = new List<object>();
            
            foreach (var anuncio in anuncios)
            {
                
                foto_p = appPicture.convertir(path + anuncio.foto_principal);
                obj.Add(new { anuncio.provincia, anuncio.canton, anuncio.distrito, anuncio.precio,
                              anuncio.tamano_construccion, anuncio.tamano_terreno,
                              anuncio.id_propiedad, anuncio.titulo, foto_principal = foto_p ,anuncio.id_dueno});
            }
            return Ok(obj);
        }



        // GET: api/Anuncios/5
        [ResponseType(typeof(Anuncio))]
        public object GetAnuncio(int id)
        {
            AppPicture appPicture = new AppPicture();
            var fotos = from a in db.Anuncio
                        where a.id_propiedad == id
                        join fs in db.Foto_Secundaria
                        on a.id_propiedad equals fs.id_propiedad
                        select fs.direccion;
            var anuncio =
                from a in db.Anuncio
                where a.id_propiedad == id
                join p in db.Propiedad
                on a.id_propiedad equals p.id_propiedad
                join u in db.Ubicacion
                on p.ubicacion equals u.id
                join v in db.Vendedor
                on a.vendedor equals v.id_vendedor
                select new { u.provincia, u.canton, u.distrito, a.precio, a.modalidad, a.visitas, vendedor = new { v.nombre, v.apellido1, v.apellido2 }, p.tamano_construccion, p.tamano_terreno,
                    p.gimnasio, p.inmueble, p.niveles, p.parqueo_visitas, p.piscina, p.foto_principal,
                    p.tipo_piso, p.cantidad_banos, p.cant_habitaciones, p.espacios_parqueo, fotos_secundarias = fotos, a.titulo };

            if (anuncio == null)
            {
                return NotFound();
            }

            string foto_p = "";
            List<string> fotos_s = new List<string>();
            object anuncio_o = new object();
            foreach (var v in anuncio)
            {
                foto_p = appPicture.convertir(path + v.foto_principal);
                foreach (string g in v.fotos_secundarias)
                {
                    fotos_s.Add(appPicture.convertir(path + g));
                }
                anuncio_o = new
                {
                    v.provincia,
                    v.canton,
                    v.distrito,
                    v.precio,
                    v.modalidad,
                    v.visitas,
                    v.vendedor,
                    v.tamano_construccion,
                    v.tamano_terreno,
                    v.gimnasio,
                    v.inmueble,
                    v.niveles,
                    v.parqueo_visitas,
                    v.piscina,
                    v.tipo_piso,
                    v.cantidad_banos,
                    v.cant_habitaciones,
                    v.espacios_parqueo,
                    v.titulo,
                    fotos_secundarias = fotos_s,
                    foto_principal = foto_p
                };
            }

            return Ok(anuncio_o);
        }

        // PUT: api/Anuncios/5
        [ResponseType(typeof(void))]
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

        // POST: api/Anuncios
        [ResponseType(typeof(Anuncio))]
        public IHttpActionResult PostAnuncio(Anuncio anuncio)

        {
            var Duracion = (from a in db.Tipo_anuncio 
                            where a.nombre == anuncio.tipo_anuncio
                            select a.Duracion);
                          
            foreach (int d in Duracion)
            {
                anuncio.fin = anuncio.inicio.Date.AddDays(7*d);
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Anuncio.Add(anuncio);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AnuncioExists(anuncio.id_propiedad))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = anuncio.id_propiedad }, anuncio);
        }

        // DELETE: api/Anuncios/5
        [ResponseType(typeof(Anuncio))]
        public IHttpActionResult DeleteAnuncio(int id)
        {
            Anuncio anuncio = db.Anuncio.Find(id);
            if (anuncio == null)
            {
                return NotFound();
            }

            db.Anuncio.Remove(anuncio);
            db.SaveChanges();

            return Ok(anuncio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnuncioExists(int id)
        {
            return db.Anuncio.Count(e => e.id_propiedad == id) > 0;
        }
    }
}