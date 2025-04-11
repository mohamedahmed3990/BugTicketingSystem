using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL.Repositories.BugRepository
{
    public class BugRepository : IBugRepository
    {
        private readonly AppDbContext _context;

        public BugRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Bug bug)
        {
            _context.Set<Bug>().Add(bug);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Bug>> GetAllAsync()
        {
            var bugs = await _context.Set<Bug>().AsNoTracking().ToListAsync();
            return bugs;
        }

        public async Task<Bug?> GetByIdAsync(int id)
        {
            return await _context.Set<Bug>().Include(b => b.Project).Include(b => b.Assignees).Include(b => b.Attachments)
           .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Bug?> GetWithUserAsync(int id)
        {
            return await _context.Set<Bug>().Include(b => b.Project).Include(b => b.Assignees)
           .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Bug?> GetBugWithAttachment(int id)
        {
            var bug = await _context.Set<Bug>().Include(b => b.Attachments).FirstOrDefaultAsync(b => b.Id == id);
            if (bug is null) return null;
            return bug;
        }


    }
}
