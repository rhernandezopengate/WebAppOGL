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
    
    public partial class sis_equipos
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Fecha_Alta { get; set; }
        public Nullable<System.DateTime> Fecha_Compra { get; set; }
        public string Modelo { get; set; }
        public string Numero_Serie { get; set; }
        public string Numero_Parte { get; set; }
        public string Nombre_Equipo { get; set; }
        public string MAC_Ethernet { get; set; }
        public string MAC_WiFi { get; set; }
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string STORAGE { get; set; }
        public int sis_marcas_Id { get; set; }
        public int sis_tipoequipos_Id { get; set; }
        public int sis_mantenimiento_Id { get; set; }
        public string Color { get; set; }
        public string Cargador { get; set; }
        public Nullable<int> sis_estatusequipo_Id { get; set; }
        public string Pantalla { get; set; }
    
        public virtual sis_estatusequipo sis_estatusequipo { get; set; }
        public virtual sis_mantenimiento sis_mantenimiento { get; set; }
        public virtual sis_marcas sis_marcas { get; set; }
        public virtual sis_tipoequipos sis_tipoequipos { get; set; }
    }
}
