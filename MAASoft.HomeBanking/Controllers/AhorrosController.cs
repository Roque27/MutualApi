using MAASoft.HomeBanking.DataAccess;
using MAASoft.HomeBanking.Seguridad;
using MAASoft.HomeBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MAASoft.HomeBanking.Controllers
{
    /// <summary>
    /// Web Services Ahorros.
    /// </summary>
    [ValidarTokenAcceso]
    public class AhorrosController : ApiController
    {
        private readonly log4net.ILog logger;
        QueriesAhorros consulta;

        public AhorrosController()
        {
            logger = log4net.LogManager.GetLogger("Ahorros");
            consulta = new QueriesAhorros();
        }

        /// <summary>
        /// Consultas Saldo en Caja de Ahorros.
        /// </summary>
        /// <param name="cuenta">Cuenta del socio.</param>
        /// <param name="tipo">Tipo de cuenta es el ultimo caracter a la derecha del comprobante (idCompro) se toma de la tabla comprobantes modulo "AMV" y pueden ser Ah.Variable Comun, Especial o diferencial=right(cCompro,1).</param>
        /// <returns>Devuelve lista de objetos SaldoCajaAhorro.</returns>
        [HttpGet]
        public RespuestaOperacion<SaldoCajaAhorro> ObtenerSaldoCajaDeAhorro(int cuenta, string tipo)
        {
            try
            {
                var saldo = consulta.QueryObtenerSaldoCajaDeAhorro(cuenta, tipo);
                if (saldo == null)
                {
                    return new RespuestaOperacion<SaldoCajaAhorro>("No se encontró la cuenta solicitada.");
                }

                return new RespuestaOperacion<SaldoCajaAhorro>(saldo);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<SaldoCajaAhorro>("No se pudo obtener el saldo de la caja de ahorro.");
            }
        }

        /// <summary>
        /// Consultas Resumen de cuentas de Socios.
        /// </summary>
        /// <param name="cuenta">Cuenta del socio.</param>
        /// <param name="comprobante">Tipo de comprobante se toma de la tabla comprobantes  modulo "AMV" y pueden ser Ah.Variable Comun, Especial o diferencial.</param>
        /// <param name="desde">Fecha desde a consultar.</param>
        /// <param name="hasta">Fecha hasta a consultar.</param>
        /// <returns>Devuelve lista de objetos ResumenCuenta.</returns>
        [HttpGet]
        public RespuestaOperacion<IEnumerable<ResumenCuenta>> ResumenCuentasSocios(int cuenta, string comprobante, DateTime desde, DateTime hasta)
        {
            try
            {
                return new RespuestaOperacion<IEnumerable<ResumenCuenta>>(consulta.QueryResumenDeCuentasDeSocios(cuenta, comprobante, desde, hasta));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<IEnumerable<ResumenCuenta>>("No se pudo obtener el resúmen de cuenta del socio.");
            }
        }

        /// <summary>
        /// Ahorros a Termino Vigentes.
        /// </summary>
        /// <param name="cuenta">Cuenta del socio.</param>
        /// <param name="desde">Fecha desde a consultar.</param>
        /// <param name="hasta">Fecha hasta a consultar.</param>
        /// <param name="moneda">Pesos o Dalares.</param>
        /// <returns>Devuelve lista de objetos AhorroTerminoVigente.</returns>
        [HttpGet]
        public RespuestaOperacion<IEnumerable<AhorroTerminoVigente>> ObtenerAhorrosATerminoVigentes(int cuenta, DateTime desde, DateTime hasta, string moneda)
        {
            try
            {
                return new RespuestaOperacion<IEnumerable<AhorroTerminoVigente>>(consulta.QueryObtenerAhorrosATerminosVigentes(cuenta, desde, hasta, moneda));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<IEnumerable<AhorroTerminoVigente>>("No se pudieron obtener los ahorros a término vigentes.");
            }
        }

        /// <summary>
        /// Detalle de Valores al Cobro de Acreditacion.
        /// </summary>
        /// <param name="cuenta">Numero de Socio.</param>
        /// <returns>Devuelve lista de objetos DetalleValorCobroAcreditacion.</returns>
        [HttpGet]
        public RespuestaOperacion<IEnumerable<DetalleValorCobroAcreditacion>> DetalleDeValoresAlCobroDeAcreditacion(int cuenta)
        {
            try
            {
                return new RespuestaOperacion<IEnumerable<DetalleValorCobroAcreditacion>>(consulta.QueryDetalleDeValoresAlCobroDeAcreditacion(cuenta));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<IEnumerable<DetalleValorCobroAcreditacion>>("No se pudo obtener el detalle de los valores al cobro de acreditacion.");
            }
        }
    }
}
