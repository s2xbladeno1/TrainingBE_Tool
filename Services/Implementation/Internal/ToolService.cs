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
using Utilities.Dtos;

namespace Services.Implementation.Internal
{
    public class ToolService : BaseService, IToolService
    {
        public ToolService(DatabaseConnectService databaseConnectService) : base(databaseConnectService)
        {
            _databaseConnectService = databaseConnectService;
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
                                        .Select(x=> new ToolDetailsViewModel() 
                                            { 
                                                Title = x.Title,
                                                Description = x.Description,
                                                ViewNumbers = x.ViewNumbers,
                                                ViewDownloads = x.ViewDownloads,
                                                FullName = x.Users.FullName,
                                                Tags = x.Tool_Tags.Select(x => x.Tag.Name).ToList(),
                                                RatedNumber = x.Rates.Select(x => x.RatedNumber).ToList()
                                            }).ToList();
                return getTool;
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
                var result = await _databaseConnectService.Connection.FindAsync<Tool>(x => x
                                                                            .Include<Tool_Tag>(join => join.LeftOuterJoin())
                                                                            .Include<Tag>(join => join.LeftOuterJoin())
                                                                            .Include<Users>(join => join.LeftOuterJoin())
                                                                            .Include<Rate>(join => join.LeftOuterJoin())
                                                                            .Where($"Tool.Title LIKE @title and Users.FullName LIKE @fullName and Tag.Name LIKE @tag and Tool.Description LIKE @desc")
                                                                            .WithParameters(new { title = dto.Title, fullName = dto.FullName, tag = dto.Tag, desc = dto.Description })
                    );
                var data = result.Skip((pagingDto.PageNumber - 1) * pagingDto.PageSize).Take(pagingDto.PageSize)
                                        .Select(x => new ToolDetailsViewModel()
                                        {
                                            Title = x.Title,
                                            Description = x.Description,
                                            ViewNumbers = x.ViewNumbers,
                                            ViewDownloads = x.ViewDownloads,
                                            FullName = x.Users.FullName,
                                            Tags = x.Tool_Tags.Select(x => x.Tag.Name).ToList(),
                                            RatedNumber = x.Rates.Select(x => x.RatedNumber).ToList()
                                        }).ToList();
            return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
