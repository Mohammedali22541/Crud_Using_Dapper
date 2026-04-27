using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EmployeeManagementSystem.DatabaseConnection
{
    public class DbConnection
    {
        private readonly string _connectionString ="Server=.;Database=EmployeeManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public IDbConnection StartConnection()
        {
            return new SqlConnection(_connectionString);
        }



    }
}
