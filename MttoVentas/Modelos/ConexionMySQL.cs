using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MttoVentas.Modelos
{
    public class ConexionMySQL : IConexionMySQL
    {
        private string _cadenaConexionMySQL;

        public ConexionMySQL(string cadenaConexionMySQL)
        {
            this._cadenaConexionMySQL = cadenaConexionMySQL;
        }

        public string getCadenaMySQL()
        {
            return this._cadenaConexionMySQL;
        }

        public MySqlConnection getConexionMySQL()
        {
            return new MySqlConnection(this._cadenaConexionMySQL);
        }
    }
}
