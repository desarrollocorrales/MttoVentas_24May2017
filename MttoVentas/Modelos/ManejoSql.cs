using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace MttoVentas.Modelos
{
    public class ManejoSql_My
    {
        public bool ok { get; set; }
        public MySqlDataReader reader { get; set; }
        public int numErr { get; set; }
        public string descErr { get; set; }
    }

    public class ManejoSql_SS
    {
        public bool ok { get; set; }
        public SqlDataReader reader { get; set; }
        public int numErr { get; set; }
        public string descErr { get; set; }
    }
}
