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
    /// Web Services Cuotas Societarias.
    /// </summary>
    [ValidarTokenAcceso]
    public class CuotasSocietariasController : ApiController
    {
        private readonly log4net.ILog logger;
        QueriesCuotasSocietarias consulta;

        public CuotasSocietariasController()
        {
            logger = log4net.LogManager.GetLogger("CuotasSocietarias");
            consulta = new QueriesCuotasSocietarias();
        }

        /// <summary>
        /// Consultas Cuotas Societarias.
        /// </summary>
        /// <param name="cuenta">Cuenta del socio.</param>
        /// <param name="desde">Fecha desde a consultar.</param>
        /// <param name="hasta">Fecha hasta a consultar.</param>
        /// <returns>Devuelve lista de objetos CuotaSocietaria.</returns>
        [HttpGet]
        public RespuestaOperacion<IEnumerable<CuotaSocietaria>> ObtenerCuotasSocietarias(int cuenta, DateTime desde, DateTime hasta)
        {
            try
            {
                return new RespuestaOperacion<IEnumerable<CuotaSocietaria>>(consulta.QueryCuotasSocietarias(cuenta, desde, hasta));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<IEnumerable<CuotaSocietaria>>("No se pudieron obtener las cuotas societarias.");
            }
        }
    }
}
