using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Entities;

namespace BugTicketingSystem.BLL.Services
{
    public interface IAttachmentService
    {
        Task DeleteAttachment(Attachment attachment);
    }
}
