using Data.Entity.Main;
using RazorTemplates.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Internal
{
    public interface IToolService
    {
        Task<List<ToolDetailsViewModel>> GetAll();
    }
}
