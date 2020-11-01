using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using MAASoft.HomeBanking.DataAccess;
using MAASoft.HomeBanking.Models;
using MAASoft.HomeBanking.Seguridad;

namespace MAASoft.HomeBanking.Controllers
{
    /// <summary>
    /// Web Service Socios.
    /// </summary>
    [ValidarTokenAcceso]
    public class SociosController : ApiController
    {
        private readonly log4net.ILog logger;
        QueriesSocios consulta;

        public SociosController()
        {
            logger = log4net.LogManager.GetLogger("Socios");
            consulta = new QueriesSocios();
        }

        [HttpGet]
        public RespuestaOperacion<Socio> ObtenerSocioPorDNI(int nroDocumento)
        {
            try
            {
                return new RespuestaOperacion<Socio>(consulta.QuerySocioPorDNI(nroDocumento));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<Socio>("No se pudo obtener el socio por DNI.");
            }
        }

        [HttpGet]
        public RespuestaOperacion<Socio> ObtenerSocioPorCUIT(long cuit)
        {
            // NOTA (ML): el tipo correcto del parametro "cuit" es long aunque en el método "QuerySocioPorCUIT" usemos double.
            try
            {
                return new RespuestaOperacion<Socio>(consulta.QuerySocioPorCUIT(cuit));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<Socio>("No se pudo obtener el socio por CUIT.");
            }
        }

        //COMIENZO NUEVO METODO

        [HttpGet]
        public RespuestaOperacion<IEnumerable<Socio>> ObtenerSocioPorNombre(string nombre)
        {
            try
            {
                return new RespuestaOperacion<IEnumerable<Socio>>(consulta.QuerySocioPorNombre(nombre));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<IEnumerable<Socio>>("No se pudieron obtener socios por nombre.");
            }
        }

        //FIN NUEVO METODO

    }
}