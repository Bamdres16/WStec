using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TECres.Datos.Model;

namespace TECresGeneral.Controllers
{
    public class OcupacionController : ApiController
    {
        private TECresEntities db = new TECresEntities();

        // GET: api/Ocupacion
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        public object GetOcupaciones()
        {
           
            var cantones =
                    from a in db.Ocupacion
                    select new { a.nombre, a.identificador };
            return cantones;

        }

    }
}