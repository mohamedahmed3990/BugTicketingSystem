using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BLL.DTOs
{
    public class ProjectWithBugsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<BugChildDto> Bugs { get; set; } = [];
    }
}
