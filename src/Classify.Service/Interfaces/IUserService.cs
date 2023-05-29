using Classify.Domain.Configurations;
using Classify.Domain.Entities;
using Classify.Domain.Enums;
using Classify.Service.DTOs.Users;
using System.Linq.Expressions;

namespace Classify.Service.Interfaces
{
    public interface IUserService
    {
        Task<bool> RemoveAsync(long id);
        Task<UserResultDto> RetrieveByIdAsync(long id);
        Task<UserResultDto> AddAsync(UserCreationDto dto);
        Task<User> RetrieveByEmailAsync(string email);
        Task<UserResultDto> RetrieveByPhoneNumberAsync(string phoneNumber);
        Task<UserResultDto> ModifyAsync(UserUpdateDto dto);
        Task<UserResultDto> ChangePasswordAsync(UserChangePasswordDto dto);
        Task<IEnumerable<UserResultDto>> RetrieveAllAsync(PaginationParams @params);
        Task<IEnumerable<UserResultDto>> RetrieveAllByRoleAsync(PaginationParams @params, Role role = Role.RegianAdmin);
        Task<UserResultDto> ChangeRoleAsync(UserChangeRoleDto dto); 
        Task<User> SelectAsync(Expression<Func<User, bool>> expression);
    }
}
