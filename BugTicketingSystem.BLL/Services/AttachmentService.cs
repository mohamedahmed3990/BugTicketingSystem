using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Entities;
using BugTicketingSystem.DAL.UnitOfWorks;

namespace BugTicketingSystem.BLL.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttachmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task DeleteAttachment(Attachment attachment)
        {
             _unitOfWork.AttachmentRepository.Delete(attachment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
