using Dapper.FastCrud;
using Data;
using Data.Entity.Account;
using Data.Entity.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RazorTemplates.ViewModels;
using Services.Dto;
using Services.Interfaces.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace Services.Implementation.Internal
{
    public class ToolService : BaseService, IToolService
    {
        private readonly ILogger<ToolService> _logger;
        public ToolService(DatabaseConnectService databaseConnectService, ILogger<ToolService> logger) : base(databaseConnectService)
        {
            _databaseConnectService = databaseConnectService;
            _logger = logger;
        }
        public async Task<List<ToolDetailsViewModel>> GetAll(PagingDto dto)
        {
            try
            {
                if(dto.PageSize == 0)
                {
                    dto.PageSize = 2;
                }
                if (dto.PageNumber == 0)
                {
                    dto.PageNumber = 1;
                }
                var joinTool = await _databaseConnectService.Connection.FindAsync<Tool>(x => x
                                                                        .Include<Tool_Tag>(join => join.LeftOuterJoin())
                                                                        .Include<Tag>(join => join.LeftOuterJoin())
                                                                        .Include<Users>(join => join.LeftOuterJoin())
                                                                        .Include<Rate>(join => join.LeftOuterJoin())
                );
                var getTool = joinTool.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize)
                                        .Select(x=> ToToolDetailsViewModel(x)).ToList();
                return getTool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<ToolDetailsViewModel>> MyTool(int id)
        {
            try
            {
                var joinTool = await _databaseConnectService.Connection.FindAsync<Tool>(x => x
                                                                        .Include<Tool_Tag>(join => join.LeftOuterJoin())
                                                                        .Include<Tag>(join => join.LeftOuterJoin())
                                                                        .Include<Users>(join => join.LeftOuterJoin())
                                                                        .Include<Rate>(join => join.LeftOuterJoin())
                                                                        .Where($"Tool.CreatedBy = @Id")
                                                                        .WithParameters(new { Id = id })
                );
                var myTool = joinTool.Select(x => new ToolDetailsViewModel()
                {
                    Title = x.Title,
                    Description = x.Description,
                    ViewNumbers = x.ViewNumbers,
                    ViewDownloads = x.ViewDownloads,
                    FullName = x.Users.FullName,
                    Tags = x.Tool_Tags.Select(x => x.Tag.Name).ToList(),
                    RatedNumber = x.Rates == null || x.Rates.Count() == 0 ? 0 : x.Rates.Select(x => x.RatedNumber).ToList().Average()
                }).ToList();
                return myTool;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ToolDetailsViewModel>> Search(SearchFilterDto dto, PagingDto pagingDto)
        {
            try
            {
                if (pagingDto.PageSize == 0)
                {
                    pagingDto.PageSize = 2;
                }
                if (pagingDto.PageNumber == 0)
                {
                    pagingDto.PageNumber = 1;
                }
                    _logger.LogInformation("Search: Title = " + dto.Title + "/Creator = " + dto.FullName + "/Tag = " + dto.Tag + "/Description = " + dto.Description);
                    var result = await _databaseConnectService.Connection.FindAsync<Tool>(x => x
                                                                                .Include<Tool_Tag>(join => join.LeftOuterJoin())
                                                                                .Include<Tag>(join => join.LeftOuterJoin())
                                                                                .Include<Users>(join => join.LeftOuterJoin())
                                                                                .Include<Rate>(join => join.LeftOuterJoin())
                                                                                .Where($"Tool.Title LIKE @title and Users.FullName LIKE @fullName and Tag.Name LIKE @tag and Tool.Description LIKE @desc")
                                                                                .WithParameters(new { title = string.Format("%{0}%", dto.Title), fullName = string.Format("%{0}%", dto.FullName), tag = string.Format("%{0}%", dto.Tag), desc = string.Format("%{0}%", dto.Description) })
                        );
                    var data = result.Skip((pagingDto.PageNumber - 1) * pagingDto.PageSize).Take(pagingDto.PageSize)
                                            .Select(x => ToToolDetailsViewModel(x)).ToList();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private ToolDetailsViewModel ToToolDetailsViewModel(Tool entity)
        {
            return entity == null
                ? null
                : new ToolDetailsViewModel()
                {
                    Title = entity.Title,
                    Description = entity.Description,
                    ViewNumbers = entity.ViewNumbers,
                    ViewDownloads = entity.ViewDownloads,
                    FullName = entity.Users.FullName,
                    Tags = entity.Tool_Tags.Select(x => x.Tag.Name).ToList(),
                    RatedNumber = entity.Rates == null || entity.Rates.Count() == 0 ? 0 : entity.Rates.Select(x => x.RatedNumber).ToList().Average()
                };
        }

    }
}
