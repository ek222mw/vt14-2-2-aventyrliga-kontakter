using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AventyrligaKontakter.Data
{
    public abstract class DALBase
    {
        

        private static string _connectionstring;

        static DALBase() {

            _connectionstring = WebConfigurationManager.ConnectionStrings["1dv406_AdventureWorksAssignmentConnectionString"].ConnectionString;
          
        }

       

       protected SqlConnection CreateConnection() {

            return new SqlConnection(_connectionstring);

        
        }




    }
}