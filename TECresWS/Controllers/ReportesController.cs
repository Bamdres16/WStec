using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TECres.Datos.Model;

namespace TECresWS.Controllers
{
    public class ReportesController : ApiController
    {
        private TECresEntities db = new TECresEntities();
        [HttpGet]
        [Route("api/Reportes/Ventas")]
        public object getVentas (DateTime inicio, DateTime fin)
        {
            var ventas = db.ReporteVentas(inicio, fin);
            return ventas;

        }
        [HttpGet]
        [Route("api/Reportes/PorVencer")]
        public object getVencer()
        {
            var porvencer = db.ReporteVencer();
            return porvencer;

        }
        [HttpGet]
        [Route("api/Reportes/Comisiones")]
        public object getComisiones()
        {
            var comisiones = db.Comisiones();
            return comisiones;

        }


    }
}
