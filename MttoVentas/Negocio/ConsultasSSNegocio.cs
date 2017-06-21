using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MttoVentas.Datos;

namespace MttoVentas.Negocio
{
    public class ConsultasSSNegocio : IConsultasSSNegocio
    {
        private IConsultasSSDatos _consultasSSDatos;

        public ConsultasSSNegocio()
        {
            this._consultasSSDatos = new ConsultasSSDatos();
        }

        public bool pruebaConn()
        {
            return this._consultasSSDatos.pruebaConn();
        }

        public string getFecha()
        {
            return this._consultasSSDatos.getFecha();
        }

        public List<Modelos.Folios> obtFolios()
        {
            return this._consultasSSDatos.obtFolios();
        }

        public List<Modelos.Turnos> getTurnosAbiertos()
        {
            return this._consultasSSDatos.getTurnosAbiertos();
        }

        public bool buscaVentas(long idTurno)
        {
            return this._consultasSSDatos.buscaVentas(idTurno);
        }

        public List<Modelos.Estaciones> getEstaciones()
        {
            return this._consultasSSDatos.getEstaciones();
        }

        public string buscaSerie(string idEstacion)
        {
            return this._consultasSSDatos.buscaSerie(idEstacion);
        }

        public bool cambiaTurno(string serie, string estacion)
        {
            return this._consultasSSDatos.cambiaTurno(serie, estacion);
        }

        public List<Modelos.Cuentas> getCuentas(string fechaIni, string fechaFin, string serie, string estacion)
        {
            return this._consultasSSDatos.getCuentas(fechaIni, fechaFin, serie, estacion);
        }

        public bool elimFolios(List<long> folios, string estacion, List<long> refolios, string nvoFolio, string serie)
        {
            bool result = this._consultasSSDatos.elimFolios(folios, estacion, refolios, nvoFolio, serie);

            this._consultasSSDatos.refoliar(folios, estacion, refolios, nvoFolio, serie);

            return result;
        }

        public string getUltFolio(string serie)
        {
            return this._consultasSSDatos.getUltFolio(serie);
        }

        public List<Modelos.TB_Chequespagos> getChequesPagos(List<long> folios)
        {
            return this._consultasSSDatos.getChequesPagos(folios);
        }

        public List<Modelos.TB_Cheqdet> getCheqdet(string estacion, List<long> folios)
        {
            return this._consultasSSDatos.getCheqdet(estacion, folios);
        }

        public List<Modelos.TB_Cheques> getCheques(string estacion, List<long> folios)
        {
            return this._consultasSSDatos.getCheques(estacion, folios);
        }

        public Modelos.TB_Folios getFolio(string serie)
        {
            return this._consultasSSDatos.getFolio(serie);
        }

        public bool cambiaFolioFacturados(string serie, List<long> foliosFact, string estacion)
        {
            // obtiene el siguiente numero para el folio segun la serie
            Modelos.TB_Folios ultimoFolio = this._consultasSSDatos.getFolio(serie);

            // cambia folio de cuentas facturadas
            return this._consultasSSDatos.cambiaFolioFacturados(serie, foliosFact, estacion, ultimoFolio.ultimofolio + 1);
        }
    }
}
