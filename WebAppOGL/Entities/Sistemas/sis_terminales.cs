//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppOGL.Entities.Sistemas
{
    using System;
    using System.Collections.Generic;
    
    public partial class sis_terminales
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string MAC { get; set; }
        public string NumeroSerie { get; set; }
        public int sis_marcas_Id { get; set; }
        public int sis_estatusequipo_Id { get; set; }
        public int sis_tipoequipos_Id { get; set; }
    
        public virtual sis_estatusequipo sis_estatusequipo { get; set; }
        public virtual sis_marcas sis_marcas { get; set; }
        public virtual sis_tipoequipos sis_tipoequipos { get; set; }
    }
}
