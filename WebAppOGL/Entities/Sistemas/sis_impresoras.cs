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
    
    public partial class sis_impresoras
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sis_impresoras()
        {
            this.sis_asignacion_impresoras = new HashSet<sis_asignacion_impresoras>();
        }
    
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Modelo_Toner { get; set; }
        public string Numero_Serie { get; set; }
        public string Numero_Parte { get; set; }
        public string MAC_Ethernet { get; set; }
        public string Hostname { get; set; }
        public int sis_marcas_Id { get; set; }
        public int sis_statusfiscal_Id { get; set; }
        public int sis_estatusequipo_Id { get; set; }
        public int sis_tipoimpresoras_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sis_asignacion_impresoras> sis_asignacion_impresoras { get; set; }
        public virtual sis_estatusequipo sis_estatusequipo { get; set; }
        public virtual sis_marcas sis_marcas { get; set; }
        public virtual sis_statusfiscal sis_statusfiscal { get; set; }
        public virtual sis_tipoimpresoras sis_tipoimpresoras { get; set; }
    }
}
