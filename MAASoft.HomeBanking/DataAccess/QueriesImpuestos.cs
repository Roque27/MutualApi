using MAASoft.HomeBanking.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.DataAccess
{
    public class QueriesImpuestos : Queries
    {
        public List<ImpuestoPendiente> QueryObtenerImpuestosPendientes(int nCuenta)
        {
            List<ImpuestoPendiente> filas = new List<ImpuestoPendiente>();
            string cmd = "SELECT fecvto,nrobol,maepi02.nombre AS Concepto,import " +
                            "FROM gesps01, maepi02 " +
                            "WHERE gesps01.concep = maepi02.codigo AND cuenta = ? " +
                            "ORDER BY fecvto ";

            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {
                    cmdOleDb.Parameters.AddWithValue("nCuenta", nCuenta);

                    using (var reader = cmdOleDb.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filas.Add(new ImpuestoPendiente {
                                FechaVto = Convert.ToDateTime(reader["fecvto"]),
                                NroBol = Convert.ToString(reader["nrobol"]).Trim(),
                                Nombre = Convert.ToString(reader["Concepto"]).Trim(),
                                Importe = Convert.ToDecimal(reader["import"])});
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