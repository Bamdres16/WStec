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
    public class NacionalidadController : ApiController
    {
        private TECresEntities db = new TECresEntities();

        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        public object getNacionalidades()
        {
            var nacionalidades = db.Nacionalidad
                            .OrderBy(loca => loca.nombre)
                            .Select(loca => new { loca.nombre })
                            .ToList();

            return nacionalidades;

        }



    }
}