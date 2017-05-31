﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobiquitous2016App.Daos
{
    public static class DatabaseConnection
    {
        public static string ConnectionString => GetConnectionString();

        private static string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = "ECOLOGDB2016",
                InitialCatalog = "ECOLOGDBver2",
                IntegratedSecurity = true,
                ConnectTimeout = 180
            };

            return builder.ToString();
        }
    }
}
