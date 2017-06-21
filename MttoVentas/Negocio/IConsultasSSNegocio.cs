using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MttoVentas.Negocio
{
    public interface IConsultasSSNegocio
    {
        bool pruebaConn();

        string getFecha();

        List<Modelos.Folios> obtFolios();

        List<Modelos.Turnos> getTurnosAbiertos();

        bool buscaVentas(long idTurno);

        List<Modelos.Estaciones> getEstaciones();

        string buscaSerie(string idEstacion);

        bool cambiaTurno(string serie, string estacion);

        List<Modelos.Cuentas> getCuentas(string fechaIni, string fechaFin, string serie, string estacion);

        bool elimFolios(List<long> folios, string estacion, List<long> refolios, string nvoFolio, string serie);

        string getUltFolio(string serie);

        List<Modelos.TB_Chequespagos> getChequesPagos(List<long> folios);

        List<Modelos.TB_Cheqdet> getCheqdet(string estacion, List<long> folios);

        List<Modelos.TB_Cheques> getCheques(string estacion, List<long> folios);

        Modelos.TB_Folios getFolio(string serie);

        bool cambiaFolioFacturados(string serie, List<long> foliosFact, string estacion);
    }
}
