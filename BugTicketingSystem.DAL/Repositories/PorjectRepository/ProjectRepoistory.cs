using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL.Repositories.PorjectRepository
{
    public class ProjectRepoistory : IProjectRepository
    {
        private readonly AppDbContext _dbContext;

        public ProjectRepoistory(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Project>> GetAllAsync()
        {
            var projects = await _dbContext.Set<Project>().ToListAsync();
            return projects;
        }
    }
}
