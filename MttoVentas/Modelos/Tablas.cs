using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MttoVentas.Modelos
{
    public class TB_Folios
    {
        public string serie { get; set; }
        public long ultimofolio { get; set; }
        public long ultimaorden { get; set; }
        public long ultimofolionotadeconsumo { get; set; }
        public long ultimofolioproduccion { get; set; }
    }

    public class TB_Cheques
    {
        public long folio { get; set; }
        public string seriefolio { get; set; }
        public long numcheque { get; set; }
        public string fecha { get; set; }
        public string salidarepartidor { get; set; }
        public string arriborepartidor { get; set; }
        public string cierre { get; set; }
        public string mesa { get; set; }
        public int nopersonas { get; set; }
        public string idmesero { get; set; }
        public int pagado { get; set; }
        public int cancelado { get; set; }
        public int impreso { get; set; }
        public int impresiones { get; set; }
        public decimal cambio { get; set; }
        public int descuento { get; set; }
        public int reabiertas { get; set; }
        public string razoncancelado { get; set; }
        public int orden { get; set; }
        public int facturado { get; set; }
        public string idcliente { get; set; }
        public string idarearestaurant { get; set; }
        public string idempresa { get; set; }
        public int tipodeservicio { get; set; }
        public int idturno { get; set; }
        public string usuariocancelo { get; set; }
        public string comentariodescuento { get; set; }
        public string estacion { get; set; }
        public decimal cambiorepartidor { get; set; }
        public string usuariodescuento { get; set; }
        public string fechacancelado { get; set; }
        public string idtipodescuento { get; set; }
        public string numerotarjeta { get; set; }
        public int folionotadeconsumo { get; set; }
        public int notadeconsumo { get; set; }
        public int propinapagada { get; set; }
        public int propinafoliomovtocaja { get; set; }
        public decimal puntosmonederogenerados { get; set; }
        public decimal propinaincluida { get; set; }
        public string tarjetadescuento { get; set; }
        public int porcentajefac { get; set; }
        public string usuariopago { get; set; }
        public int propinamanual { get; set; }
        public string observaciones { get; set; }
        public string idclientedomicilio { get; set; }
        public string iddireccion { get; set; }
        public string idclientefacturacion { get; set; }
        public string telefonousadodomicilio { get; set; }
        public int totalarticulos { get; set; }
        public decimal subtotal { get; set; }
        public decimal subtotalsinimpuestos { get; set; }
        public decimal total { get; set; }
        public decimal totalconpropina { get; set; }
        public decimal totalsinimpuestos { get; set; }
        public decimal totalsindescuentosinimpuesto { get; set; }
        public decimal totalimpuesto1 { get; set; }
        public decimal totalalimentosconimpuestos { get; set; }
        public decimal totalbebidasconimpuestos { get; set; }
        public decimal totalotrosconimpuestos { get; set; }
        public decimal totalalimentossinimpuestos { get; set; }
        public decimal totalbebidassinimpuestos { get; set; }
        public decimal totalotrossinimpuestos { get; set; }
        public decimal totaldescuentossinimpuestos { get; set; }
        public decimal totaldescuentosconimpuestos { get; set; }
        public decimal totaldescuentoalimentosconimpuesto { get; set; }
        public decimal totaldescuentobebidasconimpuesto { get; set; }
        public decimal totaldescuentootrosconimpuesto { get; set; }
        public decimal totaldescuentoalimentossinimpuesto { get; set; }
        public decimal totaldescuentobebidassinimpuesto { get; set; }
        public decimal totaldescuentootrossinimpuesto { get; set; }
        public decimal totalcortesiassinimpuestos { get; set; }
        public decimal totalcortesiasconimpuestos { get; set; }
        public decimal totalcortesiaalimentosconimpuesto { get; set; }
        public decimal totalcortesiabebidasconimpuesto { get; set; }
        public decimal totalcortesiaotrosconimpuesto { get; set; }
        public decimal totalcortesiaalimentossinimpuesto { get; set; }
        public decimal totalcortesiabebidassinimpuesto { get; set; }
        public decimal totalcortesiaotrossinimpuesto { get; set; }
        public decimal totaldescuentoycortesiasinimpuesto { get; set; }
        public decimal totaldescuentoycortesiaconimpuesto { get; set; }
        public decimal cargo { get; set; }
        public decimal totalconcargo { get; set; }
        public decimal totalconpropinacargo { get; set; }
        public decimal descuentoimporte { get; set; }
        public decimal efectivo { get; set; }
        public decimal tarjeta { get; set; }
        public decimal vales { get; set; }
        public decimal otros { get; set; }
        public decimal propina { get; set; }
        public decimal propinatarjeta { get; set; }
        public decimal totalalimentossinimpuestossindescuentos { get; set; }
        public decimal totalbebidassinimpuestossindescuentos { get; set; }
        public decimal totalotrossinimpuestossindescuentos { get; set; }
        public string campoadicional1 { get; set; }
        public string idreservacion { get; set; }
        public string idcomisionista { get; set; }
        public decimal importecomision { get; set; }
        public int comisionpagada { get; set; }
        public string fechapagocomision { get; set; }
        public int foliopagocomision { get; set; }
        public int tipoventarapida { get; set; }
        public int callcenter { get; set; }
        public long idordencompra { get; set; }
        public decimal totalsindescuento { get; set; }
        public decimal totalalimentos { get; set; }
        public decimal totalbebidas { get; set; }
        public decimal totalotros { get; set; }
        public decimal totaldescuentos { get; set; }
        public decimal totaldescuentoalimentos { get; set; }
        public decimal totaldescuentobebidas { get; set; }
        public decimal totaldescuentootros { get; set; }
        public decimal totalcortesias { get; set; }
        public decimal totalcortesiaalimentos { get; set; }
        public decimal totalcortesiabebidas { get; set; }
        public decimal totalcortesiaotros { get; set; }
        public decimal totaldescuentoycortesia { get; set; }
        public decimal totalalimentossindescuentos { get; set; }
        public decimal totalbebidassindescuentos { get; set; }
        public decimal totalotrossindescuentos { get; set; }
        public int descuentocriterio { get; set; }
        public decimal descuentomonedero { get; set; }
        public string idmenucomedor { get; set; }
        public decimal subtotalcondescuento { get; set; }
        public decimal comisionpax { get; set; }
        public int procesadointerfaz { get; set; }
        public int domicilioprogramado { get; set; }
        public string fechadomicilioprogramado { get; set; }
        public int enviado { get; set; }
        public string ncf { get; set; }
        public string numerocuenta { get; set; }
        public string codigo_unico_af { get; set; }
        public int estatushub { get; set; }
        public int idfoliohub { get; set; }
        public int EnviadoRW { get; set; }
    }

    public class TB_Chequespagos
    {
        public long folio { get; set; }
        public string idformadepago { get; set; }
        public decimal importe { get; set; }
        public decimal propina { get; set; }
        public decimal tipodecambio { get; set; }
        public string referencia { get; set; }
    }

    public class TB_Cheqdet
    {
        public long foliodet { get; set; }
        public int movimiento { get; set; }
        public string comanda { get; set; }
        public int cantidad { get; set; }
        public string idproducto { get; set; }
        public int descuento { get; set; }
        public decimal precio { get; set; }
        public int impuesto1 { get; set; }
        public int impuesto2 { get; set; }
        public int impuesto3 { get; set; }
        public decimal preciosinimpuestos { get; set; }
        public string tiempo { get; set; }
        public string hora { get; set; }
        public int modificador { get; set; }
        public int mitad { get; set; }
        public string comentario { get; set; }
        public string idestacion { get; set; }
        public string usuariodescuento { get; set; }
        public string comentariodescuento { get; set; }
        public string idtipodescuento { get; set; }
        public string horaproduccion { get; set; }
        public string idproductocompuesto { get; set; }
        public int productocompuestoprincipal { get; set; }
        public decimal preciocatalogo { get; set; }
        public int marcar { get; set; }
        public string idmeseroproducto { get; set; }
        public string prioridadproduccion { get; set; }
        public int estatuspatin { get; set; }
        public string idcortesia { get; set; }
        public string numerotarjeta { get; set; }
        public int estadomonitor { get; set; }
        public string llavemovto { get; set; }
    }
}
