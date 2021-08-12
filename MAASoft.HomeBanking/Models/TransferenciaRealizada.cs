using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.Models
{
    public class TransferenciaRealizada
    {
        public int CuentaOrigen { get; set; }
        public int CuentaDestino { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
    }
}