using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BLL.DTOs
{
    public record UserDto(string userName, string email,string phoneNumber, string token);

}
