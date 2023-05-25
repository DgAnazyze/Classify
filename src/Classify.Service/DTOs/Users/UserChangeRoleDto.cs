using Classify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classify.Service.DTOs.Users;

public class UserChangeRoleDto
{
    public int id { get; set; }
    public Role role { get; set; }
}
