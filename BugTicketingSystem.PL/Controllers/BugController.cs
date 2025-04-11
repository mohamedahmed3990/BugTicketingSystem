using BugTicketingSystem.BLL.DTOs;
using BugTicketingSystem.BLL.Services;
using BugTicketingSystem.BLL.Services.BugService;
using BugTicketingSystem.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        private readonly IBugService _bugService;
        private readonly IAttachmentService _attachmentService;

        public BugController(IBugService bugService, IAttachmentService attachmentService)
        {
            _bugService = bugService;
            _attachmentService = attachmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BugToReturnDto>>> GetAll()
        {
            var bugs = await _bugService.GetAllAsync();
            return Ok(bugs);
        }


        [HttpPost]
        public async Task<ActionResult> Add(BugAddDto bugAddDto)
        {
            await _bugService.Add(bugAddDto);
            return Ok("Added Successfully:)");
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<BugDetailsDto>>> GetBugDetails(int id)
        {
            var bugs = await _bugService.GetBugDetails(id);
            if (bugs == null) { return NotFound(); }
            return Ok(bugs);
        }


        [HttpPost("{id}/assignees")]
        public async Task<IActionResult> AssignUserToBug(int id, [FromBody] UserToBugDto dto)
        {
            try
            {
                await _bugService.AddUserToBug(id, dto);
                return Ok("User assigned to bug successfully:)");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("bug/{id}/assignees/{userId}")]
        public async Task<IActionResult> DeleteUserToBug(int id, string userId)
        {
            try
            {
                await _bugService.DeleteUserFromBug(id, userId);
                return Ok("User deleted to bug successfully:)");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/attachments")]
        public async Task<ActionResult> UploadFile(int id, [FromForm] FileUploadDto dto)
        {
            var bug = await _bugService.GetBug(id);
            if(bug == null) { return NotFound("Bug Not Exist!!"); }

            var extention = Path.GetExtension(dto.File.FileName);
            var allowedExtentions = new string[]
            {
                ".png",
                ".jpg",
                ".svg"
            };

            if (!allowedExtentions.Contains(extention, StringComparer.InvariantCultureIgnoreCase))
            {
                return BadRequest("extention not valid!!!");
            }

            if (!(dto.File.Length is > 0 and < 5*1024*1024))
            {
                return BadRequest("size is not allowed!!!");
            }

            var randomImageName = $"{Guid.NewGuid()}{extention}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "myImages", randomImageName);


            using var stream = new FileStream(filePath, FileMode.Create);
            await dto.File.CopyToAsync(stream);

            var url = $"{Request.Scheme}://{Request.Host}/api/static-files/{Path.GetFileName(filePath)}";

            var attachment = new Attachment
            {
                Name = randomImageName,
                Image = url,
                BugId = bug.Id,
            };

            await _bugService.AddAttachmentToBug(attachment, bug.Id);

            return Ok("attachment added successfully:)");


        }

        [HttpDelete("{id}/attachments/{attachmentId}")]
        public async Task<ActionResult> DeleteAttachmentFromBugAsync(int id, int attachmentId)
        {
            var bug = await _bugService.GetBugWithAttachments(id);

            if (bug == null)
                return NotFound("bug not found!");

            var attachment = bug.Attachments.FirstOrDefault(a => a.Id == attachmentId);
            if (attachment == null)
                return NotFound("attachment not found!");

            // Delete physical file from wwwroot/myImages
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "myImages", Path.GetFileName(attachment.Image));

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            bug.Attachments.Remove(attachment);
            await _attachmentService.DeleteAttachment(attachment);

            return Ok("attachment deleted successfully:)");
        }



        [HttpGet("{id}/attachments")]
        public async Task<ActionResult<AttachmentsWithBugDto>> GetBugWithAttachments(int id)
        {
            var bug = await _bugService.BugWithAttachments(id);
            if (bug is null) return NotFound("bug not found");

            return Ok(bug);
        }


    }
}
