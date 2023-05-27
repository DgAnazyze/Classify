using AutoMapper;
using Classify.DataAccess.Interfaces;
using Classify.Domain.Configurations;
using Classify.Domain.Entities;
using Classify.Domain.Enums;
using Classify.Service.Commons.Exceptions;
using Classify.Service.Commons.Extensions;
using Classify.Service.Commons.Helper.Security;
using Classify.Service.DTOs.Students;
using Classify.Service.DTOs.Users;
using Classify.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        /// <summary>
        /// Ton insert new user 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="CustomerException"></exception>
        public async Task<UserResultDto> AddAsync(UserCreationDto dto)
        {
            var user = await this.repository.SelectAsync((u) => u.PhoneNumber.ToLower() == dto.PhoneNumber.ToLower()
            && u.Email.ToLower() == dto.Email.ToLower());

            if (user is not null)
                throw new CustomerException(403, "User is already exists");

            var mapped = this.mapper.Map<User>(dto);
            mapped.CreatedAt = DateTime.UtcNow;
            mapped.PasswordHash = PasswordHasher.Hash(dto.Password);
            var result = await this.repository.InserAsync(mapped);
            return this.mapper.Map<UserResultDto>(result);
        }
        /// <summary>
        /// To change password
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="CustomerException"></exception>
        public async Task<UserResultDto> ChangePasswordAsync(UserChangePasswordDto dto)
        {
            var user = await this.repository.SelectAsync(u => u.PhoneNumber == dto.PhoneNumber);
            if (user is null || user.IsDeleted)
                throw new CustomerException(404, "User not found");

            if (!PasswordHasher.Verify(dto.OldPassword, user.PasswordHash))
                throw new CustomerException(400, "Password is incorrect");

            if (dto.NewPassword != dto.ComfirmPassword)
                throw new CustomerException(400, "New password and confir password aren't equal");

            user.PasswordHash = PasswordHasher.Hash(dto.NewPassword);

            await this.repository.SavaAsync();

            return this.mapper.Map<UserResultDto>(user);
           }
        /// <summary>
        /// To update
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="CustomerException"></exception>
        public async Task<UserResultDto> ModifyAsync(UserUpdateDto dto)
        {
            var user = await this.repository.SelectAsync(u => u.Id == dto.Id);
            if (user is null || user.IsDeleted) throw new CustomerException(404, "User not found");

            user.FirstName = String.IsNullOrEmpty(dto.FirstName) ? user.FirstName : dto.FirstName;
            user.LastName = String.IsNullOrEmpty(dto.LastName) ? user.LastName : dto.LastName;
            user.PhoneNumber = String.IsNullOrEmpty(dto.PhoneNumber) ? user.PhoneNumber : dto.PhoneNumber;
            user.Email = String.IsNullOrEmpty(dto.Email) ? user.Email : dto.Email;
            user.Address = String.IsNullOrEmpty(dto.Address) ? user.Address : dto.Address;
            user.School = String.IsNullOrEmpty(dto.School) ? user.School : dto.School;

            user.UpdatedAt = DateTime.UtcNow;
            await this.repository.SavaAsync();

            return this.mapper.Map<UserResultDto>(user);
        }

        /// <summary>
        /// To delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="CustomerException"></exception>
        public async Task<bool> RemoveAsync(long id)
        {
            var user = await this.repository.SelectAsync(u => u.Id == id);
            if (user is null || user.IsDeleted) throw new CustomerException(404, "User not found");

            await this.repository.DeleteAsync(u => u.Id == id);
            await this.repository.SavaAsync();
            return true;
        }
        /// <summary>
        /// Selects users by given role
        /// </summary>
        /// <param name="params"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserResultDto>> RetrieveAllByRoleAsync(PaginationParams @params, Role role = Role.RegianAdmin)
        {
            var users = await this.repository.SelectAll()
                .Where(u => u.Role == role && !u.IsDeleted)
                .ToPagedList(@params)
                .ToListAsync();

            return this.mapper.Map<IEnumerable<UserResultDto>>(users);
        }

        /// <summary>
        /// Returns All user with pagination
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        /// <exception cref="CustomerException"></exception>
        public async Task<IEnumerable<UserResultDto>> RetrieveAllAsync(PaginationParams @params)
        {
            var users = this.repository.SelectAll().
                 Where(u => u.IsDeleted == false)
                 .ToPagedList(@params)
                 .ToListAsync();

            if (users is null)
                throw new CustomerException(404, "Users aren't found");

            return this.mapper.Map<IEnumerable<UserResultDto>>(users);
        }

        /// <summary>
        /// Select user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="CustomerException"></exception>
        public async Task<UserResultDto> RetrieveByEmailAsync(string email)
        {
            var user = await this.repository.SelectAsync(user => user.Email.ToLower() == email.ToLower());
            if (user is null)
                throw new CustomerException(404, "Couldn't find user by given email");

            return this.mapper.Map<UserResultDto>(user);
        }
       /// <summary>
       /// Selects User by Phone number
       /// </summary>
       /// <param name="email"></param>
       /// <returns></returns>
       /// <exception cref="CustomerException"></exception>
        public async Task<UserResultDto> RetrieveByPhoneNumberAsync(string phoneNumber)
        {
            var user = await this.repository.SelectAsync(user => user.PhoneNumber.ToLower() == phoneNumber.ToLower());
            if (user is null)
                throw new CustomerException(404, "Couldn't find user by given Phone Number");

            return this.mapper.Map<UserResultDto>(user);
        }
        /// <summary>
        /// Selects by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="CustomerException"></exception>
        public async Task<UserResultDto> RetrieveByIdAsync(long id)
        {
            var user = await this.repository.SelectAsync(u => u.Id == id);
            if (user is null || user.IsDeleted)
                throw new CustomerException(404, "User Not Found");

            return this.mapper.Map<UserResultDto>(user);
        }
        /// <summary>
        /// Selects a user from db by followinf expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="CustomerException"></exception>
        public async Task<User> SelectAsync(Expression<Func<User,bool>> expression)
        {
            var user = await this.repository.SelectAsync(expression);
            if (user is null || user.IsDeleted) 
                throw new CustomerException(404, "User not found");

            return user;
        }

        public async Task<UserResultDto> ChangeRoleAsync(UserChangeRoleDto dto)
        {
            var user = await this.repository.SelectAsync(u => u.Id == dto.id);
            if (user is null)
                throw new CustomerException(404, "User not found");

            user.Role = dto.role;
            return mapper.Map<UserResultDto>(user);
        }
    }
}
