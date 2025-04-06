using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BLL.DTOs;
using BugTicketingSystem.DAL.Entities;
using BugTicketingSystem.DAL.Repositories.PorjectRepository;

namespace BugTicketingSystem.BLL.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Add(ProjectAddDto projectAddDto)
        {
            var result = new Project
            {
                Id = projectAddDto.Id,
                Name = projectAddDto.Name,
                Description = projectAddDto.Description,
            };
            await _projectRepository.Add(result);
        }

        public async Task<List<ProjectToReturnDto>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();

            var result = projects.Select(p => new ProjectToReturnDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
             }).ToList();

            return result;
        }

        public async Task<ProjectWithBugsDto?> GetProjectWithBugs(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project is null)
            {
                return null;
            }

            return new ProjectWithBugsDto
            {
                Id = project.Id,
                Name = project.Name,
                Bugs = project.Bugs
                    .Select(issue => new BugChildDto
                    {
                        Id = issue.Id,
                        Name = issue.Name
                    }).ToList()
            };
        }
    }
}
