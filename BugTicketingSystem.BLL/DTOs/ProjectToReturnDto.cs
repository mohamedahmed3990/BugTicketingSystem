using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Entities;

namespace BugTicketingSystem.BLL.DTOs
{
    public record ProjectToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
