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
    /// Web Services Impuestos.
    /// </summary>
    [ValidarTokenAcceso]
    public class ImpuestosController : ApiController
    {
        private readonly log4net.ILog logger;
        QueriesImpuestos consulta;

        public ImpuestosController()
        {
            logger = log4net.LogManager.GetLogger("Impuestos");
            consulta = new QueriesImpuestos();
        }

        /// <summary>
        /// Impuestos Pendientes
        /// </summary>
        /// <param name="cuenta">Cuenta del socio.</param>
        /// <returns>Devuelve lista de objetos ImpuestoPendiente.</returns>
        [HttpGet]
        public RespuestaOperacion<IEnumerable<ImpuestoPendiente>> ObtenerImpuestosPendientes(int cuenta)
        {
            try
            {
                return new RespuestaOperacion<IEnumerable<ImpuestoPendiente>>(consulta.QueryObtenerImpuestosPendientes(cuenta));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<IEnumerable<ImpuestoPendiente>>("No se pudieron obtener los impuestos pendientes.");
            }
        }
    }
}
