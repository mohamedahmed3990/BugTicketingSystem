using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Entities;

namespace BugTicketingSystem.DAL.Repositories.AttachmentRepository
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly AppDbContext _context;

        public AttachmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Delete(Attachment attachment)
        {
            _context.Set<Attachment>().Remove(attachment);
        }
    }
}
