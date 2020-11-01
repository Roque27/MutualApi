using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.Models
{
    public class DetalleDocumentoAyuda
    {
        public long Ayuda { get; set; }

        public string Tipo { get; set; }

        public DateTime FechaAcr { get; set; }

        public string Banco { get; set; }

        public decimal Importe { get; set; }
    }
}