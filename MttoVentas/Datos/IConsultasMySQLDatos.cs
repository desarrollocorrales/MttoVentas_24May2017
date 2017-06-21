using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MttoVentas.Datos
{
    public interface IConsultasMySQLDatos
    {
        bool pruebaConn();

        long generaBitacora(string detalle, string fecha);

        long getUltMvto();

        void insertRespCheques(List<Modelos.TB_Cheques> cheques, List<long> folios, 
            List<Modelos.TB_Cheques> chequesF, List<long> refolios,
            List<Modelos.TB_Cheques> chequesFF, List<long> foliosFact,
            List<Modelos.TB_Cheqdet> cheqdet, List<Modelos.TB_Chequespagos> chequesPagos, 
            Modelos.TB_Folios tb_fol, long mvto);
    }
}
