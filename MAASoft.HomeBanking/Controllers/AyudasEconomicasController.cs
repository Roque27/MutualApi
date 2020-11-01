using MAASoft.HomeBanking.DataAccess;
using MAASoft.HomeBanking.Models;
using MAASoft.HomeBanking.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MAASoft.HomeBanking.Controllers
{
    /// <summary>
    /// Web Services Ayudas Economicas.
    /// </summary>
    [ValidarTokenAcceso]
    public class AyudasEconomicasController : ApiController
    {
        private readonly log4net.ILog logger;
        QueriesAyudasEconomicas consulta;

        public AyudasEconomicasController()
        {
            logger = log4net.LogManager.GetLogger("AyudasEconomicas");
            consulta = new QueriesAyudasEconomicas();
        }

        /// <summary>
        /// Ayudas Economicas Vigentes o Pendientes de Cancelacion.
        /// </summary>
        /// <param name="socio">Numero de Socio.</param>
        /// <returns>Devuelve lista de objetos AyudaEconomicaVigente.</returns>
        [HttpGet]
        public RespuestaOperacion<IEnumerable<AyudaEconomicaVigente>> ObtenerAyudasEconomicasVigentes(int socio, DateTime desde, DateTime hasta)
        {
            try
            {
                return new RespuestaOperacion<IEnumerable<AyudaEconomicaVigente>>(consulta.QueryObtenerAyudasEconomicasVigentes(socio, desde, hasta));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<IEnumerable<AyudaEconomicaVigente>>("No se pudieron obtener las ayudas economicas vigentes.");
            }
        }

        /// <summary>
        /// Detalle de Cuotas de Ayudas Economicas Pendientes de Cancelacion.
        /// </summary>
        /// <param name="ayuda">Numero de Ayuda a Consultar.</param>
        /// <returns>Devuelve lista de objetos DetalleCuotaAyuda.</returns>
        [HttpGet]
        public RespuestaOperacion<IEnumerable<DetalleCuotaAyuda>> DetalleDeCuotasAyudasEconomicas(int ayuda)
        {
            try
            {
                return new RespuestaOperacion<IEnumerable<DetalleCuotaAyuda>>(consulta.QueryDetalleDeCuotas(ayuda));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<IEnumerable<DetalleCuotaAyuda>>("No se pudo obtener el detalle de las ayudas economicas pendientes de cancelacion.");
            }
        }

        /// <summary>
        /// Detalle de Documentos de Ayudas Economicas Pendientes de Cancelacion.
        /// </summary>
        /// <param name="ayuda">Numero de Ayuda a Consultar.</param>
        /// <returns>Devuelve lista de objetos DetalleDocumentoAyuda.</returns>
        [HttpGet]
        public RespuestaOperacion<IEnumerable<DetalleDocumentoAyuda>> DetalleDeDocumentosAyudasEconomicas(int ayuda)
        {
            try
            {
                return new RespuestaOperacion<IEnumerable<DetalleDocumentoAyuda>>(consulta.QueryDetalleDeDocumentos(ayuda));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<IEnumerable<DetalleDocumentoAyuda>>("No se pudo obtener el detalle de los documentos de las ayudas economicas pendientes de cancelacion.");
            }
        }

        /// <summary>
        /// Detalle de Cheques de Ayudas Economicas Pendientes de Acreditacion.
        /// </summary>
        /// <param name="ayuda">Numero de Ayuda a Consultar.</param>
        /// <returns>Devuelve lista de objetos DetalleChequeAyuda.</returns>
        [HttpGet]
        public RespuestaOperacion<IEnumerable<DetalleChequeAyuda>> DetalleDeChequesAyudasEconomicas(int ayuda)
        {
            try
            {
                return new RespuestaOperacion<IEnumerable<DetalleChequeAyuda>>(consulta.QueryDetalleDeCheques(ayuda));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<IEnumerable<DetalleChequeAyuda>>("No se pudo obtener el detalle de los cheques de las ayudas economicas pendientes de acreditacion.");
            }
        }

        /// <summary>
        /// Detalle de Valores Negociados de Ayudas Economicas Pendientes de Acreditacion.
        /// </summary>
        /// <param name="cuenta">Numero de Socio.</param>
        /// <returns>Devuelve lista de objetos DetalleValorNegociadoAyuda.</returns>
        [HttpGet]
        public RespuestaOperacion<IEnumerable<DetalleValorNegociadoAyuda>> DetalleDeValoresNegociadosAyudasEconomicas(int cuenta)
        {
            try
            {
                return new RespuestaOperacion<IEnumerable<DetalleValorNegociadoAyuda>>(consulta.QueryDetalleDeValoresNegociados(cuenta));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<IEnumerable<DetalleValorNegociadoAyuda>>("No se pudo obtener el detalle de los valores negociados de las ayudas economicas pendientes de acreditacion.");
            }
        }
    }
}
