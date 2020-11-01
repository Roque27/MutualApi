using MAASoft.HomeBanking.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.DataAccess
{
    public class QueriesAyudasEconomicas : Queries
    {
        public List<AyudaEconomicaVigente> QueryObtenerAyudasEconomicasVigentes(int nSocio, DateTime dDesde, DateTime dHasta)
        {
            List<AyudaEconomicaVigente> filas = new List<AyudaEconomicaVigente>();
            string cmd = "SELECT fecha, tipo, comprobantes.nombre AS Comprobantes, ayuda, plazo, fecvto, total, cuotas " +
                            "FROM movae01, comprobantes " +
                            "WHERE movae01.compro = comprobantes.idCompro AND socio = ? AND fecha BETWEEN ? AND ? " +
                            "ORDER BY fecha ";

            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {
                    cmdOleDb.Parameters.AddWithValue("nCuenta", nSocio);
                    cmdOleDb.Parameters.AddWithValue("dDesde", dDesde);
                    cmdOleDb.Parameters.AddWithValue("dHasta", dHasta);

                    using (var reader = cmdOleDb.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filas.Add(new AyudaEconomicaVigente {
                                Fecha = Convert.ToDateTime(reader["fecha"]),
                                Tipo = Convert.ToString(reader["tipo"]).Trim(),
                                Comprobantes = Convert.ToString(reader["Comprobantes"]).Trim(),
                                Ayuda = Convert.ToInt64(reader["ayuda"]),
                                Plazo = Convert.ToInt32(reader["plazo"]),
                                FechaVto = Convert.ToDateTime(reader["fecvto"]),
                                Total = Convert.ToDecimal(reader["total"]),
                                Cuotas = Convert.ToInt32(reader["cuotas"])}
                                );
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
            return filas;
        }

        public List<DetalleCuotaAyuda> QueryDetalleDeCuotas(int nAyuda)
        {
            List<DetalleCuotaAyuda> filas = new List<DetalleCuotaAyuda>();
            string cmd = "SELECT ayuda,moneda,fecvto,nrocta,valcta FROM cuotas WHERE ayuda= ? ORDER BY fecvto ";

            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {
                    cmdOleDb.Parameters.AddWithValue("nAyuda", nAyuda);

                    using (var reader = cmdOleDb.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filas.Add(new DetalleCuotaAyuda {
                                Ayuda = Convert.ToInt64(reader["ayuda"]),
                                Moneda = Convert.ToString(reader["moneda"]).Trim(),
                                FechaVto = Convert.ToDateTime(reader["fecvto"]),
                                NroCuota = Convert.ToInt64(reader["nrocta"]),
                                ValorCuota = Convert.ToDecimal(reader["valcta"])
                                });
                        }
                        reader.Close();
                    }
                }
                conn.Close();
            }
            return filas;
        }

        public List<DetalleChequeAyuda> QueryDetalleDeCheques(int nAyuda)
        {
            List<DetalleChequeAyuda> filas = new List<DetalleChequeAyuda>();
            string cmd = "SELECT ayuda,tipo,fecacr,banco,locali,cheque,import FROM movac01 WHERE ayuda = ? ORDER BY fecacr  ";

            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {
                    cmdOleDb.Parameters.AddWithValue("nAyuda", nAyuda);

                    using (var reader = cmdOleDb.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filas.Add(new DetalleChequeAyuda {
                                Ayuda = Convert.ToInt64(reader["ayuda"]),
                                Tipo = Convert.ToString(reader["tipo"]).Trim(),
                                FechaAcr = Convert.ToDateTime(reader["fecacr"]),
                                Banco = Convert.ToString(reader["banco"]).Trim(),
                                Localidad = Convert.ToString(reader["locali"]).Trim(),
                                Cheque = Convert.ToInt64(reader["cheque"]),
                                Importe = Convert.ToDecimal(reader["import"])
                                });
                        }
                        reader.Close();
                    }
                }
                conn.Close();
            }
            return filas;
        }

        public List<DetalleDocumentoAyuda> QueryDetalleDeDocumentos(int nAyuda)
        {
            List<DetalleDocumentoAyuda> filas = new List<DetalleDocumentoAyuda>();
            string cmd = "SELECT ayuda,tipo,fecacr,banco,import FROM movac01 WHERE ayuda= ? ORDER BY fecacr  ";

            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {
                    cmdOleDb.Parameters.AddWithValue("nAyuda", nAyuda);

                    using (var reader = cmdOleDb.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filas.Add(new DetalleDocumentoAyuda {
                                Ayuda = Convert.ToInt64(reader["ayuda"]),
                                Tipo = Convert.ToString(reader["tipo"]).Trim(),
                                FechaAcr = Convert.ToDateTime(reader["fecacr"]),
                                Banco = Convert.ToString(reader["banco"]).Trim(),
                                Importe = Convert.ToDecimal(reader["import"])
                                });
                        }
                        reader.Close();
                    }
                }
                conn.Close();
            }
            return filas;
        }
        
        public List<DetalleValorNegociadoAyuda> QueryDetalleDeValoresNegociados(int nCuenta)
        {
            List<DetalleValorNegociadoAyuda> filas = new List<DetalleValorNegociadoAyuda>();
            string cmd = "SELECT ayuda,tipo,fecdep,fecacr,banco,locali,cheque,import " +
                            "FROM movac01 WHERE cuenta= ? AND tipo = ? ORDER BY ayuda,fecdep  ";

            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {
                    cmdOleDb.Parameters.AddWithValue("nCuenta", nCuenta);
                    cmdOleDb.Parameters.AddWithValue("cTipo", "A");

                    using (var reader = cmdOleDb.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filas.Add(new DetalleValorNegociadoAyuda {
                                Ayuda = Convert.ToInt64(reader["ayuda"]),
                                Tipo = Convert.ToString(reader["tipo"]).Trim(),
                                FecDep = Convert.ToDateTime(reader["fecdep"]),
                                FechaAcr = Convert.ToDateTime(reader["fecacr"]),
                                Banco = Convert.ToString(reader["banco"]).Trim(),
                                Localidad = Convert.ToString(reader["locali"]).Trim(),
                                Cheque = Convert.ToInt64(reader["cheque"]),
                                Importe = Convert.ToDecimal(reader["import"])
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