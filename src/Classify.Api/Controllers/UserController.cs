using Classify.Domain.Configurations;
using Classify.Domain.Entities;
using Classify.Domain.Enums;
using Classify.Service.DTOs.Users;
using Classify.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Classify.Api.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    protected readonly ILogger<User> logger;
    protected readonly IUserService userService;

    public UserController(ILogger<User> logger, IUserService userService)
    {
        this.logger = logger;
        this.userService = userService;
    }

    [HttpPost("add"), Authorize("SuperAdmin")]
    public async Task<IActionResult> AddUserAsync(UserCreationDto dto) =>
        Ok(await this.userService.AddAsync(dto));

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteAsync(int id) =>
        Ok(await this.userService.RemoveAsync(id));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UserUpdateDto dto) =>
      Ok(await this.userService.ModifyAsync(dto));

    [HttpPatch("change role")]
    public async Task<IActionResult> ChangeRoleAsync(UserChangeRoleDto dto) =>
        Ok(await this.userService.ChangeRoleAsync(dto));

    [HttpPatch("change password")]
    public async Task<IActionResult> ChangePasswordAsync(UserChangePasswordDto dto) =>
        Ok(await this.userService.ChangePasswordAsync(dto));

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdAsync(int id) =>
        Ok(await this.userService.RetrieveByIdAsync(id));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params) =>
        Ok(await this.userService.RetrieveAllAsync(@params));

    [HttpGet("email")]
    public async Task<IActionResult> GetByEmailAsync([EmailAddress] string email) =>
        Ok(await this.userService.RetrieveByEmailAsync(email));

    [HttpGet("Number")]
    public async Task<IActionResult> GetByPhoneNumberAsync([Phone] string phone) =>
        Ok(await this.userService.RetrieveByPhoneNumberAsync(phone));

    [HttpGet("role")]
    public async Task<IActionResult> GetByRoleAsync([FromQuery] PaginationParams @params, Role role) =>
        Ok(await this.userService.RetrieveAllByRoleAsync(@params, role));    
}
