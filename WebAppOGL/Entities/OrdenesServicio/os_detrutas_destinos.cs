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
    
    public partial class os_detrutas_destinos
    {
        public int Id { get; set; }
        public int os_rutas_Id { get; set; }
        public int os_destinos_Id { get; set; }
    
        public virtual os_rutas os_rutas { get; set; }
        public virtual os_destinos os_destinos { get; set; }
    }
}
