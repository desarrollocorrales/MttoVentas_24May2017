using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MttoVentas.Datos;

namespace MttoVentas.Negocio
{
    public class ConsultasMySQLNegocio : IConsultasMySQLNegocio
    {
        private IConsultasMySQLDatos _consultasMySQLDatos;

        public ConsultasMySQLNegocio()
        {
            this._consultasMySQLDatos = new ConsultasMySQLDatos();
        }

        public bool pruebaConn()
        {
            return this._consultasMySQLDatos.pruebaConn();
        }
        
        public long generaBitacora(string detalle, string fecha)
        {
            return this._consultasMySQLDatos.generaBitacora(detalle, fecha);
        }

        public long getUltMvto()
        {
            return this._consultasMySQLDatos.getUltMvto();
        }

        public void insertRespCheques(List<Modelos.TB_Cheques> cheques, List<long> folios, 
            List<Modelos.TB_Cheques> chequesF, List<long> refolios,
            List<Modelos.TB_Cheques> chequesFF, List<long> foliosFact,
            List<Modelos.TB_Cheqdet> cheqdet, List<Modelos.TB_Chequespagos> chequesPagos, 
            Modelos.TB_Folios tb_fol, long mvto)
        {
            this._consultasMySQLDatos.insertRespCheques(cheques, folios, chequesF, refolios, chequesFF, foliosFact, cheqdet, chequesPagos, tb_fol, mvto);
        }
    }
}
