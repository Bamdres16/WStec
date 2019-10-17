using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TECres.Datos;
using TECres.Datos.Model;

namespace TECresGeneral.Controllers
{
    public class ImageController : ApiController
    {
        
        private TECresEntities db = new TECresEntities();
        private string path = @"D:\Documentos\Api\TECres\Propiedades\";
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        [ResponseType(typeof(Picture))]
        public object postFotoSecundaria(Picture picture)
        {


            if (db.Propiedad.Find(picture.Id_propiedad) == null)
            {
                return NotFound();
            }
            AppPicture app = new AppPicture();
            int count = (from a in db.Foto_Secundaria
                         where a.id_propiedad == picture.Id_propiedad
                         select a.direccion).Count();
            string rute = app.guardar(picture, count);
            var foto = new Foto_Secundaria
            {
                id_propiedad = picture.Id_propiedad,
                direccion = rute
            };
            db.Foto_Secundaria.Add(foto);
            db.SaveChanges();
            return foto;
        }
        
        [HttpPost]
        [Route("api/Image/Principal")]
        public object postFotoPrincipal(Picture fotoprincipal)
        {


            if (db.Propiedad.Find(fotoprincipal.Id_propiedad) == null)
            {
                return NotFound();
            }
            AppPicture app = new AppPicture();
            
            string rute = app.guardar(fotoprincipal);
            var foto = new Foto_Secundaria
            {
                id_propiedad = fotoprincipal.Id_propiedad,
                direccion = rute
            };
            
            return foto;
        }
        [ResponseType(typeof(Picture))]
        public object getFoto(string Rute)
        {
            AppPicture app = new AppPicture();
            string base64 = app.convertir(path+ Rute);
            
            
            return new { base64 };

        }

        
        
    }
}
