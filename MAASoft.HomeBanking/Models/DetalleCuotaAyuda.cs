using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.Models
{
    public class DetalleCuotaAyuda
    {
        public long Ayuda { get; set; }

        public string Moneda { get; set; }

        public DateTime FechaVto { get; set; }

        public long NroCuota { get; set; }

        public decimal ValorCuota { get; set; }
    }
}