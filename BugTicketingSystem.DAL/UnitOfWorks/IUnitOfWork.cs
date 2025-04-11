using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Repositories.AttachmentRepository;
using BugTicketingSystem.DAL.Repositories.BugRepository;
using BugTicketingSystem.DAL.Repositories.PorjectRepository;

namespace BugTicketingSystem.DAL.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public IBugRepository BugRepository { get;}
        public IProjectRepository ProjectRepoistory { get;}
        public IAttachmentRepository AttachmentRepository { get;}

        Task<int> SaveChangesAsync();
    }
}
