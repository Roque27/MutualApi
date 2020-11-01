using MAASoft.HomeBanking.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.DataAccess
{
    public class QueriesCuotasSocietarias : Queries
    {
        public List<CuotaSocietaria> QueryCuotasSocietarias(int nCuenta, DateTime dDesde, DateTime dHasta)
        {
            List<CuotaSocietaria> filas = new List<CuotaSocietaria>();
            string cmd = "SELECT fecha,import,fecha_pago,estado FROM ctasocio " +
                            "WHERE socio= ? AND fecha BETWEEN ? AND ? " +
                            "ORDER BY fecha ";
            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {
                    cmdOleDb.Parameters.AddWithValue("nCuenta", nCuenta);
                    cmdOleDb.Parameters.AddWithValue("dDesde", dDesde);
                    cmdOleDb.Parameters.AddWithValue("dHasta", dHasta);

                    using (var reader = cmdOleDb.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filas.Add(new CuotaSocietaria {
                                Fecha = Convert.ToDateTime(reader["fecha"]),
                                Importe = Convert.ToDecimal(reader["import"]),
                                FechaDePago = Convert.ToDateTime(reader["fecha_pago"]),
                                Estado = Convert.ToString(reader["estado"]).Trim()
                                });
                        }
                        reader.Close();
                    }
                }
                conn.Close();
            }
            return filas;
        }
    }
}