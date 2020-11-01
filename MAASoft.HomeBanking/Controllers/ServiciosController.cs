using MAASoft.HomeBanking.DataAccess;
using MAASoft.HomeBanking.Models;
using MAASoft.HomeBanking.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MAASoft.HomeBanking.Controllers
{
    [ValidarTokenAcceso]
    public class ServiciosController : ApiController
    {
        private readonly log4net.ILog logger;
        QueriesServicios consulta;

        public ServiciosController()
        {
            logger = log4net.LogManager.GetLogger("Servicios");
            consulta = new QueriesServicios();
        }

        [HttpGet]
        public RespuestaOperacion<IEnumerable<ServicioCuota>> ObtenerCuotasPorSocio(int socio)
        {
            try
            {
                return new RespuestaOperacion<IEnumerable<ServicioCuota>>(consulta.QueryCuotasPorSocio(socio));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<IEnumerable<ServicioCuota>>("No se pudieron obtener las cuotas de servicios del socio.");
            }
        }
    }
}