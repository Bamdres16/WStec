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

namespace TECresGeneral.Controllers
{
    public class UbicacionController : ApiController
    {
        private TECresEntities db = new TECresEntities();

        // GET: api/Ubicacion
        public IHttpActionResult Options()
        {
            HttpContext.Current.Response.AppendHeader("Allow", "GET,DELETE,PUT,POST,OPTIONS");
            return Ok();
        }
        
        public object GetCanton(string Provincia)
        {
            var cantones = db.Ubicacion
                            .Where(location => location.provincia == Provincia)
                            .Select(loca => loca.canton)
                            .Distinct()
                            .ToList();
                           
            return cantones;

        }

        public object GetDistritos(string Provincia, string Canton)
        {
            var distritos = db.Ubicacion
                            .Where(loca => loca.provincia == Provincia && loca.canton == Canton)
                            .Select(loca => new { loca.distrito, loca.id })
                            .ToList();
            return distritos;
        }


        public List<string> GetProvincias()
        {
            var distritos = db.Ubicacion
                            .Select(loca => loca.provincia)
                            .Distinct()
                            .ToList();
            return distritos;
        }
    }

}