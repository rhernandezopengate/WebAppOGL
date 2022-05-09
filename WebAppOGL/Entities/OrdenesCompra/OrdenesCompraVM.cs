using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppOGL.Entities.OrdenesCompra
{
    public class OrdenesCompraVM
    {
        
    }

    public partial class oc_ordenescompras 
    {
        public string FechaAltaString { get; set; }

        public string Proveedor { get; set; }

        public string CentroCostos { get; set; }

        public string Cuenta { get; set; }

        public string Solicitante { get; set; }

        public string StatusCompras { get; set; }

        public string StatusDirector { get; set; }

        public string StatusFinanzas { get; set; }

        public string StatusSupervisor { get; set; }
    }

    public class encabezadooc 
    {
        public int ProveedorId { get; set; }

        public int CuentaId { get; set; }

        public int CentroCostosId { get; set; }

        public int SubCentroCostosId { get; set; }

        public int LugarEntregaId { get; set; }

        public int CategoriaId { get; set; }

        public int TipoCompraId { get; set; }

        public int DivisaId { get; set; }

        public int FormaPagoId { get; set; }

        public string Proyecto { get; set; }

        public string Justificacion { get; set; }
    }

    public class detalleproductos 
    {
        public int Id { get; set; }

        public string codigo { get; set; }
        
        public string producto { get; set; }

        public int cantidad { get; set; }

        public decimal precio { get; set; }

        public decimal subtotal { get; set; }
    }
}