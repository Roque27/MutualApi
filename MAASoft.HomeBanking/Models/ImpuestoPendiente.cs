using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.Models
{
    public class ImpuestoPendiente
    {
        public DateTime FechaVto { get; set; }

        public string NroBol { get; set; }

        public string Nombre { get; set; }

        public decimal Importe { get; set; }
    }
}