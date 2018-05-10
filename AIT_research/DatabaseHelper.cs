using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Added these
using System.Data; //all sorts of thing, but I'll use it for DataTable and DataRow
using System.Data.SqlClient; //helps connect to db
using System.Configuration; //acces ti web config


namespace AIT_research
{
    public class DatabaseHelper
    {
        //static method return sql connection
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            return connection;
        }
    }
}