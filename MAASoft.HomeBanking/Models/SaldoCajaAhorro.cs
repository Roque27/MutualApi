using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.Models
{
    public class SaldoCajaAhorro
    {
        public string Tipo { get; set; }

        public long Cuenta { get; set; }

        public decimal Saldo { get; set; }
    }
}