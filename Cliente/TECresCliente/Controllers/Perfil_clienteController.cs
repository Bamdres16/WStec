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
    public class Perfil_clienteController : ApiController
    {
        private TECresEntities db = new TECresEntities();

        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        // GET: api/Perfil_cliente
        public object getperfiles()
        {
            var perfiles = db.Perfil_cliente
                            .Select(loca => new { loca.nombre, loca.id_perfil })
                            .Distinct()
                            .ToList();

            return perfiles;

        }
    }
}