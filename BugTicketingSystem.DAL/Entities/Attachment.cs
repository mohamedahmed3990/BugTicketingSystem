using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Entities
{
    public class Attachment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int BugId { get; set; }
        public Bug Bug { get; set; }
    }
}
