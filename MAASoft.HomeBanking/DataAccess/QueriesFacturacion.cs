using MAASoft.HomeBanking.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.DataAccess
{
    public class QueriesFacturacion : Queries
    {
        public List<FacturaInternet> QueryObtenerFacturasDelServicioDeInternet(int nCuenta, DateTime dDesde, DateTime dHasta)
        {
            List<FacturaInternet> filas = new List<FacturaInternet>();
            string cmd = "SELECT fecha,compro,letra,numero,periodo,vencimiento,importe,pago,fecha_Pago,interes " +
                            "FROM facturas WHERE socio = ? AND fecha between ? AND ? " +
                            "ORDER BY numero ";

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
                            filas.Add(new FacturaInternet {
                                Fecha = Convert.ToDateTime(reader["fecha"]),
                                Compro = Convert.ToString(reader["compro"]).Trim(),
                                Letra = Convert.ToString(reader["letra"]).Trim(),
                                Numero = Convert.ToInt64(reader["numero"]),
                                Periodo = Convert.ToString(reader["periodo"]).Trim(),
                                Vencimiento = Convert.ToDateTime(reader["vencimiento"]),
                                Importe = Convert.ToDecimal(reader["importe"]),
                                Pago = Convert.ToDecimal(reader["pago"]),
                                FechaPago = Convert.ToDateTime(reader["fecha_Pago"]),
                                Interes = Convert.ToDecimal(reader["interes"])});
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