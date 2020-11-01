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
    /// Web Services Facturacion.
    /// </summary>
    [ValidarTokenAcceso]
    public class FacturacionController : ApiController
    {
        private readonly log4net.ILog logger;
        QueriesFacturacion consulta;

        public FacturacionController()
        {
            logger = log4net.LogManager.GetLogger("Facturacion");
            consulta = new QueriesFacturacion();
        }

        /// <summary>
        /// Facturas del Servicio de Internet.
        /// </summary>
        /// <param name="cuenta">Cuenta del socio.</param>
        /// <param name="desde">Fecha desde a consultar.</param>
        /// <param name="hasta">Fecha hasta a consultar.</param>
        /// <returns>Devuelve lista de objetos FacturaInternet.</returns>
        [HttpGet]
        public RespuestaOperacion<IEnumerable<FacturaInternet>> ObtenerFacturasServicioDeInternet(int cuenta, DateTime desde, DateTime hasta)
        {
            try
            {
                return new RespuestaOperacion<IEnumerable<FacturaInternet>>(consulta.QueryObtenerFacturasDelServicioDeInternet(cuenta, desde, hasta));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<IEnumerable<FacturaInternet>>("No se pudieron obtener las facturas del servicio de internet.");
            }
        }
    }
}
