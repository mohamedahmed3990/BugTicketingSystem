using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Entities;

namespace BugTicketingSystem.DAL.Repositories.BugRepository
{
    public interface IBugRepository
    {
        Task<List<Bug>> GetAllAsync();

        Task Add(Bug bug);

        Task<Bug?> GetByIdAsync(int id);

        Task<Bug?> GetWithUserAsync(int id);

        Task<Bug?> GetBugWithAttachment(int id);

    }
}
