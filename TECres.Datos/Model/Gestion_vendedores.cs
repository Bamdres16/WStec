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
    
    public partial class Gestion_vendedores
    {
        public int administrador { get; set; }
        public int vendedor { get; set; }
        public int id { get; set; }
    
        public virtual Administrador Administrador1 { get; set; }
        public virtual Vendedor Vendedor1 { get; set; }
    }
}