using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MttoVentas.Modelos;
using System.Net.Sockets;
using System.Net;

namespace MttoVentas.Datos
{
    public class ConsultasMySQLDatos : IConsultasMySQLDatos
    {
        // Variable que almacena el estado de la conexión a la base de datos
        IConexionMySQL _conexionMySQL;

        public ConsultasMySQLDatos()
        {
            this._conexionMySQL = new ConexionMySQL(Modelos.ConectionString.connMySQL);
        }

        // realiza una prueba de conexion
        public bool pruebaConn()
        {
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (MySqlException)
                {
                    return false;
                }
            }
        }

        // genera bitacora
        public long generaBitacora(string detalle, string fecha)
        {
            // int rows = 0;
            long result = 0;

            /*
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    string bitacora =
                        "insert into bitacora (id_usuario, fecha, detalle, host) " +
                        // "values (@idusu, @fecha, @detalle, (SELECT SUBSTRING_INDEX(HOST, ':', 1) AS 'ip' FROM information_schema.PROCESSLIST WHERE ID = connection_id()))";
                        "values (@idusu, @fecha, @detalle, @host)";

                    cmd.Parameters.AddWithValue("@idusu", Modelos.Login.idUsuario);
                    cmd.Parameters.AddWithValue("@detalle", detalle);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@host", getIpNameMachine());

                    ManejoSql_My res = Utilerias.EjecutaSQL(bitacora, ref rows, cmd);

                    if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);
                    else result = cmd.LastInsertedId;
                }
            }
            */

            return result;
        }

        // obtiene el siguiente consecutivo del movimiento
        public long getUltMvto()
        {
            long result = 0;

            string sql = "select ifnull(max(id_movimiento), 0) mvto from movimiento";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    ManejoSql_My res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                        while (res.reader.Read()) result = Convert.ToInt64(res.reader["mvto"]);
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();

                }
            }

            return result;
        }

        // inserta la informacion de respaldo
        public void insertRespCheques(
            List<TB_Cheques> cheques, List<long> folios, 
            List<TB_Cheques> chequesF, List<long> refolios,
            List<Modelos.TB_Cheques> chequesFF, List<long> foliosFact,
            List<TB_Cheqdet> cheqdet, List<TB_Chequespagos> chequesPagos, 
            TB_Folios tb_fol, long mvto)
        {
            MySqlTransaction trans;
            int rows = 0;

            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    trans = conn.BeginTransaction();
                    cmd.Connection = conn;

                    cmd.Transaction = trans;

                    #region - CHEQUES, CHEQUES REFOLIOS, FACTURADOS -

                    string sqlCheques =
                        "insert into cheques (id_movimiento, folio, seriefolio, numcheque, fecha, salidarepartidor, arriborepartidor, cierre, mesa, nopersonas, idmesero, pagado, cancelado, impreso, impresiones, cambio, descuento, reabiertas, razoncancelado, orden, facturado, idcliente, idarearestaurant, idempresa, tipodeservicio, idturno, usuariocancelo, comentariodescuento, estacion, cambiorepartidor, usuariodescuento, fechacancelado, idtipodescuento, numerotarjeta, folionotadeconsumo, notadeconsumo, propinapagada, propinafoliomovtocaja, puntosmonederogenerados, propinaincluida, tarjetadescuento, porcentajefac, usuariopago, propinamanual, observaciones, idclientedomicilio, iddireccion, idclientefacturacion, telefonousadodomicilio, totalarticulos, subtotal, subtotalsinimpuestos, total, totalconpropina, totalsinimpuestos, totalsindescuentosinimpuesto, totalimpuesto1, totalalimentosconimpuestos, totalbebidasconimpuestos, totalotrosconimpuestos, totalalimentossinimpuestos, totalbebidassinimpuestos, totalotrossinimpuestos, totaldescuentossinimpuestos, totaldescuentosconimpuestos, totaldescuentoalimentosconimpuesto, totaldescuentobebidasconimpuesto, totaldescuentootrosconimpuesto, totaldescuentoalimentossinimpuesto, totaldescuentobebidassinimpuesto, totaldescuentootrossinimpuesto, totalcortesiassinimpuestos, totalcortesiasconimpuestos, totalcortesiaalimentosconimpuesto, totalcortesiabebidasconimpuesto, totalcortesiaotrosconimpuesto, totalcortesiaalimentossinimpuesto, totalcortesiabebidassinimpuesto, totalcortesiaotrossinimpuesto, totaldescuentoycortesiasinimpuesto, totaldescuentoycortesiaconimpuesto, cargo, totalconcargo, totalconpropinacargo, descuentoimporte, efectivo, tarjeta, vales, otros, propina, propinatarjeta, totalalimentossinimpuestossindescuentos, totalbebidassinimpuestossindescuentos, totalotrossinimpuestossindescuentos, campoadicional1, idreservacion, idcomisionista, importecomision, comisionpagada, fechapagocomision, foliopagocomision, tipoventarapida, callcenter, idordencompra, totalsindescuento, totalalimentos, totalbebidas, totalotros, totaldescuentos, totaldescuentoalimentos, totaldescuentobebidas, totaldescuentootros, totalcortesias, totalcortesiaalimentos, totalcortesiabebidas, totalcortesiaotros, totaldescuentoycortesia, totalalimentossindescuentos, totalbebidassindescuentos, totalotrossindescuentos, descuentocriterio, descuentomonedero, idmenucomedor, subtotalcondescuento, comisionpax, procesadointerfaz, domicilioprogramado, fechadomicilioprogramado, enviado, ncf, numerocuenta, codigo_unico_af, estatushub, idfoliohub, EnviadoRW) " +
                        "values (@idMovimiento, @folio, @seriefolio, @numcheque, @fecha, @salidarepartidor, @arriborepartidor, @cierre, @mesa, @nopersonas, @idmesero, @pagado, @cancelado, @impreso, @impresiones, @cambio, @descuento, @reabiertas, @razoncancelado, @orden, @facturado, @idcliente, @idarearestaurant, @idempresa, @tipodeservicio, @idturno, @usuariocancelo, @comentariodescuento, @estacion, @cambiorepartidor, @usuariodescuento, @fechacancelado, @idtipodescuento, @numerotarjeta, @folionotadeconsumo, @notadeconsumo, @propinapagada, @propinafoliomovtocaja, @puntosmonederogenerados, @propinaincluida, @tarjetadescuento, @porcentajefac, @usuariopago, @propinamanual, @observaciones, @idclientedomicilio, @iddireccion, @idclientefacturacion, @telefonousadodomicilio, @totalarticulos, @subtotal, @subtotalsinimpuestos, @total, @totalconpropina, @totalsinimpuestos, @totalsindescuentosinimpuesto, @totalimpuesto1, @totalalimentosconimpuestos, @totalbebidasconimpuestos, @totalotrosconimpuestos, @totalalimentossinimpuestos, @totalbebidassinimpuestos, @totalotrossinimpuestos, @totaldescuentossinimpuestos, @totaldescuentosconimpuestos, @totaldescuentoalimentosconimpuesto, @totaldescuentobebidasconimpuesto, @totaldescuentootrosconimpuesto, @totaldescuentoalimentossinimpuesto, @totaldescuentobebidassinimpuesto, @totaldescuentootrossinimpuesto, @totalcortesiassinimpuestos, @totalcortesiasconimpuestos, @totalcortesiaalimentosconimpuesto, @totalcortesiabebidasconimpuesto, @totalcortesiaotrosconimpuesto, @totalcortesiaalimentossinimpuesto, @totalcortesiabebidassinimpuesto, @totalcortesiaotrossinimpuesto, @totaldescuentoycortesiasinimpuesto, @totaldescuentoycortesiaconimpuesto, @cargo, @totalconcargo, @totalconpropinacargo, @descuentoimporte, @efectivo, @tarjeta, @vales, @otros, @propina, @propinatarjeta, @totalalimentossinimpuestossindescuentos, @totalbebidassinimpuestossindescuentos, @totalotrossinimpuestossindescuentos, @campoadicional1, @idreservacion, @idcomisionista, @importecomision, @comisionpagada, @fechapagocomision, @foliopagocomision, @tipoventarapida, @callcenter, @idordencompra, @totalsindescuento, @totalalimentos, @totalbebidas, @totalotros, @totaldescuentos, @totaldescuentoalimentos, @totaldescuentobebidas, @totaldescuentootros, @totalcortesias, @totalcortesiaalimentos, @totalcortesiabebidas, @totalcortesiaotros, @totaldescuentoycortesia, @totalalimentossindescuentos, @totalbebidassindescuentos, @totalotrossindescuentos, @descuentocriterio, @descuentomonedero, @idmenucomedor, @subtotalcondescuento, @comisionpax, @procesadointerfaz, @domicilioprogramado, @fechadomicilioprogramado, @enviado, @ncf, @numerocuenta, @codigo_unico_af, @estatushub, @idfoliohub, @EnviadoRW)";

                    // cheques
                    foreach (TB_Cheques c in cheques)
                    {
                        #region - PARAMETROS -

                        cmd.Parameters.AddWithValue("@idMovimiento", mvto);
                        cmd.Parameters.AddWithValue("@folio", c.folio);
                        cmd.Parameters.AddWithValue("@seriefolio", c.seriefolio);
                        cmd.Parameters.AddWithValue("@numcheque", c.numcheque);
                        cmd.Parameters.AddWithValue("@fecha", c.fecha);
                        cmd.Parameters.AddWithValue("@salidarepartidor", c.salidarepartidor);
                        cmd.Parameters.AddWithValue("@arriborepartidor", c.arriborepartidor);
                        cmd.Parameters.AddWithValue("@cierre", c.cierre);
                        cmd.Parameters.AddWithValue("@mesa", c.mesa);
                        cmd.Parameters.AddWithValue("@nopersonas", c.nopersonas);
                        cmd.Parameters.AddWithValue("@idmesero", c.idmesero);
                        cmd.Parameters.AddWithValue("@pagado", c.pagado);
                        cmd.Parameters.AddWithValue("@cancelado", c.cancelado);
                        cmd.Parameters.AddWithValue("@impreso", c.impreso);
                        cmd.Parameters.AddWithValue("@impresiones", c.impresiones);
                        cmd.Parameters.AddWithValue("@cambio", c.cambio);
                        cmd.Parameters.AddWithValue("@descuento", c.descuento);
                        cmd.Parameters.AddWithValue("@reabiertas", c.reabiertas);
                        cmd.Parameters.AddWithValue("@razoncancelado", c.razoncancelado);
                        cmd.Parameters.AddWithValue("@orden", c.orden);
                        cmd.Parameters.AddWithValue("@facturado", c.facturado);
                        cmd.Parameters.AddWithValue("@idcliente", c.idcliente);
                        cmd.Parameters.AddWithValue("@idarearestaurant", c.idarearestaurant);
                        cmd.Parameters.AddWithValue("@idempresa", c.idempresa);
                        cmd.Parameters.AddWithValue("@tipodeservicio", c.tipodeservicio);
                        cmd.Parameters.AddWithValue("@idturno", c.idturno);
                        cmd.Parameters.AddWithValue("@usuariocancelo", c.usuariocancelo);
                        cmd.Parameters.AddWithValue("@comentariodescuento", c.comentariodescuento);
                        cmd.Parameters.AddWithValue("@estacion", c.estacion);
                        cmd.Parameters.AddWithValue("@cambiorepartidor", c.cambiorepartidor);
                        cmd.Parameters.AddWithValue("@usuariodescuento", c.usuariodescuento);
                        cmd.Parameters.AddWithValue("@fechacancelado", c.fechacancelado);
                        cmd.Parameters.AddWithValue("@idtipodescuento", c.idtipodescuento);
                        cmd.Parameters.AddWithValue("@numerotarjeta", c.numerotarjeta);
                        cmd.Parameters.AddWithValue("@folionotadeconsumo", c.folionotadeconsumo);
                        cmd.Parameters.AddWithValue("@notadeconsumo", c.notadeconsumo);
                        cmd.Parameters.AddWithValue("@propinapagada", c.propinapagada);
                        cmd.Parameters.AddWithValue("@propinafoliomovtocaja", c.propinafoliomovtocaja);
                        cmd.Parameters.AddWithValue("@puntosmonederogenerados", c.puntosmonederogenerados);
                        cmd.Parameters.AddWithValue("@propinaincluida", c.propinaincluida);
                        cmd.Parameters.AddWithValue("@tarjetadescuento", c.tarjetadescuento);
                        cmd.Parameters.AddWithValue("@porcentajefac", c.porcentajefac);
                        cmd.Parameters.AddWithValue("@usuariopago", c.usuariopago);
                        cmd.Parameters.AddWithValue("@propinamanual", c.propinamanual);
                        cmd.Parameters.AddWithValue("@observaciones", c.observaciones);
                        cmd.Parameters.AddWithValue("@idclientedomicilio", c.idclientedomicilio);
                        cmd.Parameters.AddWithValue("@iddireccion", c.iddireccion);
                        cmd.Parameters.AddWithValue("@idclientefacturacion", c.idclientefacturacion);
                        cmd.Parameters.AddWithValue("@telefonousadodomicilio", c.telefonousadodomicilio);
                        cmd.Parameters.AddWithValue("@totalarticulos", c.totalarticulos);
                        cmd.Parameters.AddWithValue("@subtotal", c.subtotal);
                        cmd.Parameters.AddWithValue("@subtotalsinimpuestos", c.subtotalsinimpuestos);
                        cmd.Parameters.AddWithValue("@total", c.total);
                        cmd.Parameters.AddWithValue("@totalconpropina", c.totalconpropina);
                        cmd.Parameters.AddWithValue("@totalsinimpuestos", c.totalsinimpuestos);
                        cmd.Parameters.AddWithValue("@totalsindescuentosinimpuesto", c.totalsindescuentosinimpuesto);
                        cmd.Parameters.AddWithValue("@totalimpuesto1", c.totalimpuesto1);
                        cmd.Parameters.AddWithValue("@totalalimentosconimpuestos", c.totalalimentosconimpuestos);
                        cmd.Parameters.AddWithValue("@totalbebidasconimpuestos", c.totalbebidasconimpuestos);
                        cmd.Parameters.AddWithValue("@totalotrosconimpuestos", c.totalotrosconimpuestos);
                        cmd.Parameters.AddWithValue("@totalalimentossinimpuestos", c.totalalimentossinimpuestos);
                        cmd.Parameters.AddWithValue("@totalbebidassinimpuestos", c.totalbebidassinimpuestos);
                        cmd.Parameters.AddWithValue("@totalotrossinimpuestos", c.totalotrossinimpuestos);
                        cmd.Parameters.AddWithValue("@totaldescuentossinimpuestos", c.totaldescuentossinimpuestos);
                        cmd.Parameters.AddWithValue("@totaldescuentosconimpuestos", c.totaldescuentosconimpuestos);
                        cmd.Parameters.AddWithValue("@totaldescuentoalimentosconimpuesto", c.totaldescuentoalimentosconimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentobebidasconimpuesto", c.totaldescuentobebidasconimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentootrosconimpuesto", c.totaldescuentootrosconimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentoalimentossinimpuesto", c.totaldescuentoalimentossinimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentobebidassinimpuesto", c.totaldescuentobebidassinimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentootrossinimpuesto", c.totaldescuentootrossinimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiassinimpuestos", c.totalcortesiassinimpuestos);
                        cmd.Parameters.AddWithValue("@totalcortesiasconimpuestos", c.totalcortesiasconimpuestos);
                        cmd.Parameters.AddWithValue("@totalcortesiaalimentosconimpuesto", c.totalcortesiaalimentosconimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiabebidasconimpuesto", c.totalcortesiabebidasconimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiaotrosconimpuesto", c.totalcortesiaotrosconimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiaalimentossinimpuesto", c.totalcortesiaalimentossinimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiabebidassinimpuesto", c.totalcortesiabebidassinimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiaotrossinimpuesto", c.totalcortesiaotrossinimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentoycortesiasinimpuesto", c.totaldescuentoycortesiasinimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentoycortesiaconimpuesto", c.totaldescuentoycortesiaconimpuesto);
                        cmd.Parameters.AddWithValue("@cargo", c.cargo);
                        cmd.Parameters.AddWithValue("@totalconcargo", c.totalconcargo);
                        cmd.Parameters.AddWithValue("@totalconpropinacargo", c.totalconpropinacargo);
                        cmd.Parameters.AddWithValue("@descuentoimporte", c.descuentoimporte);
                        cmd.Parameters.AddWithValue("@efectivo", c.efectivo);
                        cmd.Parameters.AddWithValue("@tarjeta", c.tarjeta);
                        cmd.Parameters.AddWithValue("@vales", c.vales);
                        cmd.Parameters.AddWithValue("@otros", c.otros);
                        cmd.Parameters.AddWithValue("@propina", c.propina);
                        cmd.Parameters.AddWithValue("@propinatarjeta", c.propinatarjeta);
                        cmd.Parameters.AddWithValue("@totalalimentossinimpuestossindescuentos", c.totalalimentossinimpuestossindescuentos);
                        cmd.Parameters.AddWithValue("@totalbebidassinimpuestossindescuentos", c.totalbebidassinimpuestossindescuentos);
                        cmd.Parameters.AddWithValue("@totalotrossinimpuestossindescuentos", c.totalotrossinimpuestossindescuentos);
                        cmd.Parameters.AddWithValue("@campoadicional1", c.campoadicional1);
                        cmd.Parameters.AddWithValue("@idreservacion", c.idreservacion);
                        cmd.Parameters.AddWithValue("@idcomisionista", c.idcomisionista);
                        cmd.Parameters.AddWithValue("@importecomision", c.importecomision);
                        cmd.Parameters.AddWithValue("@comisionpagada", c.comisionpagada);
                        cmd.Parameters.AddWithValue("@fechapagocomision", c.fechapagocomision);
                        cmd.Parameters.AddWithValue("@foliopagocomision", c.foliopagocomision);
                        cmd.Parameters.AddWithValue("@tipoventarapida", c.tipoventarapida);
                        cmd.Parameters.AddWithValue("@callcenter", c.callcenter);
                        cmd.Parameters.AddWithValue("@idordencompra", c.idordencompra);
                        cmd.Parameters.AddWithValue("@totalsindescuento", c.totalsindescuento);
                        cmd.Parameters.AddWithValue("@totalalimentos", c.totalalimentos);
                        cmd.Parameters.AddWithValue("@totalbebidas", c.totalbebidas);
                        cmd.Parameters.AddWithValue("@totalotros", c.totalotros);
                        cmd.Parameters.AddWithValue("@totaldescuentos", c.totaldescuentos);
                        cmd.Parameters.AddWithValue("@totaldescuentoalimentos", c.totaldescuentoalimentos);
                        cmd.Parameters.AddWithValue("@totaldescuentobebidas", c.totaldescuentobebidas);
                        cmd.Parameters.AddWithValue("@totaldescuentootros", c.totaldescuentootros);
                        cmd.Parameters.AddWithValue("@totalcortesias", c.totalcortesias);
                        cmd.Parameters.AddWithValue("@totalcortesiaalimentos", c.totalcortesiaalimentos);
                        cmd.Parameters.AddWithValue("@totalcortesiabebidas", c.totalcortesiabebidas);
                        cmd.Parameters.AddWithValue("@totalcortesiaotros", c.totalcortesiaotros);
                        cmd.Parameters.AddWithValue("@totaldescuentoycortesia", c.totaldescuentoycortesia);
                        cmd.Parameters.AddWithValue("@totalalimentossindescuentos", c.totalalimentossindescuentos);
                        cmd.Parameters.AddWithValue("@totalbebidassindescuentos", c.totalbebidassindescuentos);
                        cmd.Parameters.AddWithValue("@totalotrossindescuentos", c.totalotrossindescuentos);
                        cmd.Parameters.AddWithValue("@descuentocriterio", c.descuentocriterio);
                        cmd.Parameters.AddWithValue("@descuentomonedero", c.descuentomonedero);
                        cmd.Parameters.AddWithValue("@idmenucomedor", c.idmenucomedor);
                        cmd.Parameters.AddWithValue("@subtotalcondescuento", c.subtotalcondescuento);
                        cmd.Parameters.AddWithValue("@comisionpax", c.comisionpax);
                        cmd.Parameters.AddWithValue("@procesadointerfaz", c.procesadointerfaz);
                        cmd.Parameters.AddWithValue("@domicilioprogramado", c.domicilioprogramado);
                        cmd.Parameters.AddWithValue("@fechadomicilioprogramado", c.fechadomicilioprogramado);
                        cmd.Parameters.AddWithValue("@enviado", c.enviado);
                        cmd.Parameters.AddWithValue("@ncf", c.ncf);
                        cmd.Parameters.AddWithValue("@numerocuenta", c.numerocuenta);
                        cmd.Parameters.AddWithValue("@codigo_unico_af", c.codigo_unico_af);
                        cmd.Parameters.AddWithValue("@estatushub", c.estatushub);
                        cmd.Parameters.AddWithValue("@idfoliohub", c.idfoliohub);
                        cmd.Parameters.AddWithValue("@EnviadoRW", c.EnviadoRW);

                        #endregion

                        ManejoSql_My res = Utilerias.EjecutaSQL(sqlCheques, ref rows, cmd);

                        if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);

                        cmd.Parameters.Clear();
                    }


                    // cheques folios
                    foreach (TB_Cheques c in chequesF)
                    {
                        #region - PARAMETROS -

                        cmd.Parameters.AddWithValue("@idMovimiento", mvto);
                        cmd.Parameters.AddWithValue("@folio", c.folio);
                        cmd.Parameters.AddWithValue("@seriefolio", c.seriefolio);
                        cmd.Parameters.AddWithValue("@numcheque", c.numcheque);
                        cmd.Parameters.AddWithValue("@fecha", c.fecha);
                        cmd.Parameters.AddWithValue("@salidarepartidor", c.salidarepartidor);
                        cmd.Parameters.AddWithValue("@arriborepartidor", c.arriborepartidor);
                        cmd.Parameters.AddWithValue("@cierre", c.cierre);
                        cmd.Parameters.AddWithValue("@mesa", c.mesa);
                        cmd.Parameters.AddWithValue("@nopersonas", c.nopersonas);
                        cmd.Parameters.AddWithValue("@idmesero", c.idmesero);
                        cmd.Parameters.AddWithValue("@pagado", c.pagado);
                        cmd.Parameters.AddWithValue("@cancelado", c.cancelado);
                        cmd.Parameters.AddWithValue("@impreso", c.impreso);
                        cmd.Parameters.AddWithValue("@impresiones", c.impresiones);
                        cmd.Parameters.AddWithValue("@cambio", c.cambio);
                        cmd.Parameters.AddWithValue("@descuento", c.descuento);
                        cmd.Parameters.AddWithValue("@reabiertas", c.reabiertas);
                        cmd.Parameters.AddWithValue("@razoncancelado", c.razoncancelado);
                        cmd.Parameters.AddWithValue("@orden", c.orden);
                        cmd.Parameters.AddWithValue("@facturado", c.facturado);
                        cmd.Parameters.AddWithValue("@idcliente", c.idcliente);
                        cmd.Parameters.AddWithValue("@idarearestaurant", c.idarearestaurant);
                        cmd.Parameters.AddWithValue("@idempresa", c.idempresa);
                        cmd.Parameters.AddWithValue("@tipodeservicio", c.tipodeservicio);
                        cmd.Parameters.AddWithValue("@idturno", c.idturno);
                        cmd.Parameters.AddWithValue("@usuariocancelo", c.usuariocancelo);
                        cmd.Parameters.AddWithValue("@comentariodescuento", c.comentariodescuento);
                        cmd.Parameters.AddWithValue("@estacion", c.estacion);
                        cmd.Parameters.AddWithValue("@cambiorepartidor", c.cambiorepartidor);
                        cmd.Parameters.AddWithValue("@usuariodescuento", c.usuariodescuento);
                        cmd.Parameters.AddWithValue("@fechacancelado", c.fechacancelado);
                        cmd.Parameters.AddWithValue("@idtipodescuento", c.idtipodescuento);
                        cmd.Parameters.AddWithValue("@numerotarjeta", c.numerotarjeta);
                        cmd.Parameters.AddWithValue("@folionotadeconsumo", c.folionotadeconsumo);
                        cmd.Parameters.AddWithValue("@notadeconsumo", c.notadeconsumo);
                        cmd.Parameters.AddWithValue("@propinapagada", c.propinapagada);
                        cmd.Parameters.AddWithValue("@propinafoliomovtocaja", c.propinafoliomovtocaja);
                        cmd.Parameters.AddWithValue("@puntosmonederogenerados", c.puntosmonederogenerados);
                        cmd.Parameters.AddWithValue("@propinaincluida", c.propinaincluida);
                        cmd.Parameters.AddWithValue("@tarjetadescuento", c.tarjetadescuento);
                        cmd.Parameters.AddWithValue("@porcentajefac", c.porcentajefac);
                        cmd.Parameters.AddWithValue("@usuariopago", c.usuariopago);
                        cmd.Parameters.AddWithValue("@propinamanual", c.propinamanual);
                        cmd.Parameters.AddWithValue("@observaciones", c.observaciones);
                        cmd.Parameters.AddWithValue("@idclientedomicilio", c.idclientedomicilio);
                        cmd.Parameters.AddWithValue("@iddireccion", c.iddireccion);
                        cmd.Parameters.AddWithValue("@idclientefacturacion", c.idclientefacturacion);
                        cmd.Parameters.AddWithValue("@telefonousadodomicilio", c.telefonousadodomicilio);
                        cmd.Parameters.AddWithValue("@totalarticulos", c.totalarticulos);
                        cmd.Parameters.AddWithValue("@subtotal", c.subtotal);
                        cmd.Parameters.AddWithValue("@subtotalsinimpuestos", c.subtotalsinimpuestos);
                        cmd.Parameters.AddWithValue("@total", c.total);
                        cmd.Parameters.AddWithValue("@totalconpropina", c.totalconpropina);
                        cmd.Parameters.AddWithValue("@totalsinimpuestos", c.totalsinimpuestos);
                        cmd.Parameters.AddWithValue("@totalsindescuentosinimpuesto", c.totalsindescuentosinimpuesto);
                        cmd.Parameters.AddWithValue("@totalimpuesto1", c.totalimpuesto1);
                        cmd.Parameters.AddWithValue("@totalalimentosconimpuestos", c.totalalimentosconimpuestos);
                        cmd.Parameters.AddWithValue("@totalbebidasconimpuestos", c.totalbebidasconimpuestos);
                        cmd.Parameters.AddWithValue("@totalotrosconimpuestos", c.totalotrosconimpuestos);
                        cmd.Parameters.AddWithValue("@totalalimentossinimpuestos", c.totalalimentossinimpuestos);
                        cmd.Parameters.AddWithValue("@totalbebidassinimpuestos", c.totalbebidassinimpuestos);
                        cmd.Parameters.AddWithValue("@totalotrossinimpuestos", c.totalotrossinimpuestos);
                        cmd.Parameters.AddWithValue("@totaldescuentossinimpuestos", c.totaldescuentossinimpuestos);
                        cmd.Parameters.AddWithValue("@totaldescuentosconimpuestos", c.totaldescuentosconimpuestos);
                        cmd.Parameters.AddWithValue("@totaldescuentoalimentosconimpuesto", c.totaldescuentoalimentosconimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentobebidasconimpuesto", c.totaldescuentobebidasconimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentootrosconimpuesto", c.totaldescuentootrosconimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentoalimentossinimpuesto", c.totaldescuentoalimentossinimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentobebidassinimpuesto", c.totaldescuentobebidassinimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentootrossinimpuesto", c.totaldescuentootrossinimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiassinimpuestos", c.totalcortesiassinimpuestos);
                        cmd.Parameters.AddWithValue("@totalcortesiasconimpuestos", c.totalcortesiasconimpuestos);
                        cmd.Parameters.AddWithValue("@totalcortesiaalimentosconimpuesto", c.totalcortesiaalimentosconimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiabebidasconimpuesto", c.totalcortesiabebidasconimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiaotrosconimpuesto", c.totalcortesiaotrosconimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiaalimentossinimpuesto", c.totalcortesiaalimentossinimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiabebidassinimpuesto", c.totalcortesiabebidassinimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiaotrossinimpuesto", c.totalcortesiaotrossinimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentoycortesiasinimpuesto", c.totaldescuentoycortesiasinimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentoycortesiaconimpuesto", c.totaldescuentoycortesiaconimpuesto);
                        cmd.Parameters.AddWithValue("@cargo", c.cargo);
                        cmd.Parameters.AddWithValue("@totalconcargo", c.totalconcargo);
                        cmd.Parameters.AddWithValue("@totalconpropinacargo", c.totalconpropinacargo);
                        cmd.Parameters.AddWithValue("@descuentoimporte", c.descuentoimporte);
                        cmd.Parameters.AddWithValue("@efectivo", c.efectivo);
                        cmd.Parameters.AddWithValue("@tarjeta", c.tarjeta);
                        cmd.Parameters.AddWithValue("@vales", c.vales);
                        cmd.Parameters.AddWithValue("@otros", c.otros);
                        cmd.Parameters.AddWithValue("@propina", c.propina);
                        cmd.Parameters.AddWithValue("@propinatarjeta", c.propinatarjeta);
                        cmd.Parameters.AddWithValue("@totalalimentossinimpuestossindescuentos", c.totalalimentossinimpuestossindescuentos);
                        cmd.Parameters.AddWithValue("@totalbebidassinimpuestossindescuentos", c.totalbebidassinimpuestossindescuentos);
                        cmd.Parameters.AddWithValue("@totalotrossinimpuestossindescuentos", c.totalotrossinimpuestossindescuentos);
                        cmd.Parameters.AddWithValue("@campoadicional1", c.campoadicional1);
                        cmd.Parameters.AddWithValue("@idreservacion", c.idreservacion);
                        cmd.Parameters.AddWithValue("@idcomisionista", c.idcomisionista);
                        cmd.Parameters.AddWithValue("@importecomision", c.importecomision);
                        cmd.Parameters.AddWithValue("@comisionpagada", c.comisionpagada);
                        cmd.Parameters.AddWithValue("@fechapagocomision", c.fechapagocomision);
                        cmd.Parameters.AddWithValue("@foliopagocomision", c.foliopagocomision);
                        cmd.Parameters.AddWithValue("@tipoventarapida", c.tipoventarapida);
                        cmd.Parameters.AddWithValue("@callcenter", c.callcenter);
                        cmd.Parameters.AddWithValue("@idordencompra", c.idordencompra);
                        cmd.Parameters.AddWithValue("@totalsindescuento", c.totalsindescuento);
                        cmd.Parameters.AddWithValue("@totalalimentos", c.totalalimentos);
                        cmd.Parameters.AddWithValue("@totalbebidas", c.totalbebidas);
                        cmd.Parameters.AddWithValue("@totalotros", c.totalotros);
                        cmd.Parameters.AddWithValue("@totaldescuentos", c.totaldescuentos);
                        cmd.Parameters.AddWithValue("@totaldescuentoalimentos", c.totaldescuentoalimentos);
                        cmd.Parameters.AddWithValue("@totaldescuentobebidas", c.totaldescuentobebidas);
                        cmd.Parameters.AddWithValue("@totaldescuentootros", c.totaldescuentootros);
                        cmd.Parameters.AddWithValue("@totalcortesias", c.totalcortesias);
                        cmd.Parameters.AddWithValue("@totalcortesiaalimentos", c.totalcortesiaalimentos);
                        cmd.Parameters.AddWithValue("@totalcortesiabebidas", c.totalcortesiabebidas);
                        cmd.Parameters.AddWithValue("@totalcortesiaotros", c.totalcortesiaotros);
                        cmd.Parameters.AddWithValue("@totaldescuentoycortesia", c.totaldescuentoycortesia);
                        cmd.Parameters.AddWithValue("@totalalimentossindescuentos", c.totalalimentossindescuentos);
                        cmd.Parameters.AddWithValue("@totalbebidassindescuentos", c.totalbebidassindescuentos);
                        cmd.Parameters.AddWithValue("@totalotrossindescuentos", c.totalotrossindescuentos);
                        cmd.Parameters.AddWithValue("@descuentocriterio", c.descuentocriterio);
                        cmd.Parameters.AddWithValue("@descuentomonedero", c.descuentomonedero);
                        cmd.Parameters.AddWithValue("@idmenucomedor", c.idmenucomedor);
                        cmd.Parameters.AddWithValue("@subtotalcondescuento", c.subtotalcondescuento);
                        cmd.Parameters.AddWithValue("@comisionpax", c.comisionpax);
                        cmd.Parameters.AddWithValue("@procesadointerfaz", c.procesadointerfaz);
                        cmd.Parameters.AddWithValue("@domicilioprogramado", c.domicilioprogramado);
                        cmd.Parameters.AddWithValue("@fechadomicilioprogramado", c.fechadomicilioprogramado);
                        cmd.Parameters.AddWithValue("@enviado", c.enviado);
                        cmd.Parameters.AddWithValue("@ncf", c.ncf);
                        cmd.Parameters.AddWithValue("@numerocuenta", c.numerocuenta);
                        cmd.Parameters.AddWithValue("@codigo_unico_af", c.codigo_unico_af);
                        cmd.Parameters.AddWithValue("@estatushub", c.estatushub);
                        cmd.Parameters.AddWithValue("@idfoliohub", c.idfoliohub);
                        cmd.Parameters.AddWithValue("@EnviadoRW", c.EnviadoRW);

                        #endregion

                        ManejoSql_My res = Utilerias.EjecutaSQL(sqlCheques, ref rows, cmd);

                        if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);

                        cmd.Parameters.Clear();
                    }

                    // cheques folios FACTURADOS
                    foreach (TB_Cheques c in chequesFF)
                    {
                        #region - PARAMETROS -

                        cmd.Parameters.AddWithValue("@idMovimiento", mvto);
                        cmd.Parameters.AddWithValue("@folio", c.folio);
                        cmd.Parameters.AddWithValue("@seriefolio", c.seriefolio);
                        cmd.Parameters.AddWithValue("@numcheque", c.numcheque);
                        cmd.Parameters.AddWithValue("@fecha", c.fecha);
                        cmd.Parameters.AddWithValue("@salidarepartidor", c.salidarepartidor);
                        cmd.Parameters.AddWithValue("@arriborepartidor", c.arriborepartidor);
                        cmd.Parameters.AddWithValue("@cierre", c.cierre);
                        cmd.Parameters.AddWithValue("@mesa", c.mesa);
                        cmd.Parameters.AddWithValue("@nopersonas", c.nopersonas);
                        cmd.Parameters.AddWithValue("@idmesero", c.idmesero);
                        cmd.Parameters.AddWithValue("@pagado", c.pagado);
                        cmd.Parameters.AddWithValue("@cancelado", c.cancelado);
                        cmd.Parameters.AddWithValue("@impreso", c.impreso);
                        cmd.Parameters.AddWithValue("@impresiones", c.impresiones);
                        cmd.Parameters.AddWithValue("@cambio", c.cambio);
                        cmd.Parameters.AddWithValue("@descuento", c.descuento);
                        cmd.Parameters.AddWithValue("@reabiertas", c.reabiertas);
                        cmd.Parameters.AddWithValue("@razoncancelado", c.razoncancelado);
                        cmd.Parameters.AddWithValue("@orden", c.orden);
                        cmd.Parameters.AddWithValue("@facturado", c.facturado);
                        cmd.Parameters.AddWithValue("@idcliente", c.idcliente);
                        cmd.Parameters.AddWithValue("@idarearestaurant", c.idarearestaurant);
                        cmd.Parameters.AddWithValue("@idempresa", c.idempresa);
                        cmd.Parameters.AddWithValue("@tipodeservicio", c.tipodeservicio);
                        cmd.Parameters.AddWithValue("@idturno", c.idturno);
                        cmd.Parameters.AddWithValue("@usuariocancelo", c.usuariocancelo);
                        cmd.Parameters.AddWithValue("@comentariodescuento", c.comentariodescuento);
                        cmd.Parameters.AddWithValue("@estacion", c.estacion);
                        cmd.Parameters.AddWithValue("@cambiorepartidor", c.cambiorepartidor);
                        cmd.Parameters.AddWithValue("@usuariodescuento", c.usuariodescuento);
                        cmd.Parameters.AddWithValue("@fechacancelado", c.fechacancelado);
                        cmd.Parameters.AddWithValue("@idtipodescuento", c.idtipodescuento);
                        cmd.Parameters.AddWithValue("@numerotarjeta", c.numerotarjeta);
                        cmd.Parameters.AddWithValue("@folionotadeconsumo", c.folionotadeconsumo);
                        cmd.Parameters.AddWithValue("@notadeconsumo", c.notadeconsumo);
                        cmd.Parameters.AddWithValue("@propinapagada", c.propinapagada);
                        cmd.Parameters.AddWithValue("@propinafoliomovtocaja", c.propinafoliomovtocaja);
                        cmd.Parameters.AddWithValue("@puntosmonederogenerados", c.puntosmonederogenerados);
                        cmd.Parameters.AddWithValue("@propinaincluida", c.propinaincluida);
                        cmd.Parameters.AddWithValue("@tarjetadescuento", c.tarjetadescuento);
                        cmd.Parameters.AddWithValue("@porcentajefac", c.porcentajefac);
                        cmd.Parameters.AddWithValue("@usuariopago", c.usuariopago);
                        cmd.Parameters.AddWithValue("@propinamanual", c.propinamanual);
                        cmd.Parameters.AddWithValue("@observaciones", c.observaciones);
                        cmd.Parameters.AddWithValue("@idclientedomicilio", c.idclientedomicilio);
                        cmd.Parameters.AddWithValue("@iddireccion", c.iddireccion);
                        cmd.Parameters.AddWithValue("@idclientefacturacion", c.idclientefacturacion);
                        cmd.Parameters.AddWithValue("@telefonousadodomicilio", c.telefonousadodomicilio);
                        cmd.Parameters.AddWithValue("@totalarticulos", c.totalarticulos);
                        cmd.Parameters.AddWithValue("@subtotal", c.subtotal);
                        cmd.Parameters.AddWithValue("@subtotalsinimpuestos", c.subtotalsinimpuestos);
                        cmd.Parameters.AddWithValue("@total", c.total);
                        cmd.Parameters.AddWithValue("@totalconpropina", c.totalconpropina);
                        cmd.Parameters.AddWithValue("@totalsinimpuestos", c.totalsinimpuestos);
                        cmd.Parameters.AddWithValue("@totalsindescuentosinimpuesto", c.totalsindescuentosinimpuesto);
                        cmd.Parameters.AddWithValue("@totalimpuesto1", c.totalimpuesto1);
                        cmd.Parameters.AddWithValue("@totalalimentosconimpuestos", c.totalalimentosconimpuestos);
                        cmd.Parameters.AddWithValue("@totalbebidasconimpuestos", c.totalbebidasconimpuestos);
                        cmd.Parameters.AddWithValue("@totalotrosconimpuestos", c.totalotrosconimpuestos);
                        cmd.Parameters.AddWithValue("@totalalimentossinimpuestos", c.totalalimentossinimpuestos);
                        cmd.Parameters.AddWithValue("@totalbebidassinimpuestos", c.totalbebidassinimpuestos);
                        cmd.Parameters.AddWithValue("@totalotrossinimpuestos", c.totalotrossinimpuestos);
                        cmd.Parameters.AddWithValue("@totaldescuentossinimpuestos", c.totaldescuentossinimpuestos);
                        cmd.Parameters.AddWithValue("@totaldescuentosconimpuestos", c.totaldescuentosconimpuestos);
                        cmd.Parameters.AddWithValue("@totaldescuentoalimentosconimpuesto", c.totaldescuentoalimentosconimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentobebidasconimpuesto", c.totaldescuentobebidasconimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentootrosconimpuesto", c.totaldescuentootrosconimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentoalimentossinimpuesto", c.totaldescuentoalimentossinimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentobebidassinimpuesto", c.totaldescuentobebidassinimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentootrossinimpuesto", c.totaldescuentootrossinimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiassinimpuestos", c.totalcortesiassinimpuestos);
                        cmd.Parameters.AddWithValue("@totalcortesiasconimpuestos", c.totalcortesiasconimpuestos);
                        cmd.Parameters.AddWithValue("@totalcortesiaalimentosconimpuesto", c.totalcortesiaalimentosconimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiabebidasconimpuesto", c.totalcortesiabebidasconimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiaotrosconimpuesto", c.totalcortesiaotrosconimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiaalimentossinimpuesto", c.totalcortesiaalimentossinimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiabebidassinimpuesto", c.totalcortesiabebidassinimpuesto);
                        cmd.Parameters.AddWithValue("@totalcortesiaotrossinimpuesto", c.totalcortesiaotrossinimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentoycortesiasinimpuesto", c.totaldescuentoycortesiasinimpuesto);
                        cmd.Parameters.AddWithValue("@totaldescuentoycortesiaconimpuesto", c.totaldescuentoycortesiaconimpuesto);
                        cmd.Parameters.AddWithValue("@cargo", c.cargo);
                        cmd.Parameters.AddWithValue("@totalconcargo", c.totalconcargo);
                        cmd.Parameters.AddWithValue("@totalconpropinacargo", c.totalconpropinacargo);
                        cmd.Parameters.AddWithValue("@descuentoimporte", c.descuentoimporte);
                        cmd.Parameters.AddWithValue("@efectivo", c.efectivo);
                        cmd.Parameters.AddWithValue("@tarjeta", c.tarjeta);
                        cmd.Parameters.AddWithValue("@vales", c.vales);
                        cmd.Parameters.AddWithValue("@otros", c.otros);
                        cmd.Parameters.AddWithValue("@propina", c.propina);
                        cmd.Parameters.AddWithValue("@propinatarjeta", c.propinatarjeta);
                        cmd.Parameters.AddWithValue("@totalalimentossinimpuestossindescuentos", c.totalalimentossinimpuestossindescuentos);
                        cmd.Parameters.AddWithValue("@totalbebidassinimpuestossindescuentos", c.totalbebidassinimpuestossindescuentos);
                        cmd.Parameters.AddWithValue("@totalotrossinimpuestossindescuentos", c.totalotrossinimpuestossindescuentos);
                        cmd.Parameters.AddWithValue("@campoadicional1", c.campoadicional1);
                        cmd.Parameters.AddWithValue("@idreservacion", c.idreservacion);
                        cmd.Parameters.AddWithValue("@idcomisionista", c.idcomisionista);
                        cmd.Parameters.AddWithValue("@importecomision", c.importecomision);
                        cmd.Parameters.AddWithValue("@comisionpagada", c.comisionpagada);
                        cmd.Parameters.AddWithValue("@fechapagocomision", c.fechapagocomision);
                        cmd.Parameters.AddWithValue("@foliopagocomision", c.foliopagocomision);
                        cmd.Parameters.AddWithValue("@tipoventarapida", c.tipoventarapida);
                        cmd.Parameters.AddWithValue("@callcenter", c.callcenter);
                        cmd.Parameters.AddWithValue("@idordencompra", c.idordencompra);
                        cmd.Parameters.AddWithValue("@totalsindescuento", c.totalsindescuento);
                        cmd.Parameters.AddWithValue("@totalalimentos", c.totalalimentos);
                        cmd.Parameters.AddWithValue("@totalbebidas", c.totalbebidas);
                        cmd.Parameters.AddWithValue("@totalotros", c.totalotros);
                        cmd.Parameters.AddWithValue("@totaldescuentos", c.totaldescuentos);
                        cmd.Parameters.AddWithValue("@totaldescuentoalimentos", c.totaldescuentoalimentos);
                        cmd.Parameters.AddWithValue("@totaldescuentobebidas", c.totaldescuentobebidas);
                        cmd.Parameters.AddWithValue("@totaldescuentootros", c.totaldescuentootros);
                        cmd.Parameters.AddWithValue("@totalcortesias", c.totalcortesias);
                        cmd.Parameters.AddWithValue("@totalcortesiaalimentos", c.totalcortesiaalimentos);
                        cmd.Parameters.AddWithValue("@totalcortesiabebidas", c.totalcortesiabebidas);
                        cmd.Parameters.AddWithValue("@totalcortesiaotros", c.totalcortesiaotros);
                        cmd.Parameters.AddWithValue("@totaldescuentoycortesia", c.totaldescuentoycortesia);
                        cmd.Parameters.AddWithValue("@totalalimentossindescuentos", c.totalalimentossindescuentos);
                        cmd.Parameters.AddWithValue("@totalbebidassindescuentos", c.totalbebidassindescuentos);
                        cmd.Parameters.AddWithValue("@totalotrossindescuentos", c.totalotrossindescuentos);
                        cmd.Parameters.AddWithValue("@descuentocriterio", c.descuentocriterio);
                        cmd.Parameters.AddWithValue("@descuentomonedero", c.descuentomonedero);
                        cmd.Parameters.AddWithValue("@idmenucomedor", c.idmenucomedor);
                        cmd.Parameters.AddWithValue("@subtotalcondescuento", c.subtotalcondescuento);
                        cmd.Parameters.AddWithValue("@comisionpax", c.comisionpax);
                        cmd.Parameters.AddWithValue("@procesadointerfaz", c.procesadointerfaz);
                        cmd.Parameters.AddWithValue("@domicilioprogramado", c.domicilioprogramado);
                        cmd.Parameters.AddWithValue("@fechadomicilioprogramado", c.fechadomicilioprogramado);
                        cmd.Parameters.AddWithValue("@enviado", c.enviado);
                        cmd.Parameters.AddWithValue("@ncf", c.ncf);
                        cmd.Parameters.AddWithValue("@numerocuenta", c.numerocuenta);
                        cmd.Parameters.AddWithValue("@codigo_unico_af", c.codigo_unico_af);
                        cmd.Parameters.AddWithValue("@estatushub", c.estatushub);
                        cmd.Parameters.AddWithValue("@idfoliohub", c.idfoliohub);
                        cmd.Parameters.AddWithValue("@EnviadoRW", c.EnviadoRW);

                        #endregion

                        ManejoSql_My res = Utilerias.EjecutaSQL(sqlCheques, ref rows, cmd);

                        if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);

                        cmd.Parameters.Clear();
                    }

                    #endregion

                    #region - CHEQUESPAGOS -

                    string sqlChequesPagos =
                        "insert into chequespagos (id_movimiento, folio, idformadepago, importe, propina, tipodecambio, referencia) " +
                        "values (@idMovimiento, @folio, @idformadepago, @importe, @propina, @tipodecambio, @referencia)";

                    foreach (TB_Chequespagos c in chequesPagos)
                    {
                        #region - PARAMETROS -

                        cmd.Parameters.AddWithValue("@idMovimiento", mvto);
                        cmd.Parameters.AddWithValue("@folio", c.folio);
                        cmd.Parameters.AddWithValue("@idformadepago", c.idformadepago);
                        cmd.Parameters.AddWithValue("@importe", c.importe);
                        cmd.Parameters.AddWithValue("@propina", c.propina);
                        cmd.Parameters.AddWithValue("@tipodecambio", c.tipodecambio);
                        cmd.Parameters.AddWithValue("@referencia", c.referencia);

                        #endregion

                        ManejoSql_My res = Utilerias.EjecutaSQL(sqlChequesPagos, ref rows, cmd);

                        if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);

                        cmd.Parameters.Clear();
                    }

                    #endregion

                    #region - CHEQDET -

                    string sqlCheqdet =
                        "insert into cheqdet (id_movimiento, foliodet, movimiento, comanda, cantidad, idproducto, descuento, precio, impuesto1, impuesto2, impuesto3, preciosinimpuestos, tiempo, hora, modificador, mitad, comentario, idestacion, usuariodescuento, comentariodescuento, idtipodescuento, horaproduccion, idproductocompuesto, productocompuestoprincipal, preciocatalogo, marcar, idmeseroproducto, prioridadproduccion, estatuspatin, idcortesia, numerotarjeta, estadomonitor, llavemovto) " +
                        "values (@idMovimiento, @foliodet, @movimiento, @comanda, @cantidad, @idproducto, @descuento, @precio, @impuesto1, @impuesto2, @impuesto3, @preciosinimpuestos, @tiempo, @hora, @modificador, @mitad, @comentario, @idestacion, @usuariodescuento, @comentariodescuento, @idtipodescuento, @horaproduccion, @idproductocompuesto, @productocompuestoprincipal, @preciocatalogo, @marcar, @idmeseroproducto, @prioridadproduccion, @estatuspatin, @idcortesia, @numerotarjeta, @estadomonitor, @llavemovto)";

                    foreach (TB_Cheqdet c in cheqdet)
                    {
                        #region - PARAMETROS -

                        cmd.Parameters.AddWithValue("@idMovimiento", mvto);
                        cmd.Parameters.AddWithValue("@foliodet", c.foliodet);
                        cmd.Parameters.AddWithValue("@movimiento", c.movimiento);
                        cmd.Parameters.AddWithValue("@comanda", c.comanda);
                        cmd.Parameters.AddWithValue("@cantidad", c.cantidad);
                        cmd.Parameters.AddWithValue("@idproducto", c.idproducto);
                        cmd.Parameters.AddWithValue("@descuento", c.descuento);
                        cmd.Parameters.AddWithValue("@precio", c.precio);
                        cmd.Parameters.AddWithValue("@impuesto1", c.impuesto1);
                        cmd.Parameters.AddWithValue("@impuesto2", c.impuesto2);
                        cmd.Parameters.AddWithValue("@impuesto3", c.impuesto3);
                        cmd.Parameters.AddWithValue("@preciosinimpuestos", c.preciosinimpuestos);
                        cmd.Parameters.AddWithValue("@tiempo", c.tiempo);
                        cmd.Parameters.AddWithValue("@hora", c.hora);
                        cmd.Parameters.AddWithValue("@modificador", c.modificador);
                        cmd.Parameters.AddWithValue("@mitad", c.mitad);
                        cmd.Parameters.AddWithValue("@comentario", c.comentario);
                        cmd.Parameters.AddWithValue("@idestacion", c.idestacion);
                        cmd.Parameters.AddWithValue("@usuariodescuento", c.usuariodescuento);
                        cmd.Parameters.AddWithValue("@comentariodescuento", c.comentariodescuento);
                        cmd.Parameters.AddWithValue("@idtipodescuento", c.idtipodescuento);
                        cmd.Parameters.AddWithValue("@horaproduccion", c.horaproduccion);
                        cmd.Parameters.AddWithValue("@idproductocompuesto", c.idproductocompuesto);
                        cmd.Parameters.AddWithValue("@productocompuestoprincipal", c.productocompuestoprincipal);
                        cmd.Parameters.AddWithValue("@preciocatalogo", c.preciocatalogo);
                        cmd.Parameters.AddWithValue("@marcar", c.marcar);
                        cmd.Parameters.AddWithValue("@idmeseroproducto", c.idmeseroproducto);
                        cmd.Parameters.AddWithValue("@prioridadproduccion", c.prioridadproduccion);
                        cmd.Parameters.AddWithValue("@estatuspatin", c.estatuspatin);
                        cmd.Parameters.AddWithValue("@idcortesia", c.idcortesia);
                        cmd.Parameters.AddWithValue("@numerotarjeta", c.numerotarjeta);
                        cmd.Parameters.AddWithValue("@estadomonitor", c.estadomonitor);
                        cmd.Parameters.AddWithValue("@llavemovto", c.llavemovto);

                        #endregion

                        ManejoSql_My res = Utilerias.EjecutaSQL(sqlCheqdet, ref rows, cmd);

                        if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);

                        cmd.Parameters.Clear();
                    }

                    #endregion

                    #region - FOLIOS -

                    string sqlFolios =
                        "insert into folios (id_movimiento, serie, ultimofolio, ultimaorden, ultimofolionotadeconsumo, ultimofolioproduccion) " +
                        "values (@idMovimiento, @serie, @ultimofolio, @ultimaorden, @ultimofolionotadeconsumo, @ultimofolioproduccion)";

                    #region - PARAMETROS -

                    cmd.Parameters.AddWithValue("@idMovimiento", mvto);
                    cmd.Parameters.AddWithValue("@serie", tb_fol.serie);
                    cmd.Parameters.AddWithValue("@ultimofolio", tb_fol.ultimofolio);
                    cmd.Parameters.AddWithValue("@ultimaorden", tb_fol.ultimaorden);
                    cmd.Parameters.AddWithValue("@ultimofolionotadeconsumo", tb_fol.ultimofolionotadeconsumo);
                    cmd.Parameters.AddWithValue("@ultimofolioproduccion", tb_fol.ultimofolioproduccion);

                    #endregion

                    ManejoSql_My resF = Utilerias.EjecutaSQL(sqlFolios, ref rows, cmd);

                    if (!resF.ok) throw new Exception(resF.numErr + ": " + resF.descErr);

                    #endregion

                    cmd.Parameters.Clear();

                    // MOVIMIENTO
                    string sqlMvtos =
                        "insert into movimiento (id_movimiento, descripcion, fecha) " +
                        "values (@idMovimiento, @descripcion, now())";

                    #region - PARAMETROS -

                    string desc = "Folios Eliminados: " + string.Join(", ", folios);
                    desc += " Folios Refoliar: " + string.Join(", ", refolios);
                    desc += " Folios Facturas: " + string.Join(", ", foliosFact);

                    cmd.Parameters.AddWithValue("@idMovimiento", mvto);
                    cmd.Parameters.AddWithValue("@descripcion", desc);

                    #endregion

                    ManejoSql_My resM = Utilerias.EjecutaSQL(sqlMvtos, ref rows, cmd);

                    if (!resM.ok) throw new Exception(resM.numErr + ": " + resM.descErr);

                    trans.Commit();
                }
            }
        }
    }
}
