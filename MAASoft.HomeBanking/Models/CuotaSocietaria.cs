using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.Models
{
    public class CuotaSocietaria
    {
        public DateTime Fecha { get; set; }

        public decimal Importe { get; set; }

        public DateTime FechaDePago { get; set; }

        public string Estado { get; set; }
    }
}