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
    
    public partial class os_rutas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public os_rutas()
        {
            this.os_detrutas_destinos = new HashSet<os_detrutas_destinos>();
            this.os_ordenesservicio = new HashSet<os_ordenesservicio>();
        }
    
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int os_origen_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<os_detrutas_destinos> os_detrutas_destinos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<os_ordenesservicio> os_ordenesservicio { get; set; }
        public virtual os_origen os_origen { get; set; }
    }
}
