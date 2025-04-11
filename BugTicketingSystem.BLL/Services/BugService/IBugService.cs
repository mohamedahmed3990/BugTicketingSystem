using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BLL.DTOs;
using BugTicketingSystem.DAL.Entities;

namespace BugTicketingSystem.BLL.Services.BugService
{
    public interface IBugService
    {
        Task<List<BugToReturnDto>> GetAllAsync();

        Task Add(BugAddDto bugAddDto);

        Task<BugDetailsDto?> GetBugDetails(int id);

        Task AddUserToBug(int id, UserToBugDto userdto);

        Task DeleteUserFromBug(int bugId, string UserId);

        Task<Bug?> GetBug(int id);

        Task AddAttachmentToBug(Attachment attachment, int id);

        Task<Bug?> GetBugWithAttachments(int id);

        Task<AttachmentsWithBugDto> BugWithAttachments(int id);


    }
}
