using Data;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Implementation
{
    public class BaseService : IBaseService
    {
        public DatabaseConnectService _databaseConnectService { get; set; }
        public BaseService(DatabaseConnectService databaseConnectService)
        {
            this._databaseConnectService = databaseConnectService;
        }


    }
}
