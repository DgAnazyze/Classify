using AutoMapper;
using Classify.DataAccess.Interfaces;
using Classify.Domain.Configurations;
using Classify.Domain.Entities;
using Classify.Domain.Enums;
using Classify.Service.Commons.Exceptions;
using Classify.Service.Commons.Helper.Security;
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
            var user = await this.repository.SelectAsync((u) => u.PhoneNumber.ToLower() == dto.PhoneNumber.ToLower()
            && u.Email.ToLower() == dto.Email.ToLower());

            if (user is not null)
                throw new CustomerException(403, "User is already exists");

            var mapped = this.mapper.Map<User>(dto);
            mapped.CreatedAt = DateTime.UtcNow;
            mapped.PasswordHash = PasswordHasher.Hash(dto.Password);
            var result = await this.repository.InserAsync(mapped);
            return this.mapper.Map<UserForResultDto>(result);
        }

        public async Task<UserForResultDto> ChangePasswordAsync(UserForChangePasswordDto dto)
        {
            var user = await this.repository.SelectAsync(u => u.PhoneNumber == dto.PhoneNumber);
            if (user is null || user.IsDeleted)
                throw new CustomerException(404, "User not found");

            if (!PasswordHasher.Verify(dto.OldPassword, user.PasswordHash))
                throw new CustomerException(400, "Password is incorrect");

            if (dto.NewPassword != dto.OldPassword)
                throw new CustomerException(400, "New password and confir password aren't equal");

            user.PasswordHash = PasswordHasher.Hash(dto.NewPassword);

            await this.repository.SavaAsync();

            return this.mapper.Map<UserForResultDto>(user);
           }

        public async Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
        {
            var user = await this.repository.SelectAsync(u => u.Id == id);
            if (user is null || user.IsDeleted) throw new CustomerException(404, "User not found");

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
