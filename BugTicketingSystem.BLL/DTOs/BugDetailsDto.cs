using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BLL.DTOs
{
    public class BugDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public List<AttachmentChild> attachmentChild { get; set; }

        public List<UserChild> userChild { get; set; }
    }
}
