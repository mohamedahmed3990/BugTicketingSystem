using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BLL.DTOs;

namespace BugTicketingSystem.BLL.Services.ProjectService
{
    public interface IProjectService
    {
        Task<List<ProjectToReturnDto>> GetAllAsync();

        Task Add(ProjectAddDto projectAddDto);

        Task<ProjectWithBugsDto> GetProjectWithBugs(int id);
    }
}
