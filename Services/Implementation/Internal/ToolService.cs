using Dapper.FastCrud;
using Data;
using Data.Entity.Account;
using Data.Entity.Main;
using RazorTemplates.ViewModels;
using Services.Dto;
using Services.Interfaces.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation.Internal
{
    public class ToolService : BaseService, IToolService
    {
        public ToolService(DatabaseConnectService databaseConnectService) : base(databaseConnectService)
        {
            _databaseConnectService = databaseConnectService;
        }
        public async Task<List<ToolDetailsViewModel>> GetAll()
        {
            try
            {
                var joinTool = await _databaseConnectService.Connection.FindAsync<Tool>(x => x
                                                                    .Include<Tool_Tag>(join => join.LeftOuterJoin())
                                                                    .Include<Tag>(join => join.LeftOuterJoin())
                                                                    .Include<Users>(join => join.LeftOuterJoin())
                );
                var selectTool = joinTool.Select(x=> new ToolDetailsViewModel() 
                { 
                    Title = x.Title,
                    Description = x.Description,
                    FullName = x.Users.FullName,
                    ViewNumbers = x.ViewNumbers,
                    ViewDownloads = x.ViewDownloads,
                    Tags = x.Tool_Tags.Select(x=>x.Tag.Name).ToList()
                });
                return selectTool.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
