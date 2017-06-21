using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MttoVentas.Modelos
{
    public interface IConexionSS
    {
        SqlConnection getConexionSS();
    }
}
