using MAASoft.HomeBanking.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.DataAccess
{
    public class QueriesSocios : Queries
    {
        #region SELECT
        public Socio QuerySocioPorDNI(int nroDocumento)
        {
            Socio valor = null;

            string cmd = 
                "SELECT codigo,nombre,domici,locali,telefo,fax,celular,nrodoc,socade,cuota,fecing,fecnac,tipodoc,cuit,sitiva,mail " +
                " FROM socios WHERE nrodoc = ?";

            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {
                    cmdOleDb.Parameters.AddWithValue("nNroDoc", nroDocumento);

                    using (var reader = cmdOleDb.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            valor = MapearSocio(reader);
                        }
                        reader.Close();
                    }
                }

                conn.Close();
            }

            return valor;
        }

        // NOTA (ML): el parametro cuit esta definido como "double" para ser reconocido correctamente por el driver OLEDB
        public Socio QuerySocioPorCUIT(double cuit)
        {
            Socio valor = null;

            string cmd =
                "SELECT codigo,nombre,domici,locali,codpostal,telefo,fax,celular,nrodoc,socade,cuota,fecing,fecnac,tipodoc,cuit,sitiva,mail " +
                " FROM socios WHERE cuit = ?";

            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {
                    cmdOleDb.Parameters.AddWithValue("nCUIT", cuit);

                    using (var reader = cmdOleDb.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            valor = MapearSocio(reader);
                        }
                        reader.Close();
                    }
                }

                conn.Close();
            }

            return valor;
        }

        public List<Socio> QuerySocioPorNombre(string nombre)
        {
            List<Socio> valor = new List<Socio>();

            nombre = nombre.ToUpper();

            string cmd = "SELECT codigo,nombre,domici,locali,codpostal,telefo,fax,celular,nrodoc,socade,cuota,fecing,fecnac,tipodoc,cuit,sitiva,mail " +
                " FROM socios WHERE nombre like '%"+ nombre + "%'";

            using (var conn = new OleDbConnection(connetionString))
            {
                conn.Open();

                using (var cmdOleDb = new OleDbCommand(cmd, conn))
                {

                    using (var reader = cmdOleDb.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            valor.Add(MapearSocio(reader));
                        }
                        reader.Close();
                    }
                }

                conn.Close();
            }

            if (valor.Count == 0)
                valor = null;

            return valor;

        }

        #endregion

        #region INSERT
        #endregion

        #region UPDATE

        public RespuestaQuery QueryActualizarSocio(Socio socio)
        {
            try
            {
                string cmd =
                    "UPDATE dbo.socios SET domici = ?, locali = ?, codpostal = ?, telefo = ?, celular = ? " +
                    "WHERE CustomerID = ?";

                using (var conn = new OleDbConnection(connetionString))
                {
                    conn.Open();

                    using (var cmdOleDb = new OleDbCommand(cmd, conn))
                    {
                        cmdOleDb.Parameters.Add("@domici", OleDbType.Decimal).Value = socio.Domicilio;
                        cmdOleDb.Parameters.Add("@locali", OleDbType.Decimal).Value = socio.Localidad;
                        cmdOleDb.Parameters.Add("@codpostal", OleDbType.Decimal).Value = socio.CodPostal;
                        cmdOleDb.Parameters.Add("@telefo", OleDbType.Decimal).Value = socio.Telefono;
                        cmdOleDb.Parameters.Add("@celular", OleDbType.Decimal).Value = socio.Celular;
                        cmdOleDb.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return RespuestaQuery.OK;
            }
            catch
            {
                return RespuestaQuery.Error;
            }
        }

        #endregion

        #region DELETE
        #endregion

        #region MAP

        private Socio MapearSocio(OleDbDataReader reader)
        {
            return new Socio
            {
                Codigo = Convert.ToInt32(reader["codigo"]),
                Nombre = Convert.ToString(reader["nombre"]).Trim(),
                Domicilio = Convert.ToString(reader["domici"]).Trim(),
                Localidad = Convert.ToString(reader["locali"]).Trim(),
                CodPostal = Convert.ToString(reader["codpostal"]).Trim(),
                Telefono = Convert.ToString(reader["telefo"]).Trim(),
                Fax = Convert.ToString(reader["fax"]).Trim(),
                Celular = Convert.ToString(reader["celular"]).Trim(),
                Email = Convert.ToString(reader["mail"]).Trim(),
                Socade = Convert.ToChar(reader["socade"]),
                Cuota = Convert.ToDecimal(reader["cuota"]),
                FechaIngreso = Convert.ToDateTime(reader["fecing"]),
                FechaNacimiento = Convert.ToDateTime(reader["fecnac"]),
                TipoDocumento = Convert.ToString(reader["tipodoc"]).Trim(),
                NroDocumento = Convert.ToInt64(reader["nrodoc"]),
                CUIT = Convert.ToInt64(reader["cuit"]),
                SituacionIva = Convert.ToString(reader["sitiva"]).Trim(),
            };
        }

        #endregion
    }
}