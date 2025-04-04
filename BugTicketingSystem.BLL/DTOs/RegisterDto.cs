using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BLL.DTOs
{
    public record RegisterDto(string userName, string email, string password);
}
