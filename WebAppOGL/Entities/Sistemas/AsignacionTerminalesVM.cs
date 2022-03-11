using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppOGL.Entities.Sistemas
{
    public class AsignacionTerminalesVM
    {
    }

    public partial class sis_asignacion_terminales 
    {
        public string Cuenta { get; set; }

        public string Sucursal { get; set; }

        public string Proveedor { get; set; }

        public string Area { get; set; }

        public string Empleado { get; set; }
                
        public string Terminal { get; set; }

        public string Modelo { get; set; }

        public string FechaAltaString { get; set; }
    }
}