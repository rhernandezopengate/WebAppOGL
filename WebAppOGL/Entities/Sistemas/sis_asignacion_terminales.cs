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
    
    public partial class sis_asignacion_terminales
    {
        public int Id { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public int sis_terminales_Id { get; set; }
        public int adm_empleados_Id { get; set; }
        public int adm_area_Id { get; set; }
        public int adm_cuentas_Id { get; set; }
        public int adm_sucursales_Id { get; set; }
        public int oc_proveedores_Id { get; set; }
    
        public virtual sis_terminales sis_terminales { get; set; }
    }
}