using MAASoft.HomeBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace MAASoft.HomeBanking.DataAccess
{
    public class QueriesTransferencias : Queries
    {
        #region SELECT
        #endregion

        #region INSERT
        #endregion

        #region UPDATE

        public RespuestaQuery QueryInsertarMovimientoEntreCuentas(int cuentaorigen, int cuentadestino, string tipo, decimal monto)
        {
            string cmd =
                "BEGIN TRANSACTION " +
                "INSERT INTO movca01 (fecha,tipo,cuenta,codmov,movimi,coddep,codimp,compro,numero,observa,terminal,usuario,hs,transcaja,operador) " +
                "SET fecha = ?, tipo = ?, cuenta = ?, codmov = ?, movimi = ?, coddep = ?, codimp = ?, compro = ?, numero = ?, observa = ?, terminal = ?, usuario = ?, hs = ?, transcaja = ?, operador = ? " +
                "WHERE codigo = ? " +
                "INSERT INTO movca01 (fecha,tipo,cuenta,codmov,movimi,coddep,codimp,compro,numero,observa,terminal,usuario,hs,transcaja,operador) " +
                "SET fecha = ?, tipo = ?, cuenta = ?, codmov = ?, movimi = ?, coddep = ?, codimp = ?, compro = ?, numero = ?, observa = ?, terminal = ?, usuario = ?, hs = ?, transcaja = ?, operador = ? " +
                "WHERE codigo = ? " +
                "END TRANSACION ";

            // Insercion cuenta Destino
            try
            {
                using (var conn = new OleDbConnection(connetionString))
                {
                    conn.Open();
                    using (var cmdOleDb = new OleDbCommand(crudCmd, conn))
                    {
                        cmdOleDb.CommandType = CommandType.Text;
                        cmdOleDb.ExecuteNonQuery();
                    }

                    using (var cmdOleDb = new OleDbCommand(cmd, conn))
                    {
                        cmdOleDb.Parameters.Add("@fecha", OleDbType.Date).Value = DateTime.Now.Date;
                        cmdOleDb.Parameters.Add("@tipo", OleDbType.VarChar).Value = tipo;
                        cmdOleDb.Parameters.Add("@cuenta", OleDbType.Integer).Value = cuentadestino;
                        cmdOleDb.Parameters.Add("@codmov", OleDbType.Integer).Value = 61;
                        cmdOleDb.Parameters.Add("@movimi", OleDbType.Decimal).Value = monto;
                        cmdOleDb.Parameters.Add("@coddep", OleDbType.Integer).Value = null;
                        cmdOleDb.Parameters.Add("@codimp", OleDbType.Integer).Value = null;
                        cmdOleDb.Parameters.Add("@compro", OleDbType.VarChar).Value = tipo.Equals("C") ? "AMVC" : (tipo.Equals("D") ? "AMVD" : "AMVE");
                        cmdOleDb.Parameters.Add("@numero", OleDbType.Integer).Value = null;
                        cmdOleDb.Parameters.Add("@observa", OleDbType.VarChar).Value = "TRANSFERENCIA REALIZADA WEB SOCIO " + cuentaorigen + " A SOCIO " + cuentadestino;
                        cmdOleDb.Parameters.Add("@terminal", OleDbType.Integer).Value = "PCWEB";
                        cmdOleDb.Parameters.Add("@usuario", OleDbType.Integer).Value = "Web";
                        cmdOleDb.Parameters.Add("@hs", OleDbType.Date).Value = DateTime.Now;
                        cmdOleDb.Parameters.Add("@transcaja", OleDbType.Integer).Value = null;
                        cmdOleDb.Parameters.Add("@operador", OleDbType.Integer).Value = null;
                        cmdOleDb.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return RespuestaQuery.OK;
            }
            catch (Exception e)
            {
                return RespuestaQuery.Error;
            }
        }

        #endregion

    }
}