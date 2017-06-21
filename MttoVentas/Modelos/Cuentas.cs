using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MttoVentas.Modelos
{
    public class Cuentas
    {
        public string turno { get; set; }
        public long folio { get; set; }
        public string serieFolio { get; set; }
        public string folioCuenta { get; set; }         // numcheque
        public string folioNotaConsumo { get; set; }
        public string fecha { get; set; }
        public bool cancelado { get; set; }
        public bool facturado { get; set; }
        public decimal descuento { get; set; }
        public decimal totalOriginal { get; set; }
        public int productosEliminados { get; set; }
        public decimal totalDesc { get; set; }
        public decimal efectivo { get; set; }
        public decimal tarjeta { get; set; }
        public decimal vales { get; set; }
        public decimal otros { get; set; }
        public bool eliminar { get; set; }
        public bool modificar { get; set; }
    }
}
