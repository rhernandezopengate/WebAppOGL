using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppOGL.Entities.Sistemas
{
    public class EquiposViewModel
    {
    }

    public partial class sis_equipos 
    {
        public string TipoEquipo { get; set; }

        public string Mantenimiento { get; set; }

        public string Marca { get; set; }
    }
}