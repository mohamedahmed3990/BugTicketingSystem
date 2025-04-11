using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Repositories.AttachmentRepository;
using BugTicketingSystem.DAL.Repositories.BugRepository;
using BugTicketingSystem.DAL.Repositories.PorjectRepository;

namespace BugTicketingSystem.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IBugRepository BugRepository { get; }
        public IAttachmentRepository AttachmentRepository { get; }
        public IProjectRepository ProjectRepoistory { get; }

        public UnitOfWork(AppDbContext context, IProjectRepository projectRepository, IBugRepository bugRepository, IAttachmentRepository attachmentRepository)
        {
            _context = context;
            ProjectRepoistory = projectRepository;
            BugRepository = bugRepository;
            AttachmentRepository = attachmentRepository;
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
