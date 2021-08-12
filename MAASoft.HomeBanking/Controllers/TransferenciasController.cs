using System;
using System.Linq;
using System.Web;
using System.Web.Http;

using MAASoft.HomeBanking.DataAccess;
using MAASoft.HomeBanking.Models;
using MAASoft.HomeBanking.Seguridad;

namespace MAASoft.HomeBanking.Controllers
{
    /// <summary>
    /// Web Service Transferencias.
    /// </summary>
    [ValidarTokenAcceso]
    public class TransferenciasController : ApiController
    {
        private readonly log4net.ILog logger;
        QueriesTransferencias consulta;

        public TransferenciasController()
        {
            logger = log4net.LogManager.GetLogger("Transferencias");
            consulta = new QueriesTransferencias();
        }

        [HttpPost]
        public RespuestaOperacion<TransferenciaRealizada> InsertarMovimientoEntreCuentas([FromBody] TransferenciaRealizada transferencia)
        {
            try
            {
                if (consulta.QueryInsertarMovimientoEntreCuentas(transferencia.CuentaOrigen, transferencia.CuentaDestino, transferencia.Tipo, transferencia.Monto) == Queries.RespuestaQuery.OK)
                    return new RespuestaOperacion<TransferenciaRealizada>("OK", false);
                else
                    return new RespuestaOperacion<TransferenciaRealizada>("No se pudo actualizar el movimiento entre cuentas.");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + " ,InnerException: " + (ex.InnerException != null ? ex.InnerException.Message : String.Empty), ex);
                return new RespuestaOperacion<TransferenciaRealizada>(@"{'Msj':'Ocurrio un error y no se pudo actualizar el movimiento entre cuentas.'}");
            }
        }
    }
}