using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MttoVentas.Modelos
{
    public class ConexionSS : IConexionSS
    {
        private string _cadenaConexionSS;

        public ConexionSS(string cadenaConexionSS)
        {
            this._cadenaConexionSS = cadenaConexionSS;
        }

        public string getCadenaSS()
        {
            return this._cadenaConexionSS;
        }

        public SqlConnection getConexionSS()
        {
            return new SqlConnection(this._cadenaConexionSS);
        }
    }
}
