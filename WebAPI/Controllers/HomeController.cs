using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity.Main;
using Microsoft.AspNetCore.Mvc;
using RazorTemplates.ViewModels;
using Services.Interfaces.Internal;

namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToolService _toolService;
        public HomeController(IToolService toolService)
        {
            _toolService = toolService;
        }
        [HttpGet]
        [Route("api/home/getall")]
        public async Task<List<ToolDetailsViewModel>>  GetAll()
        {
            var result = await _toolService.GetAll();
            return result;
        }
    }
}