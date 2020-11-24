using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.Models
{
    public class RespuestaOperacion<T>
        where T : class
    {
        public T Resultado { get; private set; }
        public bool TieneError { get; private set; }
        public string MensajeError { get; private set; }

        public RespuestaOperacion(T resultado)
        {
            Resultado = resultado;
        }

        public RespuestaOperacion(string mensajeError, bool tieneError = true)
        {
            MensajeError = mensajeError;
            TieneError = tieneError;
        }
    }
}