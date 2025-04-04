using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Entities
{
    public class Bug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public ICollection<AppUser> Assignees { get; set; } = new HashSet<AppUser>();
        public ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();

    }
}
