using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Entities;

namespace BugTicketingSystem.DAL.Repositories.PorjectRepository
{
    public interface IProjectRepository
    {

        Task<List<Project>> GetAllAsync();

        Task Add(Project project);

        Task<Project?> GetByIdAsync(int id);

    }
}
