using MAASoft.HomeBanking.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.DataAccess
{
    public class QueriesServicios : Queries
    {
        public List<ServicioCuota> QueryCuotasPorSocio(int nSocio)
        {
            var lista = new List<ServicioCuota>();

            string cmd =
                "SELECT servCtas.socio, servCtas.fecha, servicios.nombre, servCtas.numero, servCtas.periodo, servCtas.importe, servCtas.gastos, servCtas.total, servCtas.fecha_pago, servCtas.cuota, servCtas.plan" +
                " FROM servCtas, servicios" +
                " WHERE servCtas.socio = ? AND servCtas.servicio = servicios.compro" +
                " ORDER BY servCtas.socio, servCtas.servicio, servCtas.fecha";

            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {
                    cmdOleDb.Parameters.AddWithValue("nSocio", nSocio);

                    using (var reader = cmdOleDb.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MapearServicioCuota(reader));
                        }
                        reader.Close();
                    }
                }

                conn.Close();
            }

            return lista;
        }

        private ServicioCuota MapearServicioCuota(OleDbDataReader reader)
        {
            return new ServicioCuota
            {
                Fecha = Convert.ToDateTime(reader["fecha"]),
                Nombre = Convert.ToString(reader["nombre"]).Trim(),
                Numero = Convert.ToInt32(reader["numero"]),
                Periodo = Convert.ToInt32(reader["periodo"]),
                Importe = Convert.ToDecimal(reader["importe"]),
                Gastos = Convert.ToDecimal(reader["gastos"]),
                Total = Convert.ToDecimal(reader["total"]),
                FechaPago = MapearDateTimeNullable(reader["fecha_pago"]),
                Cuota = Convert.ToInt32(reader["cuota"]),
                Plan = Convert.ToInt32(reader["plan"])
            };
        }
    }
}