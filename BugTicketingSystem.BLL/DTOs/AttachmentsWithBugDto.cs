using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BLL.DTOs
{
    public class AttachmentsWithBugDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AttachmentChild> Attachments { get; set; }
    }
}
