using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppOGL.Entities.OrdenesServicio
{
    public class OrdenesServicioVM
    {
    }

    public partial class os_ordenesservicio 
    {
        public string FechaSolicitudString { get; set; }

        public string Proveedor { get; set; }

        public string Ruta { get; set; }

        public string Utilidad { get; set; }
        
        public string Profit { get; set; }
    }
}