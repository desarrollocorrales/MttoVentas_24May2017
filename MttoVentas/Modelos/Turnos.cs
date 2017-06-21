using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MttoVentas.Modelos
{
    public class Turnos
    {
        public long idTurnoInterno { get; set; }
        public long idTurno { get; set; }
        public string apertura { get; set; }
        public string cierre { get; set; }
        public string estacion { get; set; }
    }
}
