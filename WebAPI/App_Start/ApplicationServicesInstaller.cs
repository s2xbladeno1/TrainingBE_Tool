using AutoMapper;
using Data;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.AutoMapper;
using Services.Implementation.Account;
using Services.Implementation.Internal;
using Services.Interfaces.Account;
using Services.Interfaces.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.App_Start
{
    public class ApplicationServicesInstaller
    {
        public static void ConfigureApplicationServices(IServiceCollection services, IConfiguration configuration)
        {

            Mapper.Initialize(cfg => cfg.AddProfile<DtoMappingProfile>());
            services.AddTransient<DatabaseConnectService>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IToolService, ToolService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
