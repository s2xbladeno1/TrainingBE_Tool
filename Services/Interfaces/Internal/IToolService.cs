using Data.Entity.Main;
using RazorTemplates.ViewModels;
using Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace Services.Interfaces.Internal
{
    public interface IToolService
    {
        Task<List<ToolDetailsViewModel>> GetAll(PagingDto dto);
        Task<List<ToolDetailsViewModel>> Search(SearchFilterDto dto, PagingDto pagingDto);
    }
}
