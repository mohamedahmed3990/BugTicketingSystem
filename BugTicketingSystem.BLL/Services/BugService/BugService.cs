using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BLL.DTOs;
using BugTicketingSystem.DAL.Entities;
using BugTicketingSystem.DAL.Repositories.BugRepository;
using BugTicketingSystem.DAL.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.BLL.Services.BugService
{
    public class BugService : IBugService
    {
        private readonly IBugRepository _bugRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public BugService(IBugRepository bugRepository, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _bugRepository = bugRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(BugAddDto bugAddDto)
        {
            var bug = new Bug
            {
                Name = bugAddDto.Name,
                Description = bugAddDto.Description,
                ProjectId = bugAddDto.ProjectId,
            };

            await _bugRepository.Add(bug);


        }

        public async Task<List<BugToReturnDto>> GetAllAsync()
        {
            var bugs = await _bugRepository.GetAllAsync();

            var bugsDto = bugs.Select(b => new BugToReturnDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
            }).ToList();

            return bugsDto;
        }

        public async Task<BugDetailsDto?> GetBugDetails(int id)
        {
            var bug = await _bugRepository.GetByIdAsync(id);

            if (bug == null) { return null; }
            var bugDetailsDto = new BugDetailsDto
            {
                Id = bug.Id,
                Name = bug.Name,
                Description = bug.Description,
                ProjectId = bug.ProjectId,
                ProjectName = bug.Project.Name,
                attachmentChild = bug.Attachments.Select(a => new AttachmentChild
                {
                    Id = a.Id,
                    Name = a.Name,
                    Image = a.Image
                }).ToList(),
                userChild = bug.Assignees.Select(a => new UserChild
                {
                    Id = a.Id,
                    UserName = a.UserName,
                    Email = a.Email
                }).ToList()
            };

            return bugDetailsDto;
        }

        public async Task AddUserToBug(int id,UserToBugDto userdto)
        {
            var bug = await _bugRepository.GetWithUserAsync(id);

            if(bug is null){ throw new Exception("Not Found"); }

            var user = await _userManager.FindByEmailAsync(userdto.Email);

            if (user == null) { throw new Exception("Not Fount"); };

            if(bug.Assignees.Any(u => u.Email == userdto.Email)) 
            {
                throw new Exception("User is already assigned to this bug");
            }

            bug.Assignees.Add(user);
            await _unitOfWork.SaveChangesAsync();
            

        }

        public async Task DeleteUserFromBug(int bugId, string UserId)
        {
            var bug = await _bugRepository.GetWithUserAsync(bugId);

            if (bug is null) { throw new Exception("Not Found"); }

            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null) { throw new Exception("Not Fount"); };

            if (!bug.Assignees.Any(u => u.Email == user.Email))
            {
                throw new Exception("User is not assigned to this bug");
            }

            bug.Assignees.Remove(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Bug?> GetBug(int id)
        {
            var bug = await _unitOfWork.BugRepository.GetByIdAsync(id);
            if (bug is null) { return null; }

            return bug;
        }

        public async Task AddAttachmentToBug(Attachment attachment, int id)
        {

            var bug = await _unitOfWork.BugRepository.GetByIdAsync(id);
            if (bug is null) { throw new Exception("bug not found"); }
            bug.Attachments.Add(attachment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Bug?> GetBugWithAttachments(int id)
        {
            var bug = await _unitOfWork.BugRepository.GetBugWithAttachment(id);
            if (bug is null) { return null; }
            return bug;
        }

        public async Task<AttachmentsWithBugDto> BugWithAttachments(int id)
        {
            var bug = await _unitOfWork.BugRepository.GetBugWithAttachment(id);

            if (bug is null) return null;

            var result = new AttachmentsWithBugDto
            {
                Id = bug.Id,
                Name = bug.Name,
                Attachments = bug.Attachments.Select(a => new AttachmentChild
                {
                    Id = a.Id,
                    Name = a.Name,
                    Image = a.Image,
                }).ToList(),
            };

            return result;
        }
    }
}
