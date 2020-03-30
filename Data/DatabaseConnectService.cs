using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Utilities.Common.Dependency;
using Utilities.Configuration;

namespace Data
{
    public class DatabaseConnectService : IDisposable
    {
        private static readonly AppSettings AppSettings = SingletonDependency<IOptions<AppSettings>>.Instance.Value;
        public DatabaseConnectService()
        {
            this.Connection = new SqlConnection(AppSettings.ConnectionString);
            ConnectionOpen();
        }
        public IDbConnection Connection { get; set; }
        public bool IsConnected => this.Connection.State == ConnectionState.Open;
        public void ConnectionOpen()
        {
            if (!this.IsConnected)
            {
                this.Connection.Open();
            }
        }
        public void ConnectionClose()
        {
            if (this.IsConnected)
            {
                this.Connection.Close();
            }
        }
        public void Dispose()
        {
            ConnectionClose();
        }
    }
}
