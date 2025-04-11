using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Entities;

namespace BugTicketingSystem.DAL.Repositories.AttachmentRepository
{
    public interface IAttachmentRepository
    {
        void Delete(Attachment attachment);
    }
}
