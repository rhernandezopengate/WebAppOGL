using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppOGL.Entities.OrdenesCompra
{
    public class OrdenesCompraVM
    {
        
    }

    public class encabezadooc 
    {
        public string Folio { get; set; }
    }

    public class detalleproductos 
    {
        public string codigo { get; set; }
        
        public string producto { get; set; }

        public decimal cantidad { get; set; }

        public decimal precio { get; set; }

        public decimal subtotal { get; set; }
    }
}