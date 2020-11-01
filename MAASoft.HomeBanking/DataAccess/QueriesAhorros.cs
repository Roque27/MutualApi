using MAASoft.HomeBanking.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.DataAccess
{
    public class QueriesAhorros : Queries
    {
        private const int COD_MOV_SALDO_ANTERIOR = 1;

        public SaldoCajaAhorro QueryObtenerSaldoCajaDeAhorro(int cuenta, string tipo)
        {
            SaldoCajaAhorro valor = null;

            string cmd = "SELECT tipo,cuenta,saldo FROM sdoca01 WHERE cuenta= ? AND tipo= ? ";
            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {
                    cmdOleDb.Parameters.AddWithValue("nCuenta", cuenta);
                    cmdOleDb.Parameters.AddWithValue("cTipo", tipo);

                    using (var reader = cmdOleDb.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            valor = new SaldoCajaAhorro
                            {
                                Tipo = Convert.ToString(reader["tipo"]).Trim(),
                                Cuenta = Convert.ToInt64(reader["cuenta"]),
                                Saldo = Convert.ToDecimal(reader["saldo"])
                            };
                        }
                        reader.Close();
                    }
                }

                conn.Close();
            }

            return valor;
        }

        public List<ResumenCuenta> QueryResumenDeCuentasDeSocios(int cuenta, string comprobante, DateTime desde, DateTime hasta)
        {
            List<ResumenCuenta> filas = new List<ResumenCuenta>();
            string cmd = "SELECT acumamvc.compro, acumamvc.fecha, acumamvc.numero, acumamvc.cuenta, acumamvc.codmov, Amvcpto.nombre, acumamvc.movimi*Amvcpto.multiplica AS importe, acumamvc.movimi*0 AS saldo, acumamvc.codimp, acumamvc.observa, acumamvc.operador " +
                            "FROM acumamvc, amvcpto " +
                            "WHERE acumamvc.codmov = Amvcpto.codigo AND acumamvc.cuenta = ? AND acumamvc.fecha BETWEEN ? AND ? AND compro = ? " +
                            "union all " +
                            "SELECT Movca01.compro, Movca01.fecha, Movca01.numero, Movca01.cuenta, Movca01.codmov, Amvcpto.nombre, Movca01.movimi* Amvcpto.multiplica AS importe, acumamvc.movimi * 0 AS saldo, Movca01.codimp, Movca01.observa, movca01.operador " +
                            "FROM movca01, amvcpto " +
                            "WHERE Movca01.codmov = Amvcpto.codigo AND Movca01.cuenta = ? AND Movca01.fecha BETWEEN ? AND ? AND compro = ? " +
                            "ORDER BY 4, 2";

            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                decimal saldo = 0;
                if (desde.Day != 1)
                {
                    saldo = CalcularSaldoMensualDeCuentaHastaFecha(cuenta, comprobante, desde, conn);
                }

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {
                    cmdOleDb.Parameters.AddWithValue("nCuenta1", cuenta);
                    cmdOleDb.Parameters.AddWithValue("dDesde1", desde);
                    cmdOleDb.Parameters.AddWithValue("dHasta1", hasta);
                    cmdOleDb.Parameters.AddWithValue("cCompro1", comprobante);
                    cmdOleDb.Parameters.AddWithValue("nCuenta2", cuenta);
                    cmdOleDb.Parameters.AddWithValue("dDesde2", desde);
                    cmdOleDb.Parameters.AddWithValue("dHasta2", hasta);
                    cmdOleDb.Parameters.AddWithValue("cCompro2", comprobante);

                    using (var reader = cmdOleDb.ExecuteReader())
                    {
                        int codMov = 0;
                        decimal importe = 0;

                        while (reader.Read())
                        {
                            codMov = Convert.ToInt32(reader["codmov"]);
                            importe = Convert.ToDecimal(reader["importe"]);

                            if (codMov == COD_MOV_SALDO_ANTERIOR)
                            {
                                saldo = importe;
                            }
                            else
                            {
                                saldo += importe;
                            }

                            filas.Add(new ResumenCuenta
                            {
                                Comprobante = Convert.ToString(reader["compro"]).Trim(),
                                Fecha = Convert.ToDateTime(reader["fecha"]),
                                Numero = Convert.ToInt64(reader["numero"]),
                                Cuenta = Convert.ToInt64(reader["cuenta"]),
                                CodMovimiento = codMov,
                                Nombre = Convert.ToString(reader["nombre"]).Trim(),
                                Importe = importe,
                                Saldo = saldo,
                                CodImp = Convert.ToInt32(reader["codimp"]),
                                Observaciones = Convert.ToString(reader["observa"]).Trim(),
                                Operador = Convert.ToString(reader["operador"]).Trim()
                            });
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
            return filas;
        }

        private decimal CalcularSaldoMensualDeCuentaHastaFecha(int cuenta, string comprobante, DateTime fecha, OleDbConnection conn)
        {
            string sql =
                "SELECT SUM(acumamvc.movimi*Amvcpto.multiplica)" +
                "FROM acumamvc, amvcpto " +
                "WHERE acumamvc.codmov = Amvcpto.codigo AND acumamvc.cuenta = ? AND acumamvc.fecha >= ? AND acumamvc.fecha < ? AND compro = ?";

            DateTime desde = new DateTime(fecha.Year, fecha.Month, 1);
            DateTime hasta = fecha.Date;

            using (var cmdSaldo = new OleDbCommand(sql, conn))
            {
                cmdSaldo.Parameters.AddWithValue("nCuenta", cuenta);
                cmdSaldo.Parameters.AddWithValue("dFechaDesde", desde);
                cmdSaldo.Parameters.AddWithValue("dFechaHasta", hasta);
                cmdSaldo.Parameters.AddWithValue("cTipo", comprobante);

                return Convert.ToDecimal(cmdSaldo.ExecuteScalar());
            }
        }

        public List<AhorroTerminoVigente> QueryObtenerAhorrosATerminosVigentes(int cuenta, DateTime desde, DateTime hasta, string moneda)
        {
            List<AhorroTerminoVigente> filas = new List<AhorroTerminoVigente>();
            string cmd = "SELECT fecha,fecvto,numero,plazo,tem,tna,deposi,estimu,sello,total " +
                            "FROM movpf01 WHERE cuenta = ? AND fecha BETWEEN ? AND ? AND moneda = ? " +
                            "ORDER BY fecha ";

            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {
                    cmdOleDb.Parameters.AddWithValue("nCuenta", cuenta);
                    cmdOleDb.Parameters.AddWithValue("dDesde", desde);
                    cmdOleDb.Parameters.AddWithValue("dHasta", hasta);
                    cmdOleDb.Parameters.AddWithValue("cMoneda", moneda);

                    using (var reader = cmdOleDb.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filas.Add(new AhorroTerminoVigente
                            {
                                Fecha = Convert.ToDateTime(reader["fecha"]),
                                FechaVto = Convert.ToDateTime(reader["fecvto"]),
                                Numero = Convert.ToInt64(reader["numero"]),
                                Plazo = Convert.ToInt32(reader["plazo"]),
                                TEM = Convert.ToDecimal(reader["tem"]),
                                TNA = Convert.ToDecimal(reader["tna"]),
                                Deposito = Convert.ToDecimal(reader["deposi"]),
                                Estimu = Convert.ToDecimal(reader["estimu"]),
                                Sello = Convert.ToDecimal(reader["sello"]),
                                Total = Convert.ToDecimal(reader["total"])
                            });
                        }
                        reader.Close();
                    }
                }
                conn.Close();
            }
            return filas;
        }

        public List<DetalleValorCobroAcreditacion> QueryDetalleDeValoresAlCobroDeAcreditacion(int nCuenta)
        {
            List<DetalleValorCobroAcreditacion> filas = new List<DetalleValorCobroAcreditacion>();
            string cmd = "SELECT tipo,fecdep,fecacr,banco,locali,cheque,import " +
                            "FROM movac01 WHERE cuenta= ? AND ayuda = 0 ORDER BY ayuda,fecdep ";

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
                            filas.Add(new DetalleValorCobroAcreditacion
                            {
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