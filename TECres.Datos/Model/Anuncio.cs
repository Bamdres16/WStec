//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TECres.Datos.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Anuncio
    {
        public string modalidad { get; set; }
        public System.DateTime inicio { get; set; }
        public System.DateTime fin { get; set; }
        public string tarjeta_credito { get; set; }
        public string titulo { get; set; }
        public int visitas { get; set; }
        public int vendedor { get; set; }
        public Nullable<int> aprobado_por { get; set; }
        public int publico { get; set; }
        public string tipo_anuncio { get; set; }
        public int id_cliente { get; set; }
        public int id_propiedad { get; set; }
        public double precio { get; set; }
    
        public virtual Administrador Administrador { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Propiedad Propiedad { get; set; }
        public virtual Publico Publico1 { get; set; }
        public virtual Tipo_anuncio Tipo_anuncio1 { get; set; }
        public virtual Vendedor Vendedor1 { get; set; }
    }
}
