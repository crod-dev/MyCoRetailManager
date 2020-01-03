using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 13 Add library  and library class for UI to use to access the database
namespace MRMDataManager.Library.Internal.DataAccess
{
    // 13 internal - can't be seen or used outside the Class Library
    internal class SqlDataAccess
    {
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        
        // 13 Installed Dapper nuget package; Dapper is like a micro ORM (Object Relational Mapper)
        // Generic T specifies the list type to return
        // Generic U allows for flexiblity of parameters to be passed to Query
        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            // 13 Use System.data for IDbConnection
            // 13 Use System.data.SqlClient for SqlConnection
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                // 13 Use dapper for Query; generic T specifies the type of model that each returning row should be
                List<T> rows = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }
        //13
        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            // 13 Use System.data for IDbConnection
            // 13 Use System.data.SqlClient for SqlConnection
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                // 13 Use dapper for Execute
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
