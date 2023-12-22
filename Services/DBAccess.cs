using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.ViewModels;
using Dapper;

namespace BookBuddy.Services
{
    public class DBAccess :  IDBAccess
    {
        private readonly string _connectionString = "Server=10.56.8.36;Database=DB_F23_32;User Id=DB_F23_USER_32;Password=OPENDB_32;";

        public async Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters)
        {
            using IDbConnection conn = new SqlConnection(_connectionString);
          

            return await conn.QueryAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure);
        }
             
        public async Task SaveData<T>(string sql, T parameters)
        {           
            using IDbConnection conn = new SqlConnection(_connectionString);

            await conn.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
        }

       
    }
}
