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
    
    public partial class os_detos_conceptos
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public Nullable<decimal> PrecioUnitario { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public int os_ordenesservicio_Id { get; set; }
        public int os_conceptos_Id { get; set; }
    
        public virtual os_conceptos os_conceptos { get; set; }
        public virtual os_ordenesservicio os_ordenesservicio { get; set; }
    }
}
