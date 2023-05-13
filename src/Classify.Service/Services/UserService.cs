using AutoMapper;
using Classify.DataAccess.Interfaces;
using Classify.Domain.Configurations;
using Classify.Domain.Entities;
using Classify.Domain.Enums;
using Classify.Service.DTOs.Users;
using Classify.Service.Interfaces;

namespace Classify.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;
        private readonly IMapper mapper;

        public UserService(IRepository<User> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<UserForResultDto> AddAsync(UserCreationDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UserForResultDto> ChangePasswordAsync(UserForChangePasswordDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserForResultDto>> RetrieveAllByRoleAsync(PaginationParams @params, Role role = Role.RegianAdmin)
        {
            throw new NotImplementedException();
        }

        public Task<User> RetrieveByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserForResultDto> RetrieveByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
