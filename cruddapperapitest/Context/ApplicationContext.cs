﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace cruddapperapitest.Context
{
    public class ApplicationContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ApplicationContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ApplicationContextConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
