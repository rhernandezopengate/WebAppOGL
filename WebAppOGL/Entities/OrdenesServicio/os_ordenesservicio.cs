//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppOGL.Entities.OrdenesServicio
{
    using System;
    using System.Collections.Generic;
    
    public partial class os_ordenesservicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public os_ordenesservicio()
        {
            this.os_detos_conceptos = new HashSet<os_detos_conceptos>();
            this.os_facturas_compras = new HashSet<os_facturas_compras>();
            this.os_facturas_trafico = new HashSet<os_facturas_trafico>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }
        public string Folio { get; set; }
        public Nullable<System.DateTime> FechaSolicitud { get; set; }
        public Nullable<int> Semana { get; set; }
        public string Remision { get; set; }
        public Nullable<decimal> CostoVenta { get; set; }
        public string Observaciones { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> IVA { get; set; }
        public Nullable<decimal> Retencion { get; set; }
        public Nullable<decimal> Total { get; set; }
        public int adm_empleados_Id { get; set; }
        public int os_statuscompras_Id { get; set; }
        public int adm_cuentas_Id { get; set; }
        public int oc_proveedores_Id { get; set; }
        public int os_rutas_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<os_detos_conceptos> os_detos_conceptos { get; set; }
        public virtual os_rutas os_rutas { get; set; }
        public virtual os_statuscompras os_statuscompras { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<os_facturas_compras> os_facturas_compras { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<os_facturas_trafico> os_facturas_trafico { get; set; }
    }
}
