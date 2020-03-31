using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RazorTemplates.ViewModels;
using Services.Dto;
using Services.Interfaces.Internal;
using Utilities.Dtos;

namespace WebAPI.Controllers
{
    public class ToolController : Controller
    {
        private readonly IToolService _toolService;
        private readonly ISessionService _sessionService;
        public ToolController(IToolService toolService, ISessionService sessionService)
        {
            _toolService = toolService;
            _sessionService = sessionService;
        }
        [HttpGet]
        [Route("api/home/getall")]
        public async Task<List<ToolDetailsViewModel>>  GetAll(PagingDto dto)
        {
            var result = await _toolService.GetAll(dto);
            return result;
        }

        [HttpGet]
        [Route("api/home/search")]
        public async Task<List<ToolDetailsViewModel>> Search(SearchFilterDto dto, PagingDto pagingDto)
        {
            var result = await _toolService.Search(dto, pagingDto);
            return result;
        }
        [HttpGet]
        [Route("api/tool/mytool")]
        public async Task<List<ToolDetailsViewModel>> MyTool()
        {
            var result = await _toolService.MyTool(_sessionService.UserId);
            return result;
        }
    }
}