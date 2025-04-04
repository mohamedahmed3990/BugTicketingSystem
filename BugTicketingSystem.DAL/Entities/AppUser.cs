using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BugTicketingSystem.DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public ICollection<Bug> Bugs { get; set; } = new HashSet<Bug>();
    }
}
