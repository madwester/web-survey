using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Added these
using System.Data; //all sorts of thing, but I'll use it for DataTable and DataRow
using System.Data.SqlClient; //helps connect to db
using System.Configuration; //access to web config


namespace AIT_research
{
    public class DatabaseHelper
    {
        //static method return sql connection
        public static SqlConnection GetConnection()
        {
            try
            {
                //creting a connection string to connect to db
                string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                //creting connection
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionString;
                //opening created connection
                connection.Open();
                //returning connection so we can use it in other functions
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("System couldn't connect to database.");
                return null;
            }
        }
    }
}