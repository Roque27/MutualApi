﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.Models
{
    public class DetalleValorCobroAcreditacion
    {
        public string Tipo { get; set; }

        public DateTime FecDep { get; set; }

        public DateTime FechaAcr { get; set; }

        public string Banco { get; set; }

        public string Localidad { get; set; }

        public long Cheque { get; set; }

        public decimal Importe { get; set; }
    }
}