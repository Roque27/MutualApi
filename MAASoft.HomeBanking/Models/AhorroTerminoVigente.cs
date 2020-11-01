using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.Models
{
    public class AhorroTerminoVigente
    {
        public DateTime Fecha { get; set; }

        public DateTime FechaVto { get; set; }

        public long Numero { get; set; }

        public int Plazo { get; set; }

        public decimal TEM { get; set; }

        public decimal TNA { get; set; }

        public decimal Deposito { get; set; }

        public decimal Estimu { get; set; }

        public decimal Sello { get; set; }

        public decimal Total { get; set; }
    }
}