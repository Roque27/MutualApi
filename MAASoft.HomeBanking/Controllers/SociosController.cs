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

        [HttpGet]
        public RespuestaOperacion<Socio> ObtenerSocioPorNombreYEmail(string nombre, string email)
        {
            List<Socio> Socios;
            try
            {
                Socios = consulta.QuerySocioPorNombre(nombre);

                if (Socios != null && Socios.Count() > 0)
                    if (Socios.Count() > 1)
                    {
                        Socio Socio = Socios.FirstOrDefault(s => s.Email.Equals(email));
                        
                        return new RespuestaOperacion<Socio>(Socio);
                    }
                    else
                        return new RespuestaOperacion<Socio>(Socios.FirstOrDefault());
                else
                    return new RespuestaOperacion<Socio>("No existe el socio");

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<Socio>("No se pudieron obtener socios por nombre y email.");
            }
        }

        [HttpGet]
        public RespuestaOperacion<Socio> ObtenerSocioPorNroSocio(int nrosocio)
        {
            try
            {
                return new RespuestaOperacion<Socio>(consulta.QuerySocioPorNroSocio(nrosocio).First());
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return new RespuestaOperacion<Socio>("No se pudieron obtener socios por nombre y email.");
            }
        }

        [HttpPost]
        public RespuestaOperacion<String> ActualizarSocio([FromBody] Socio socio)
        {
            try
            {
                if (consulta.QueryActualizarSocio(socio) == Queries.RespuestaQuery.OK)
                    return new RespuestaOperacion<string>("OK", false);
                else
                    return new RespuestaOperacion<string>("No se pudo actualizar la informacion del socio.");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + " ,InnerException: " + (ex.InnerException != null? ex.InnerException.Message : String.Empty), ex);
                return new RespuestaOperacion<string>("Ocurrio un error y no se pudo actualizar la informacion del socio.");
            }
        }

        public class Socio2
        {
            public int Codigo { get; set; }
            public string Nombre { get; set; }
            public string Domicilio { get; set; }
            public string Localidad { get; set; }
            public string CodPostal { get; set; }
            public string Telefono { get; set; }
            public string Fax { get; set; }
            public string Celular { get; set; }
            public string Email { get; set; }
            public char Socade { get; set; }
            public decimal Cuota { get; set; }
            public DateTime FechaIngreso { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public string TipoDocumento { get; set; }
            public long NroDocumento { get; set; }
            public long CUIT { get; set; }
            public string SituacionIva { get; set; }
        }
    }
}