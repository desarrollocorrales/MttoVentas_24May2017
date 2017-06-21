using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MttoVentas.Modelos;

namespace MttoVentas.Datos
{
    public class ConsultasSSDatos : IConsultasSSDatos
    {
        // Variable que almacena el estado de la conexión a la base de datos
        IConexionSS _conexionSS;

        public ConsultasSSDatos()
        {
            this._conexionSS = new ConexionSS(Modelos.ConectionString.connSS);
        }

        // realiza una prueba de conexion a la base de datos de FIREBIRD
        public bool pruebaConn()
        {
            using (var conn = this._conexionSS.getConexionSS())
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        // obtiene la fecha del sqlserver, es la hora exacta mas cercana posible
        // ya que se debe de contar con la hora real para los tickets
        public string getFecha()
        {
            string result = Convert.ToString(DateTime.Now);

            string sql = "select GETDATE() as fecha";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionSS.getConexionSS())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                        while (res.reader.Read()) result = Convert.ToString(res.reader["fecha"]).Trim();
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();

                }
            }

            DateTime dt = DateTime.Parse(result);

            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // obtiene folios
        public List<Folios> obtFolios()
        {
            List<Modelos.Folios> result = new List<Modelos.Folios>();
            Modelos.Folios ent;

            string sql = "select serie, ultimofolio from folios order by serie asc";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionSS.getConexionSS())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        if (res.reader.HasRows)
                            while (res.reader.Read())
                            {
                                ent = new Modelos.Folios();

                                ent.serie = Convert.ToString(res.reader["serie"]);
                                ent.ultimoFolio = Convert.ToString(res.reader["ultimofolio"]);

                                result.Add(ent);
                            }
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();
                }
            }

            return result;
        }

        // obtiene el turno abierto, si no se encuentra regresa una lista vacia
        public List<Turnos> getTurnosAbiertos()
        {
            List<Modelos.Turnos> result = new List<Modelos.Turnos>();
            Modelos.Turnos ent;

            string sql =
                "select idturno, idturnointerno, apertura, cierre, idestacion " +
                "from turnos where cierre is null or cierre = ''";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionSS.getConexionSS())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        if (res.reader.HasRows)
                            while (res.reader.Read())
                            {
                                ent = new Modelos.Turnos();

                                ent.idTurno = Convert.ToInt64(res.reader["idturno"]);
                                ent.idTurnoInterno = Convert.ToInt64(res.reader["idturnointerno"]);
                                ent.apertura = Convert.ToString(res.reader["apertura"]);
                                ent.cierre = Convert.ToString(res.reader["cierre"]);
                                ent.estacion = Convert.ToString(res.reader["idestacion"]);

                                result.Add(ent);
                            }
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();
                }
            }

            return result;
        }

        // busca si ya se han realizado ventas en el turno actual
        // false = NO tiene ventas
        // true  = tiene ventas
        public bool buscaVentas(long idTurno)
        {
            bool result = false;

            string sql =
                "select count(*) count " +
                "from tempcheques where idturno = @idTurno";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionSS.getConexionSS())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@idTurno", idTurno);

                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        if (res.reader.HasRows)
                            while (res.reader.Read())
                            {
                                int count = Convert.ToInt16(res.reader["count"]);

                                if(count != 0)
                                    result = true;
                            }
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();
                }
            }

            return result;
        }

        // obtiene las estaciones
        public List<Estaciones> getEstaciones()
        {
            List<Modelos.Estaciones> result = new List<Modelos.Estaciones>();
            Modelos.Estaciones ent;

            string sql =
                "select idestacion, descripcion " +
                "from estaciones";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionSS.getConexionSS())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        if (res.reader.HasRows)
                            while (res.reader.Read())
                            {
                                ent = new Modelos.Estaciones();

                                ent.idestacion = Convert.ToString(res.reader["idestacion"]);
                                ent.descripcion = Convert.ToString(res.reader["descripcion"]);

                                result.Add(ent);
                            }
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();
                }
            }

            return result;
        }

        // busca la serie seleccionada segun la estacion
        public string buscaSerie(string idEstacion)
        {
            string result = string.Empty;

            string sql = "select seriefolio from estaciones where idestacion = @idEstacion";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionSS.getConexionSS())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("idEstacion", idEstacion);

                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                        while (res.reader.Read()) result = Convert.ToString(res.reader["seriefolio"]).Trim();
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();

                }
            }

            return result;
        }

        // cambia el turno
        public bool cambiaTurno(string serie, string estacion)
        {
            string sql = "UPDATE estaciones SET seriefolio = @serieFolio where idestacion = @idEstacion";

            bool result = true;

            int rows = 0;

            using (var conn = this._conexionSS.getConexionSS())
            {
                conn.Open();
                string error = string.Empty;

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    // define parametros
                    cmd.Parameters.AddWithValue("@serieFolio", serie);
                    cmd.Parameters.AddWithValue("@idEstacion", estacion);

                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, ref rows, cmd);

                    if (res.ok)
                    {
                        if (rows == 0) result = false;
                    }
                    else error = res.numErr + ": " + res.descErr;
                }

            }

            return result;
        }

        // regresa las cuentas cerradas
        public List<Cuentas> getCuentas(string fechaIni, string fechaFin, string serie, string estacion)
        {
            List<Modelos.Cuentas> result = new List<Modelos.Cuentas>();
            Modelos.Cuentas ent;

            string sql =
                "select idturno, folio, seriefolio, numcheque as foliocuenta, folionotadeconsumo, fecha, " +
                    "cancelado, facturado, descuento, total, efectivo, tarjeta, vales, otros " +
                "from cheques " +
                "where estacion = @estacion AND fecha BETWEEN @fechaIni and @fechaFin ";
            
            
            // "where estacion = @estacion -- AND fecha BETWEEN '" + fechaIni + "' and '" + fechaFin + "' ";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionSS.getConexionSS())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    // parametros
                    cmd.Parameters.AddWithValue("@fechaIni", fechaIni);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("@estacion", estacion);

                    if (!serie.Equals("TODAS"))
                    {
                        sql += "and seriefolio = @serie ";
                        cmd.Parameters.AddWithValue("serie", serie);
                    }

                    sql += "order by idturno, seriefolio, numcheque";


                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        while (res.reader.Read())
                        {
                            ent = new Modelos.Cuentas();

                            if (res.reader["idturno"] == DBNull.Value) ent.turno = string.Empty;
                            else ent.turno = Convert.ToString(res.reader["idturno"]);

                            if (res.reader["folio"] == DBNull.Value) ent.folio = 0;
                            else ent.folio = Convert.ToInt64(res.reader["folio"]);

                            if (res.reader["seriefolio"] == DBNull.Value) ent.serieFolio = string.Empty;
                            else ent.serieFolio = Convert.ToString(res.reader["seriefolio"]);

                            if (res.reader["foliocuenta"] == DBNull.Value) ent.folioCuenta = string.Empty;
                            else ent.folioCuenta = Convert.ToString(res.reader["foliocuenta"]);

                            if (res.reader["folionotadeconsumo"] == DBNull.Value) ent.folioNotaConsumo = string.Empty;
                            else ent.folioNotaConsumo = Convert.ToString(res.reader["folionotadeconsumo"]);

                            if (res.reader["fecha"] == DBNull.Value) ent.fecha = string.Empty;
                            // else ent.fecha = Convert.ToString(res.reader["fecha"]);
                            else ent.fecha = Convert.ToDateTime(res.reader["fecha"]).ToString("yyyy/MM/dd HH:mm:ss");

                            if (res.reader["cancelado"] == DBNull.Value) ent.cancelado = false;
                            else ent.cancelado = Convert.ToInt16(res.reader["cancelado"]) == 0 ? false : true;

                            if (res.reader["facturado"] == DBNull.Value) ent.facturado = false;
                            else ent.facturado = Convert.ToInt16(res.reader["facturado"]) == 0 ? false : true;

                            if (res.reader["descuento"] == DBNull.Value) ent.descuento = 0;
                            else ent.descuento = Convert.ToDecimal(Convert.ToString(res.reader["descuento"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["total"] == DBNull.Value) ent.totalOriginal = 0;
                            else ent.totalOriginal = Convert.ToDecimal(Convert.ToString(res.reader["total"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["efectivo"] == DBNull.Value) ent.efectivo = 0;
                            else ent.efectivo = Convert.ToDecimal(Convert.ToString(res.reader["efectivo"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["tarjeta"] == DBNull.Value) ent.tarjeta = 0;
                            else ent.tarjeta = Convert.ToDecimal(Convert.ToString(res.reader["tarjeta"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["vales"] == DBNull.Value) ent.vales = 0;
                            else ent.vales = Convert.ToDecimal(Convert.ToString(res.reader["vales"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["otros"] == DBNull.Value) ent.otros = 0;
                            else ent.otros = Convert.ToDecimal(Convert.ToString(res.reader["otros"]).TrimEnd(new Char[] { '0' }));

                            ent.eliminar = true;
                            ent.productosEliminados = 0;
                            result.Add(ent);
                        }
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();
                }
            }

            return result;
        }

        // elimina folios seleccionados
        public bool elimFolios(List<long> folios, string estacion, List<long> refolios, string nvoFolio, string serie)
        {
            SqlTransaction trans;

            bool result = true;

            int rows = 0;

            using (var conn = this._conexionSS.getConexionSS())
            {
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    trans = conn.BeginTransaction();

                    cmd.Connection = conn;
                    cmd.Transaction = trans;

                    /* CHEQUESPAGOS */
                    string sql = "delete from chequespagos where folio in ("; // "folio = @folio";

                    // define parametros
                    string whereIn = string.Empty;

                    int i = 1;
                    foreach (long fl in folios)
                    {
                        whereIn += "@f" + i + ", ";

                        cmd.Parameters.AddWithValue("@f" + i, fl);

                        i++;
                    }

                    sql += whereIn.Substring(0, whereIn.Trim().Length - 1) + ")";

                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, ref rows, cmd);

                    if (res.ok)
                    {
                        if (rows == 0) result = false;
                    }
                    else
                    {
                        trans.Rollback();
                        throw new Exception(res.numErr + ": " + res.descErr);
                    }

                    cmd.Parameters.Clear();

                    /* CHEQUESDET */
                    string sqlCD = "delete from cheqdet where idestacion = @idEstacion and foliodet in ("; // "folio = @folio";

                    // define parametros
                    // cmd.Parameters.AddWithValue("@folio", fol);
                    cmd.Parameters.AddWithValue("@idEstacion", estacion);

                    whereIn = string.Empty;

                    i = 1;
                    foreach (long fl in folios)
                    {
                        whereIn += "@f" + i + ", ";

                        cmd.Parameters.AddWithValue("@f" + i, fl);

                        i++;
                    }

                    sqlCD += whereIn.Substring(0, whereIn.Trim().Length - 1) + ")";

                    ManejoSql_SS res1 = Utilerias.EjecutaSQL(sqlCD, ref rows, cmd);

                    if (res1.ok)
                    {
                        if (rows == 0) result = false;
                    }
                    else
                    {
                        trans.Rollback();
                        throw new Exception(res1.numErr + ": " + res1.descErr);
                    }

                    cmd.Parameters.Clear();

                    /* CHEQUES */
                    string sqlC = "delete from cheques where estacion = @idEstacion and folio in ("; // "folio = @folio";

                    // define parametros
                    cmd.Parameters.AddWithValue("@idEstacion", estacion);

                    whereIn = string.Empty;

                    i = 1;
                    foreach (long fl in folios)
                    {
                        whereIn += "@f" + i + ", ";

                        cmd.Parameters.AddWithValue("@f" + i, fl);

                        i++;
                    }

                    sqlC += whereIn.Substring(0, whereIn.Trim().Length - 1) + ")";

                    ManejoSql_SS res2 = Utilerias.EjecutaSQL(sqlC, ref rows, cmd);

                    if (res2.ok)
                    {
                        if (rows == 0) result = false;
                    }
                    else
                    {
                        trans.Rollback();
                        throw new Exception(res2.numErr + ": " + res2.descErr);
                    }

                    trans.Commit();
                }
            }

            return result;
        }

        // obtiene el ultimo folio
        public string getUltFolio(string serie)
        {
            string result = string.Empty;

            string sql = "select (ultimofolio + 1) as ultimofolio from folios where serie = @serie";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionSS.getConexionSS())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("serie", serie);

                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                        while (res.reader.Read()) result = Convert.ToString(res.reader["ultimofolio"]).Trim();
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();

                }
            }

            return result;
        }

        // refoliar 
        public void refoliar(List<long> folios, string estacion, List<long> refolios, string nvoFolio, string serie)
        {
            SqlTransaction trans;

            int rows = 0;

            using (var conn = this._conexionSS.getConexionSS())
            {
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    trans = conn.BeginTransaction();

                    cmd.Connection = conn;
                    cmd.Transaction = trans;

                    /* REFOLIAR */

                    if (!string.IsNullOrEmpty(nvoFolio))
                    {
                        Int32 _nvoFolio = Convert.ToInt32(nvoFolio);
                        string sqlRF = "update cheques set numcheque = @numCheque where estacion = @idEstacion and folio = @folio";

                        cmd.Parameters.Clear();

                        foreach (long fl in refolios)
                        {
                            // define parametros
                            cmd.Parameters.AddWithValue("@idEstacion", estacion);
                            cmd.Parameters.AddWithValue("@folio", fl);
                            cmd.Parameters.AddWithValue("@numCheque", _nvoFolio);

                            ManejoSql_SS res3 = Utilerias.EjecutaSQL(sqlRF, ref rows, cmd);

                            if (!res3.ok)
                            {
                                trans.Rollback();
                                throw new Exception(res3.numErr + ": " + res3.descErr);
                            }

                            _nvoFolio++;
                            cmd.Parameters.Clear();
                        }

                        /* CAMBIA FOLIO - SECUENCIA */
                        string sqlF = "update folios set ultimofolio = @ultFolio where serie = @serie";

                        cmd.Parameters.Clear();

                        // define parametros
                        cmd.Parameters.AddWithValue("@ultFolio", _nvoFolio - 1);
                        cmd.Parameters.AddWithValue("@serie", serie);

                        ManejoSql_SS res4 = Utilerias.EjecutaSQL(sqlF, ref rows, cmd);

                        if (!res4.ok)
                        {
                            trans.Rollback();
                            throw new Exception(res4.numErr + ": " + res4.descErr);
                        }
                    }

                    trans.Commit();
                }
            }
        }

        // obtiene datos de CHEQUESPAGOS para generar el respaldo
        public List<TB_Chequespagos> getChequesPagos(List<long> folios)
        {
            List<Modelos.TB_Chequespagos> result = new List<Modelos.TB_Chequespagos>();
            Modelos.TB_Chequespagos ent;

            if (folios.Count == 0) return result;

            string sql =
                "select folio, idformadepago, importe, propina, tipodecambio, referencia " +
                "from chequespagos " +
                "where folio in (";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionSS.getConexionSS())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    
                    // define parametros
                    string whereIn = string.Empty;

                    int i = 1;
                    foreach (long fl in folios)
                    {
                        whereIn += "@f" + i + ", ";

                        cmd.Parameters.AddWithValue("@f" + i, fl);

                        i++;
                    }

                    sql += whereIn.Substring(0, whereIn.Trim().Length - 1) + ")";

                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, cmd);
                    
                    if (res.ok)
                    {
                        while (res.reader.Read())
                        {
                            ent = new Modelos.TB_Chequespagos();

                            if (res.reader["folio"] == DBNull.Value) ent.folio = 0;
                            else ent.folio = Convert.ToInt64(res.reader["folio"]);

                            if (res.reader["idformadepago"] == DBNull.Value) ent.idformadepago = string.Empty;
                            else ent.idformadepago = Convert.ToString(res.reader["idformadepago"]);

                            if (res.reader["importe"] == DBNull.Value) ent.importe = 0;
                            else ent.importe = Convert.ToDecimal(Convert.ToString(res.reader["importe"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["propina"] == DBNull.Value) ent.propina = 0;
                            else ent.propina = Convert.ToDecimal(Convert.ToString(res.reader["propina"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["tipodecambio"] == DBNull.Value) ent.tipodecambio = 0;
                            else ent.tipodecambio = Convert.ToDecimal(Convert.ToString(res.reader["tipodecambio"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["referencia"] == DBNull.Value) ent.referencia = string.Empty;
                            else ent.referencia = Convert.ToString(res.reader["referencia"]);

                            result.Add(ent);
                        }
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();
                }
            }

            return result;
        }

        // obtiene datos de CHEQUESDET para generar el respaldo
        public List<TB_Cheqdet> getCheqdet(string estacion, List<long> folios)
        {
            List<Modelos.TB_Cheqdet> result = new List<Modelos.TB_Cheqdet>();
            Modelos.TB_Cheqdet ent;

            if (folios.Count == 0) return result;

            string sql =
                "select foliodet, movimiento, comanda, cantidad, idproducto, descuento, precio, impuesto1, impuesto2, " + 
                    "impuesto3, preciosinimpuestos, tiempo, hora, modificador, mitad, comentario, idestacion, usuariodescuento, " +
                    "comentariodescuento, idtipodescuento, horaproduccion, idproductocompuesto, productocompuestoprincipal, " + 
                    "preciocatalogo, marcar, idmeseroproducto, prioridadproduccion, estatuspatin, idcortesia, numerotarjeta, " + 
                    "estadomonitor, llavemovto " +
                "from cheqdet " +
                "where idestacion = @estacion and foliodet in (";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionSS.getConexionSS())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@estacion", estacion);

                    // define parametros
                    string whereIn = string.Empty;

                    int i = 1;
                    foreach (long fl in folios)
                    {
                        whereIn += "@f" + i + ", ";

                        cmd.Parameters.AddWithValue("@f" + i, fl);

                        i++;
                    }

                    sql += whereIn.Substring(0, whereIn.Trim().Length - 1) + ")";

                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        while (res.reader.Read())
                        {
                            ent = new Modelos.TB_Cheqdet();

                            if (res.reader["foliodet"] == DBNull.Value) ent.foliodet = 0;
                            else ent.foliodet = Convert.ToInt64(res.reader["foliodet"]);

                            if (res.reader["movimiento"] == DBNull.Value) ent.movimiento = 0;
                            else ent.movimiento = Convert.ToInt16(res.reader["movimiento"]);

                            if (res.reader["comanda"] == DBNull.Value) ent.comanda = string.Empty;
                            else ent.comanda = Convert.ToString(res.reader["comanda"]);

                            if (res.reader["cantidad"] == DBNull.Value) ent.cantidad = 0;
                            else ent.cantidad = Convert.ToInt16(res.reader["cantidad"]);

                            if (res.reader["idproducto"] == DBNull.Value) ent.idproducto = string.Empty;
                            else ent.idproducto = Convert.ToString(res.reader["idproducto"]);

                            if (res.reader["descuento"] == DBNull.Value) ent.descuento = 0;
                            else ent.descuento = Convert.ToInt16(res.reader["descuento"]);

                            if (res.reader["precio"] == DBNull.Value) ent.precio = 0;
                            else ent.precio = Convert.ToDecimal(Convert.ToString(res.reader["precio"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["impuesto1"] == DBNull.Value) ent.impuesto1 = 0;
                            else ent.impuesto1 = Convert.ToInt16(res.reader["impuesto1"]);

                            if (res.reader["impuesto2"] == DBNull.Value) ent.impuesto2 = 0;
                            else ent.impuesto2 = Convert.ToInt16(res.reader["impuesto2"]);

                            if (res.reader["impuesto3"] == DBNull.Value) ent.impuesto3 = 0;
                            else ent.impuesto3 = Convert.ToInt16(res.reader["impuesto3"]);

                            if (res.reader["preciosinimpuestos"] == DBNull.Value) ent.preciosinimpuestos = 0;
                            else ent.preciosinimpuestos = Convert.ToDecimal(Convert.ToString(res.reader["preciosinimpuestos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["tiempo"] == DBNull.Value) ent.tiempo = string.Empty;
                            else ent.tiempo = Convert.ToString(res.reader["tiempo"]);

                            if (res.reader["hora"] == DBNull.Value) ent.hora = string.Empty;
                            else ent.hora = Convert.ToString(res.reader["hora"]);

                            if (res.reader["modificador"] == DBNull.Value) ent.modificador = 0;
                            else ent.modificador = Convert.ToInt16(res.reader["modificador"]);

                            if (res.reader["mitad"] == DBNull.Value) ent.mitad = 0;
                            else ent.mitad = Convert.ToInt16(res.reader["mitad"]);

                            if (res.reader["comentario"] == DBNull.Value) ent.comentario = string.Empty;
                            else ent.comentario = Convert.ToString(res.reader["comentario"]);

                            if (res.reader["idestacion"] == DBNull.Value) ent.idestacion = string.Empty;
                            else ent.idestacion = Convert.ToString(res.reader["idestacion"]);

                            if (res.reader["usuariodescuento"] == DBNull.Value) ent.usuariodescuento = string.Empty;
                            else ent.usuariodescuento = Convert.ToString(res.reader["usuariodescuento"]);

                            if (res.reader["comentariodescuento"] == DBNull.Value) ent.comentariodescuento = string.Empty;
                            else ent.comentariodescuento = Convert.ToString(res.reader["comentariodescuento"]);

                            if (res.reader["idtipodescuento"] == DBNull.Value) ent.idtipodescuento = string.Empty;
                            else ent.idtipodescuento = Convert.ToString(res.reader["idtipodescuento"]);

                            if (res.reader["horaproduccion"] == DBNull.Value) ent.horaproduccion = string.Empty;
                            else ent.horaproduccion = Convert.ToString(res.reader["horaproduccion"]);

                            if (res.reader["idproductocompuesto"] == DBNull.Value) ent.idproductocompuesto = string.Empty;
                            else ent.idproductocompuesto = Convert.ToString(res.reader["idproductocompuesto"]);

                            if (res.reader["productocompuestoprincipal"] == DBNull.Value) ent.productocompuestoprincipal = 0;
                            else ent.productocompuestoprincipal = Convert.ToInt16(res.reader["productocompuestoprincipal"]);

                            if (res.reader["preciocatalogo"] == DBNull.Value) ent.preciocatalogo = 0;
                            else ent.preciocatalogo = Convert.ToDecimal(Convert.ToString(res.reader["preciocatalogo"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["marcar"] == DBNull.Value) ent.marcar = 0;
                            else ent.marcar = Convert.ToInt16(res.reader["marcar"]);

                            if (res.reader["idmeseroproducto"] == DBNull.Value) ent.idmeseroproducto = string.Empty;
                            else ent.idmeseroproducto = Convert.ToString(res.reader["idmeseroproducto"]);

                            if (res.reader["prioridadproduccion"] == DBNull.Value) ent.prioridadproduccion = string.Empty;
                            else ent.prioridadproduccion = Convert.ToString(res.reader["prioridadproduccion"]);

                            if (res.reader["estatuspatin"] == DBNull.Value) ent.estatuspatin = 0;
                            else ent.estatuspatin = Convert.ToInt16(res.reader["estatuspatin"]);

                            if (res.reader["idcortesia"] == DBNull.Value) ent.idcortesia = string.Empty;
                            else ent.idcortesia = Convert.ToString(res.reader["idcortesia"]);

                            if (res.reader["numerotarjeta"] == DBNull.Value) ent.numerotarjeta = string.Empty;
                            else ent.numerotarjeta = Convert.ToString(res.reader["numerotarjeta"]);

                            if (res.reader["estadomonitor"] == DBNull.Value) ent.estadomonitor = 0;
                            else ent.estadomonitor = Convert.ToInt16(res.reader["estadomonitor"]);

                            if (res.reader["llavemovto"] == DBNull.Value) ent.llavemovto = string.Empty;
                            else ent.llavemovto = Convert.ToString(res.reader["llavemovto"]);

                            result.Add(ent);
                        }
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();
                }
            }

            return result;
        }

        // obtiene datos de CHEQUES para generar el respaldo
        public List<TB_Cheques> getCheques(string estacion, List<long> folios)
        {
            List<Modelos.TB_Cheques> result = new List<Modelos.TB_Cheques>();
            Modelos.TB_Cheques ent;

            if (folios.Count == 0) return result;

            string sql =
                "select folio, seriefolio, numcheque, fecha, salidarepartidor, arriborepartidor, cierre, mesa, nopersonas, idmesero, pagado, " +
                    "cancelado, impreso, impresiones, cambio, descuento, reabiertas, razoncancelado, orden, facturado, idcliente, idarearestaurant, " +
                    "idempresa, tipodeservicio, idturno, usuariocancelo, comentariodescuento, estacion, cambiorepartidor, usuariodescuento, " +
                    "fechacancelado, idtipodescuento, numerotarjeta, folionotadeconsumo, notadeconsumo, propinapagada, propinafoliomovtocaja, " +
                    "puntosmonederogenerados, propinaincluida, tarjetadescuento, porcentajefac, usuariopago, propinamanual, observaciones, " +
                    "idclientedomicilio, iddireccion, idclientefacturacion, telefonousadodomicilio, totalarticulos, subtotal, subtotalsinimpuestos, " +
                    "total, totalconpropina, totalsinimpuestos, totalsindescuentosinimpuesto, totalimpuesto1, totalalimentosconimpuestos, " +
                    "totalbebidasconimpuestos, totalotrosconimpuestos, totalalimentossinimpuestos, totalbebidassinimpuestos, totalotrossinimpuestos, " +
                    "totaldescuentossinimpuestos, totaldescuentosconimpuestos, totaldescuentoalimentosconimpuesto, totaldescuentobebidasconimpuesto, " +
                    "totaldescuentootrosconimpuesto, totaldescuentoalimentossinimpuesto, totaldescuentobebidassinimpuesto, " +
                    "totaldescuentootrossinimpuesto, totalcortesiassinimpuestos, totalcortesiasconimpuestos, totalcortesiaalimentosconimpuesto, " +
                    "totalcortesiabebidasconimpuesto, totalcortesiaotrosconimpuesto, totalcortesiaalimentossinimpuesto, " +
                    "totalcortesiabebidassinimpuesto, totalcortesiaotrossinimpuesto, totaldescuentoycortesiasinimpuesto, " +
                    "totaldescuentoycortesiaconimpuesto, cargo, totalconcargo, totalconpropinacargo, descuentoimporte, efectivo, tarjeta, vales, otros, " +
                    "propina, propinatarjeta, totalalimentossinimpuestossindescuentos, totalbebidassinimpuestossindescuentos, " +
                    "totalotrossinimpuestossindescuentos, campoadicional1, idreservacion, idcomisionista, importecomision, comisionpagada, " +
                    "fechapagocomision, foliopagocomision, tipoventarapida, callcenter, idordencompra, totalsindescuento, totalalimentos, totalbebidas, " +
                    "totalotros, totaldescuentos, totaldescuentoalimentos, totaldescuentobebidas, totaldescuentootros, totalcortesias, " +
                    "totalcortesiaalimentos, totalcortesiabebidas, totalcortesiaotros, totaldescuentoycortesia, totalalimentossindescuentos, " +
                    "totalbebidassindescuentos, totalotrossindescuentos, descuentocriterio, descuentomonedero, idmenucomedor, subtotalcondescuento, " +
                    "comisionpax, procesadointerfaz, domicilioprogramado, fechadomicilioprogramado, enviado, ncf, numerocuenta, codigo_unico_af, " +
                    "estatushub, idfoliohub, EnviadoRW " +
                "from cheques " +
                "where estacion = @estacion and folio in (";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionSS.getConexionSS())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@estacion", estacion);

                    // define parametros
                    string whereIn = string.Empty;

                    int i = 1;
                    foreach (long fl in folios)
                    {
                        whereIn += "@f" + i + ", ";

                        cmd.Parameters.AddWithValue("@f" + i, fl);

                        i++;
                    }

                    sql += whereIn.Substring(0, whereIn.Trim().Length - 1) + ")";

                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        while (res.reader.Read())
                        {
                            ent = new Modelos.TB_Cheques();

                            if (res.reader["folio"] == DBNull.Value) ent.folio = 0;
                            else ent.folio = Convert.ToInt64(res.reader["folio"]);

                            if (res.reader["seriefolio"] == DBNull.Value) ent.seriefolio = string.Empty;
                            else ent.seriefolio = Convert.ToString(res.reader["seriefolio"]);

                            if (res.reader["numcheque"] == DBNull.Value) ent.numcheque = 0;
                            else ent.numcheque = Convert.ToInt64(res.reader["numcheque"]);

                            if (res.reader["fecha"] == DBNull.Value) ent.fecha = string.Empty;
                            else ent.fecha = Convert.ToString(res.reader["fecha"]);

                            if (res.reader["salidarepartidor"] == DBNull.Value) ent.salidarepartidor = string.Empty;
                            else ent.salidarepartidor = Convert.ToString(res.reader["salidarepartidor"]);

                            if (res.reader["arriborepartidor"] == DBNull.Value) ent.arriborepartidor = string.Empty;
                            else ent.arriborepartidor = Convert.ToString(res.reader["arriborepartidor"]);

                            if (res.reader["cierre"] == DBNull.Value) ent.cierre = string.Empty;
                            else ent.cierre = Convert.ToString(res.reader["cierre"]);

                            if (res.reader["mesa"] == DBNull.Value) ent.mesa = string.Empty;
                            else ent.mesa = Convert.ToString(res.reader["mesa"]);

                            if (res.reader["nopersonas"] == DBNull.Value) ent.nopersonas = 0;
                            else ent.nopersonas = Convert.ToInt16(res.reader["nopersonas"]);

                            if (res.reader["idmesero"] == DBNull.Value) ent.idmesero = string.Empty;
                            else ent.idmesero = Convert.ToString(res.reader["idmesero"]);

                            if (res.reader["pagado"] == DBNull.Value) ent.pagado = 0;
                            else ent.pagado = Convert.ToInt16(res.reader["pagado"]);

                            if (res.reader["cancelado"] == DBNull.Value) ent.cancelado = 0;
                            else ent.cancelado = Convert.ToInt16(res.reader["cancelado"]);

                            if (res.reader["impreso"] == DBNull.Value) ent.impreso = 0;
                            else ent.impreso = Convert.ToInt16(res.reader["impreso"]);

                            if (res.reader["impresiones"] == DBNull.Value) ent.impresiones = 0;
                            else ent.impresiones = Convert.ToInt16(res.reader["impresiones"]);

                            if (res.reader["cambio"] == DBNull.Value) ent.cambio = 0;
                            else ent.cambio = Convert.ToDecimal(Convert.ToString(res.reader["cambio"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["descuento"] == DBNull.Value) ent.descuento = 0;
                            else ent.descuento = Convert.ToInt16(res.reader["descuento"]);

                            if (res.reader["reabiertas"] == DBNull.Value) ent.reabiertas = 0;
                            else ent.reabiertas = Convert.ToInt16(res.reader["reabiertas"]);

                            if (res.reader["razoncancelado"] == DBNull.Value) ent.razoncancelado = string.Empty;
                            else ent.razoncancelado = Convert.ToString(res.reader["razoncancelado"]);

                            if (res.reader["orden"] == DBNull.Value) ent.orden = 0;
                            else ent.orden = Convert.ToInt16(res.reader["orden"]);

                            if (res.reader["facturado"] == DBNull.Value) ent.facturado = 0;
                            else ent.facturado = Convert.ToInt16(res.reader["facturado"]);

                            if (res.reader["idcliente"] == DBNull.Value) ent.idcliente = string.Empty;
                            else ent.idcliente = Convert.ToString(res.reader["idcliente"]);

                            if (res.reader["idarearestaurant"] == DBNull.Value) ent.idarearestaurant = string.Empty;
                            else ent.idarearestaurant = Convert.ToString(res.reader["idarearestaurant"]);

                            if (res.reader["idempresa"] == DBNull.Value) ent.idempresa = string.Empty;
                            else ent.idempresa = Convert.ToString(res.reader["idempresa"]);

                            if (res.reader["tipodeservicio"] == DBNull.Value) ent.tipodeservicio = 0;
                            else ent.tipodeservicio = Convert.ToInt16(res.reader["tipodeservicio"]);

                            if (res.reader["idturno"] == DBNull.Value) ent.idturno = 0;
                            else ent.idturno = Convert.ToInt16(res.reader["idturno"]);

                            if (res.reader["usuariocancelo"] == DBNull.Value) ent.usuariocancelo = string.Empty;
                            else ent.usuariocancelo = Convert.ToString(res.reader["usuariocancelo"]);

                            if (res.reader["comentariodescuento"] == DBNull.Value) ent.comentariodescuento = string.Empty;
                            else ent.comentariodescuento = Convert.ToString(res.reader["comentariodescuento"]);

                            if (res.reader["estacion"] == DBNull.Value) ent.estacion = string.Empty;
                            else ent.estacion = Convert.ToString(res.reader["estacion"]);

                            if (res.reader["cambiorepartidor"] == DBNull.Value) ent.cambiorepartidor = 0;
                            else ent.cambiorepartidor = Convert.ToDecimal(Convert.ToString(res.reader["cambiorepartidor"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["usuariodescuento"] == DBNull.Value) ent.usuariodescuento = string.Empty;
                            else ent.usuariodescuento = Convert.ToString(res.reader["usuariodescuento"]);

                            if (res.reader["fechacancelado"] == DBNull.Value) ent.fechacancelado = string.Empty;
                            else ent.fechacancelado = Convert.ToString(res.reader["fechacancelado"]);

                            if (res.reader["idtipodescuento"] == DBNull.Value) ent.idtipodescuento = string.Empty;
                            else ent.idtipodescuento = Convert.ToString(res.reader["idtipodescuento"]);

                            if (res.reader["numerotarjeta"] == DBNull.Value) ent.numerotarjeta = string.Empty;
                            else ent.numerotarjeta = Convert.ToString(res.reader["numerotarjeta"]);

                            if (res.reader["folionotadeconsumo"] == DBNull.Value) ent.folionotadeconsumo = 0;
                            else ent.folionotadeconsumo = Convert.ToInt16(res.reader["folionotadeconsumo"]);

                            if (res.reader["notadeconsumo"] == DBNull.Value) ent.notadeconsumo = 0;
                            else ent.notadeconsumo = Convert.ToInt16(res.reader["notadeconsumo"]);

                            if (res.reader["propinapagada"] == DBNull.Value) ent.propinapagada = 0;
                            else ent.propinapagada = Convert.ToInt16(res.reader["propinapagada"]);

                            if (res.reader["propinafoliomovtocaja"] == DBNull.Value) ent.propinafoliomovtocaja = 0;
                            else ent.propinafoliomovtocaja = Convert.ToInt16(res.reader["propinafoliomovtocaja"]);

                            if (res.reader["puntosmonederogenerados"] == DBNull.Value) ent.puntosmonederogenerados = 0;
                            else ent.puntosmonederogenerados = Convert.ToDecimal(Convert.ToString(res.reader["puntosmonederogenerados"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["propinaincluida"] == DBNull.Value) ent.propinaincluida = 0;
                            else ent.propinaincluida = Convert.ToDecimal(Convert.ToString(res.reader["propinaincluida"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["tarjetadescuento"] == DBNull.Value) ent.tarjetadescuento = string.Empty;
                            else ent.tarjetadescuento = Convert.ToString(res.reader["tarjetadescuento"]);

                            if (res.reader["porcentajefac"] == DBNull.Value) ent.porcentajefac = 0;
                            else ent.porcentajefac = Convert.ToInt16(res.reader["porcentajefac"]);

                            if (res.reader["usuariopago"] == DBNull.Value) ent.usuariopago = string.Empty;
                            else ent.usuariopago = Convert.ToString(res.reader["usuariopago"]);

                            if (res.reader["propinamanual"] == DBNull.Value) ent.propinamanual = 0;
                            else ent.propinamanual = Convert.ToInt16(res.reader["propinamanual"]);

                            if (res.reader["observaciones"] == DBNull.Value) ent.observaciones = string.Empty;
                            else ent.observaciones = Convert.ToString(res.reader["observaciones"]);

                            if (res.reader["idclientedomicilio"] == DBNull.Value) ent.idclientedomicilio = string.Empty;
                            else ent.idclientedomicilio = Convert.ToString(res.reader["idclientedomicilio"]);

                            if (res.reader["iddireccion"] == DBNull.Value) ent.iddireccion = string.Empty;
                            else ent.iddireccion = Convert.ToString(res.reader["iddireccion"]);

                            if (res.reader["idclientefacturacion"] == DBNull.Value) ent.idclientefacturacion = string.Empty;
                            else ent.idclientefacturacion = Convert.ToString(res.reader["idclientefacturacion"]);

                            if (res.reader["telefonousadodomicilio"] == DBNull.Value) ent.telefonousadodomicilio = string.Empty;
                            else ent.telefonousadodomicilio = Convert.ToString(res.reader["telefonousadodomicilio"]);

                            if (res.reader["totalarticulos"] == DBNull.Value) ent.totalarticulos = 0;
                            else ent.totalarticulos = Convert.ToInt16(res.reader["totalarticulos"]);

                            if (res.reader["subtotal"] == DBNull.Value) ent.subtotal = 0;
                            else ent.subtotal = Convert.ToDecimal(Convert.ToString(res.reader["subtotal"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["subtotalsinimpuestos"] == DBNull.Value) ent.subtotalsinimpuestos = 0;
                            else ent.subtotalsinimpuestos = Convert.ToDecimal(Convert.ToString(res.reader["subtotalsinimpuestos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["total"] == DBNull.Value) ent.total = 0;
                            else ent.total = Convert.ToDecimal(Convert.ToString(res.reader["total"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalconpropina"] == DBNull.Value) ent.totalconpropina = 0;
                            else ent.totalconpropina = Convert.ToDecimal(Convert.ToString(res.reader["totalconpropina"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalsinimpuestos"] == DBNull.Value) ent.totalsinimpuestos = 0;
                            else ent.totalsinimpuestos = Convert.ToDecimal(Convert.ToString(res.reader["totalsinimpuestos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalsindescuentosinimpuesto"] == DBNull.Value) ent.totalsindescuentosinimpuesto = 0;
                            else ent.totalsindescuentosinimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totalsindescuentosinimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalimpuesto1"] == DBNull.Value) ent.totalimpuesto1 = 0;
                            else ent.totalimpuesto1 = Convert.ToDecimal(Convert.ToString(res.reader["totalimpuesto1"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalalimentosconimpuestos"] == DBNull.Value) ent.totalalimentosconimpuestos = 0;
                            else ent.totalalimentosconimpuestos = Convert.ToDecimal(Convert.ToString(res.reader["totalalimentosconimpuestos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalbebidasconimpuestos"] == DBNull.Value) ent.totalbebidasconimpuestos = 0;
                            else ent.totalbebidasconimpuestos = Convert.ToDecimal(Convert.ToString(res.reader["totalbebidasconimpuestos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalotrosconimpuestos"] == DBNull.Value) ent.totalotrosconimpuestos = 0;
                            else ent.totalotrosconimpuestos = Convert.ToDecimal(Convert.ToString(res.reader["totalotrosconimpuestos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalalimentossinimpuestos"] == DBNull.Value) ent.totalalimentossinimpuestos = 0;
                            else ent.totalalimentossinimpuestos = Convert.ToDecimal(Convert.ToString(res.reader["totalalimentossinimpuestos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalbebidassinimpuestos"] == DBNull.Value) ent.totalbebidassinimpuestos = 0;
                            else ent.totalbebidassinimpuestos = Convert.ToDecimal(Convert.ToString(res.reader["totalbebidassinimpuestos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalotrossinimpuestos"] == DBNull.Value) ent.totalotrossinimpuestos = 0;
                            else ent.totalotrossinimpuestos = Convert.ToDecimal(Convert.ToString(res.reader["totalotrossinimpuestos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentossinimpuestos"] == DBNull.Value) ent.totaldescuentossinimpuestos = 0;
                            else ent.totaldescuentossinimpuestos = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentossinimpuestos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentosconimpuestos"] == DBNull.Value) ent.totaldescuentosconimpuestos = 0;
                            else ent.totaldescuentosconimpuestos = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentosconimpuestos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentoalimentosconimpuesto"] == DBNull.Value) ent.totaldescuentoalimentosconimpuesto = 0;
                            else ent.totaldescuentoalimentosconimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentoalimentosconimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentobebidasconimpuesto"] == DBNull.Value) ent.totaldescuentobebidasconimpuesto = 0;
                            else ent.totaldescuentobebidasconimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentobebidasconimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentootrosconimpuesto"] == DBNull.Value) ent.totaldescuentootrosconimpuesto = 0;
                            else ent.totaldescuentootrosconimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentootrosconimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentoalimentossinimpuesto"] == DBNull.Value) ent.totaldescuentoalimentossinimpuesto = 0;
                            else ent.totaldescuentoalimentossinimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentoalimentossinimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentobebidassinimpuesto"] == DBNull.Value) ent.totaldescuentobebidassinimpuesto = 0;
                            else ent.totaldescuentobebidassinimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentobebidassinimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentootrossinimpuesto"] == DBNull.Value) ent.totaldescuentootrossinimpuesto = 0;
                            else ent.totaldescuentootrossinimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentootrossinimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalcortesiassinimpuestos"] == DBNull.Value) ent.totalcortesiassinimpuestos = 0;
                            else ent.totalcortesiassinimpuestos = Convert.ToDecimal(Convert.ToString(res.reader["totalcortesiassinimpuestos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalcortesiasconimpuestos"] == DBNull.Value) ent.totalcortesiasconimpuestos = 0;
                            else ent.totalcortesiasconimpuestos = Convert.ToDecimal(Convert.ToString(res.reader["totalcortesiasconimpuestos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalcortesiaalimentosconimpuesto"] == DBNull.Value) ent.totalcortesiaalimentosconimpuesto = 0;
                            else ent.totalcortesiaalimentosconimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totalcortesiaalimentosconimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalcortesiabebidasconimpuesto"] == DBNull.Value) ent.totalcortesiabebidasconimpuesto = 0;
                            else ent.totalcortesiabebidasconimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totalcortesiabebidasconimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalcortesiaotrosconimpuesto"] == DBNull.Value) ent.totalcortesiaotrosconimpuesto = 0;
                            else ent.totalcortesiaotrosconimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totalcortesiaotrosconimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalcortesiaalimentossinimpuesto"] == DBNull.Value) ent.totalcortesiaalimentossinimpuesto = 0;
                            else ent.totalcortesiaalimentossinimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totalcortesiaalimentossinimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalcortesiabebidassinimpuesto"] == DBNull.Value) ent.totalcortesiabebidassinimpuesto = 0;
                            else ent.totalcortesiabebidassinimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totalcortesiabebidassinimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalcortesiaotrossinimpuesto"] == DBNull.Value) ent.totalcortesiaotrossinimpuesto = 0;
                            else ent.totalcortesiaotrossinimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totalcortesiaotrossinimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentoycortesiasinimpuesto"] == DBNull.Value) ent.totaldescuentoycortesiasinimpuesto = 0;
                            else ent.totaldescuentoycortesiasinimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentoycortesiasinimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentoycortesiaconimpuesto"] == DBNull.Value) ent.totaldescuentoycortesiaconimpuesto = 0;
                            else ent.totaldescuentoycortesiaconimpuesto = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentoycortesiaconimpuesto"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["cargo"] == DBNull.Value) ent.cargo = 0;
                            else ent.cargo = Convert.ToDecimal(Convert.ToString(res.reader["cargo"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalconcargo"] == DBNull.Value) ent.totalconcargo = 0;
                            else ent.totalconcargo = Convert.ToDecimal(Convert.ToString(res.reader["totalconcargo"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalconpropinacargo"] == DBNull.Value) ent.totalconpropinacargo = 0;
                            else ent.totalconpropinacargo = Convert.ToDecimal(Convert.ToString(res.reader["totalconpropinacargo"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["descuentoimporte"] == DBNull.Value) ent.descuentoimporte = 0;
                            else ent.descuentoimporte = Convert.ToDecimal(Convert.ToString(res.reader["descuentoimporte"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["efectivo"] == DBNull.Value) ent.efectivo = 0;
                            else ent.efectivo = Convert.ToDecimal(Convert.ToString(res.reader["efectivo"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["tarjeta"] == DBNull.Value) ent.tarjeta = 0;
                            else ent.tarjeta = Convert.ToDecimal(Convert.ToString(res.reader["tarjeta"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["vales"] == DBNull.Value) ent.vales = 0;
                            else ent.vales = Convert.ToDecimal(Convert.ToString(res.reader["vales"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["otros"] == DBNull.Value) ent.otros = 0;
                            else ent.otros = Convert.ToDecimal(Convert.ToString(res.reader["otros"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["propina"] == DBNull.Value) ent.propina = 0;
                            else ent.propina = Convert.ToDecimal(Convert.ToString(res.reader["propina"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["propinatarjeta"] == DBNull.Value) ent.propinatarjeta = 0;
                            else ent.propinatarjeta = Convert.ToDecimal(Convert.ToString(res.reader["propinatarjeta"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalalimentossinimpuestossindescuentos"] == DBNull.Value) ent.totalalimentossinimpuestossindescuentos = 0;
                            else ent.totalalimentossinimpuestossindescuentos = Convert.ToDecimal(Convert.ToString(res.reader["totalalimentossinimpuestossindescuentos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalbebidassinimpuestossindescuentos"] == DBNull.Value) ent.totalbebidassinimpuestossindescuentos = 0;
                            else ent.totalbebidassinimpuestossindescuentos = Convert.ToDecimal(Convert.ToString(res.reader["totalbebidassinimpuestossindescuentos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalotrossinimpuestossindescuentos"] == DBNull.Value) ent.totalotrossinimpuestossindescuentos = 0;
                            else ent.totalotrossinimpuestossindescuentos = Convert.ToDecimal(Convert.ToString(res.reader["totalotrossinimpuestossindescuentos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["campoadicional1"] == DBNull.Value) ent.campoadicional1 = string.Empty;
                            else ent.campoadicional1 = Convert.ToString(res.reader["campoadicional1"]);

                            if (res.reader["idreservacion"] == DBNull.Value) ent.idreservacion = string.Empty;
                            else ent.idreservacion = Convert.ToString(res.reader["idreservacion"]);

                            if (res.reader["idcomisionista"] == DBNull.Value) ent.idcomisionista = string.Empty;
                            else ent.idcomisionista = Convert.ToString(res.reader["idcomisionista"]);

                            if (res.reader["importecomision"] == DBNull.Value) ent.importecomision = 0;
                            else ent.importecomision = Convert.ToDecimal(Convert.ToString(res.reader["importecomision"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["comisionpagada"] == DBNull.Value) ent.comisionpagada = 0;
                            else ent.comisionpagada = Convert.ToInt16(res.reader["comisionpagada"]);

                            if (res.reader["fechapagocomision"] == DBNull.Value) ent.fechapagocomision = string.Empty;
                            else ent.fechapagocomision = Convert.ToString(res.reader["fechapagocomision"]);

                            if (res.reader["foliopagocomision"] == DBNull.Value) ent.foliopagocomision = 0;
                            else ent.foliopagocomision = Convert.ToInt16(res.reader["foliopagocomision"]);

                            if (res.reader["tipoventarapida"] == DBNull.Value) ent.tipoventarapida = 0;
                            else ent.tipoventarapida = Convert.ToInt16(res.reader["tipoventarapida"]);

                            if (res.reader["callcenter"] == DBNull.Value) ent.callcenter = 0;
                            else ent.callcenter = Convert.ToInt16(res.reader["callcenter"]);

                            if (res.reader["idordencompra"] == DBNull.Value) ent.idordencompra = 0;
                            else ent.idordencompra = Convert.ToInt64(res.reader["idordencompra"]);

                            if (res.reader["totalsindescuento"] == DBNull.Value) ent.totalsindescuento = 0;
                            else ent.totalsindescuento = Convert.ToDecimal(Convert.ToString(res.reader["totalsindescuento"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalalimentos"] == DBNull.Value) ent.totalalimentos = 0;
                            else ent.totalalimentos = Convert.ToDecimal(Convert.ToString(res.reader["totalalimentos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalbebidas"] == DBNull.Value) ent.totalbebidas = 0;
                            else ent.totalbebidas = Convert.ToDecimal(Convert.ToString(res.reader["totalbebidas"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalotros"] == DBNull.Value) ent.totalotros = 0;
                            else ent.totalotros = Convert.ToDecimal(Convert.ToString(res.reader["totalotros"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentos"] == DBNull.Value) ent.totaldescuentos = 0;
                            else ent.totaldescuentos = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentoalimentos"] == DBNull.Value) ent.totaldescuentoalimentos = 0;
                            else ent.totaldescuentoalimentos = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentoalimentos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentobebidas"] == DBNull.Value) ent.totaldescuentobebidas = 0;
                            else ent.totaldescuentobebidas = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentobebidas"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentootros"] == DBNull.Value) ent.totaldescuentootros = 0;
                            else ent.totaldescuentootros = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentootros"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalcortesias"] == DBNull.Value) ent.totalcortesias = 0;
                            else ent.totalcortesias = Convert.ToDecimal(Convert.ToString(res.reader["totalcortesias"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalcortesiaalimentos"] == DBNull.Value) ent.totalcortesiaalimentos = 0;
                            else ent.totalcortesiaalimentos = Convert.ToDecimal(Convert.ToString(res.reader["totalcortesiaalimentos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalcortesiabebidas"] == DBNull.Value) ent.totalcortesiabebidas = 0;
                            else ent.totalcortesiabebidas = Convert.ToDecimal(Convert.ToString(res.reader["totalcortesiabebidas"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalcortesiaotros"] == DBNull.Value) ent.totalcortesiaotros = 0;
                            else ent.totalcortesiaotros = Convert.ToDecimal(Convert.ToString(res.reader["totalcortesiaotros"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totaldescuentoycortesia"] == DBNull.Value) ent.totaldescuentoycortesia = 0;
                            else ent.totaldescuentoycortesia = Convert.ToDecimal(Convert.ToString(res.reader["totaldescuentoycortesia"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalalimentossindescuentos"] == DBNull.Value) ent.totalalimentossindescuentos = 0;
                            else ent.totalalimentossindescuentos = Convert.ToDecimal(Convert.ToString(res.reader["totalalimentossindescuentos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalbebidassindescuentos"] == DBNull.Value) ent.totalbebidassindescuentos = 0;
                            else ent.totalbebidassindescuentos = Convert.ToDecimal(Convert.ToString(res.reader["totalbebidassindescuentos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["totalotrossindescuentos"] == DBNull.Value) ent.totalotrossindescuentos = 0;
                            else ent.totalotrossindescuentos = Convert.ToDecimal(Convert.ToString(res.reader["totalotrossindescuentos"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["descuentocriterio"] == DBNull.Value) ent.descuentocriterio = 0;
                            else ent.descuentocriterio = Convert.ToInt16(res.reader["descuentocriterio"]);

                            if (res.reader["descuentomonedero"] == DBNull.Value) ent.descuentomonedero = 0;
                            else ent.descuentomonedero = Convert.ToDecimal(Convert.ToString(res.reader["descuentomonedero"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["idmenucomedor"] == DBNull.Value) ent.idmenucomedor = string.Empty;
                            else ent.idmenucomedor = Convert.ToString(res.reader["idmenucomedor"]);

                            if (res.reader["subtotalcondescuento"] == DBNull.Value) ent.subtotalcondescuento = 0;
                            else ent.subtotalcondescuento = Convert.ToDecimal(Convert.ToString(res.reader["subtotalcondescuento"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["comisionpax"] == DBNull.Value) ent.comisionpax = 0;
                            else ent.comisionpax = Convert.ToDecimal(Convert.ToString(res.reader["comisionpax"]).TrimEnd(new Char[] { '0' }));

                            if (res.reader["procesadointerfaz"] == DBNull.Value) ent.procesadointerfaz = 0;
                            else ent.procesadointerfaz = Convert.ToInt16(res.reader["procesadointerfaz"]);

                            if (res.reader["domicilioprogramado"] == DBNull.Value) ent.domicilioprogramado = 0;
                            else ent.domicilioprogramado = Convert.ToInt16(res.reader["domicilioprogramado"]);

                            if (res.reader["fechadomicilioprogramado"] == DBNull.Value) ent.fechadomicilioprogramado = string.Empty;
                            else ent.fechadomicilioprogramado = Convert.ToString(res.reader["fechadomicilioprogramado"]);

                            if (res.reader["enviado"] == DBNull.Value) ent.enviado = 0;
                            else ent.enviado = Convert.ToInt16(res.reader["enviado"]);

                            if (res.reader["ncf"] == DBNull.Value) ent.ncf = string.Empty;
                            else ent.ncf = Convert.ToString(res.reader["ncf"]);

                            if (res.reader["numerocuenta"] == DBNull.Value) ent.numerocuenta = string.Empty;
                            else ent.numerocuenta = Convert.ToString(res.reader["numerocuenta"]);

                            if (res.reader["codigo_unico_af"] == DBNull.Value) ent.codigo_unico_af = string.Empty;
                            else ent.codigo_unico_af = Convert.ToString(res.reader["codigo_unico_af"]);

                            if (res.reader["estatushub"] == DBNull.Value) ent.estatushub = 0;
                            else ent.estatushub = Convert.ToInt16(res.reader["estatushub"]);

                            if (res.reader["idfoliohub"] == DBNull.Value) ent.idfoliohub = 0;
                            else ent.idfoliohub = Convert.ToInt16(res.reader["idfoliohub"]);

                            if (res.reader["EnviadoRW"] == DBNull.Value) ent.EnviadoRW = 0;
                            else ent.EnviadoRW = Convert.ToInt16(res.reader["EnviadoRW"]);

                            result.Add(ent);
                        }
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();
                }
            }

            return result;
        }

        // obtiene datos de FOLIOS para generar respaldo
        public TB_Folios getFolio(string serie)
        {
            Modelos.TB_Folios result = new Modelos.TB_Folios();

            string sql =
                "select serie, ultimofolio, ultimaorden, ultimofolionotadeconsumo, ultimofolioproduccion " +
                "from folios " +
                "where serie = @serie";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionSS.getConexionSS())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@serie", serie);

                    ManejoSql_SS res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        while (res.reader.Read())
                        {
                            if (res.reader["serie"] == DBNull.Value) result.serie = string.Empty;
                            else result.serie = Convert.ToString(res.reader["serie"]);

                            if (res.reader["ultimofolio"] == DBNull.Value) result.ultimofolio = 0;
                            else result.ultimofolio = Convert.ToInt64(res.reader["ultimofolio"]);

                            if (res.reader["ultimaorden"] == DBNull.Value) result.ultimaorden = 0;
                            else result.ultimaorden = Convert.ToInt64(res.reader["ultimaorden"]);

                            if (res.reader["ultimofolionotadeconsumo"] == DBNull.Value) result.ultimofolionotadeconsumo = 0;
                            else result.ultimofolionotadeconsumo = Convert.ToInt64(res.reader["ultimofolionotadeconsumo"]);

                            if (res.reader["ultimofolioproduccion"] == DBNull.Value) result.ultimofolioproduccion = 0;
                            else result.ultimofolioproduccion = Convert.ToInt64(res.reader["ultimofolioproduccion"]);
                        }
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();
                }
            }

            return result;
        }

        // cambia folio de cuentas facturadas
        public bool cambiaFolioFacturados(string serie, List<long> foliosFact, string estacion, long ultimoFolio)
        {
            bool result = false;

            SqlTransaction trans;

            int rows = 0;

            using (var conn = this._conexionSS.getConexionSS())
            {
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    trans = conn.BeginTransaction();

                    cmd.Connection = conn;
                    cmd.Transaction = trans;

                    /* REFOLIAR */

                    string sqlRF = "update cheques set numcheque = @numCheque where estacion = @idEstacion and folio = @folio";

                    cmd.Parameters.Clear();

                    foreach (long fl in foliosFact)
                    {
                        // define parametros
                        cmd.Parameters.AddWithValue("@idEstacion", estacion);
                        cmd.Parameters.AddWithValue("@folio", fl);
                        cmd.Parameters.AddWithValue("@numCheque", ultimoFolio);

                        ManejoSql_SS res3 = Utilerias.EjecutaSQL(sqlRF, ref rows, cmd);

                        if (!res3.ok)
                        {
                            trans.Rollback();
                            throw new Exception(res3.numErr + ": " + res3.descErr);
                        }

                        ultimoFolio++;
                        cmd.Parameters.Clear();
                    }

                    /* CAMBIA FOLIO - SECUENCIA */
                    string sqlF = "update folios set ultimofolio = @ultFolio where serie = @serie";

                    cmd.Parameters.Clear();

                    // define parametros
                    cmd.Parameters.AddWithValue("@ultFolio", ultimoFolio - 1);
                    cmd.Parameters.AddWithValue("@serie", serie);

                    ManejoSql_SS res4 = Utilerias.EjecutaSQL(sqlF, ref rows, cmd);

                    if (!res4.ok)
                    {
                        trans.Rollback();
                        throw new Exception(res4.numErr + ": " + res4.descErr);
                    }

                    trans.Commit();
                    result = true;
                }
            }

            return result;
        }
    }
}
