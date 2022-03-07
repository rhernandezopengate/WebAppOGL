//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppOGL.Entities.OrdenesCompra
{
    using System;
    using System.Collections.Generic;
    
    public partial class oc_ordenescompras
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public oc_ordenescompras()
        {
            this.oc_det_ordenes_productos = new HashSet<oc_det_ordenes_productos>();
        }
    
        public int Id { get; set; }
        public string Folio { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaPosibleEntrega { get; set; }
        public string Proyecto { get; set; }
        public string Justificacion { get; set; }
        public int oc_categoria_Id { get; set; }
        public int oc_formapago_Id { get; set; }
        public int oc_centrocostos_Id { get; set; }
        public int oc_subcentrocostos_Id { get; set; }
        public int oc_tipocompra_Id { get; set; }
        public int oc_lugarentrega_Id { get; set; }
        public int oc_divisa_Id { get; set; }
        public int oc_proveedores_Id { get; set; }
        public int adm_cuentas_Id { get; set; }
    
        public virtual oc_categoria oc_categoria { get; set; }
        public virtual oc_centrocostos oc_centrocostos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<oc_det_ordenes_productos> oc_det_ordenes_productos { get; set; }
        public virtual oc_divisa oc_divisa { get; set; }
        public virtual oc_formapago oc_formapago { get; set; }
        public virtual oc_lugarentrega oc_lugarentrega { get; set; }
        public virtual oc_proveedores oc_proveedores { get; set; }
        public virtual oc_subcentrocostos oc_subcentrocostos { get; set; }
        public virtual oc_tipocompra oc_tipocompra { get; set; }
    }
}