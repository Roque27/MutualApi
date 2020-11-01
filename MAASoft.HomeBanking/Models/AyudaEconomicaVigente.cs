using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.Models
{
    public class AyudaEconomicaVigente
    {
        public DateTime Fecha { get; set; }

        public string Tipo { get; set; }

        public string Comprobantes { get; set; }

        public long Ayuda { get; set; }

        public int Plazo { get; set; }

        public DateTime FechaVto { get; set; }

        public decimal Total { get; set; }

        public int Cuotas { get; set; }
    }
}