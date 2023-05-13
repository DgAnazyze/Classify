using Classify.Domain.Configurations;
using Classify.Domain.Entities;
using Classify.Domain.Enums;
using Classify.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classify.Service.Interfaces
{
    public interface IUserService
    {
        Task<bool> RemoveAsync(long id);
        Task<User> RetrieveByEmailAsync(string email);
        Task<UserForResultDto> RetrieveByIdAsync(long id);
        Task<UserForResultDto> AddAsync(UserCreationDto dto);
        Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto);
        Task<UserForResultDto> ChangePasswordAsync(UserForChangePasswordDto dto);
        Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
        Task<IEnumerable<UserForResultDto>> RetrieveAllByRoleAsync(PaginationParams @params, Role role = Role.RegianAdmin);

    }
}
