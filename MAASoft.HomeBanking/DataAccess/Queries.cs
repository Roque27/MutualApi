using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MAASoft.HomeBanking.DataAccess
{
    public class Queries
    {
        private readonly DateTime DATE_TIME_VACIO = new DateTime(1899, 12, 30);

        public string connetionString;

        public Queries()
        {
            this.connetionString = ConfigurationManager.ConnectionStrings["HomeBanking"].ConnectionString;
        }

        public DateTime? MapearDateTimeNullable(object valor)
        {
            DateTime? dateTime = valor as DateTime?;
            return dateTime == null || DateTime.Equals(dateTime.Value, DATE_TIME_VACIO)
                ? (DateTime?)null
                : dateTime.Value;
        }

        public enum RespuestaQuery
        {
            OK,
            Error
        }
    }
}