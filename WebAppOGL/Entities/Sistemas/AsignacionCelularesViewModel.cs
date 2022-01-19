using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppOGL.Entities.Sistemas
{
    public class AsignacionCelularesViewModel
    {
        public int Id { get; set; }

        public string FechaAlta { get; set; }

        public string Area { get; set; }

        public string Empleado { get; set; }

        public string Sucursal { get; set; }

        public string Cuenta { get; set; }

        public string ModeloCelular { get; set; }
    }

    public partial class sis_asignacion_celulares 
    {
        
    }
}