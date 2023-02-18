// UsersController.cs
// Authors: Araf Al-Jami
// Created: 23-06-2021 3:54 PM
// Updated: 23-06-2021 6:25 PM

using System.Threading.Tasks;
using RestaurantsApi.Dtos;
using RestaurantsApi.ViewModels;

namespace RestaurantsApi.WebService.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using RestaurantsApi.Services.Abstractions;

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> logger;
        private readonly IUserService userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        [HttpGet("{id}", Name = "GetUser")]
        [Authorize]
        public async Task<UserViewModel> Get(Guid id)
        {
            return await this.userService.GetUserAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            ServiceResponse<UserViewModel> response = await this.userService.CreateUserAsync(createUserDto);
            if (response.Error != null)
            {
                return BadRequest(response.Error);
            }

            return CreatedAtRoute("GetUser", new {response.Data.Id}, response.Data);
        }

        [HttpGet("", Name = "GetAllUser")]
        [Authorize]
        public async Task<PageResponse<UserViewModel>> Get([FromQuery] GetUsersQueryDto getUsersQueryDto)
        {
            return await this.userService.GetUsersAsync(getUsersQueryDto.PageSize, getUsersQueryDto.PageNumber);
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        [Authorize]
        public async Task DeleteUser(Guid id)
        {
            await this.userService.DeleteUserAsync(id);
            this.Ok();
        }

        [HttpPut("", Name = "EditUser")]
        [Authorize]
        public async Task DeleteUser([FromBody] EditUserDto editUserDto)
        {
            await this.userService.EditUserAsync(editUserDto);
            this.Ok();
        }
    }
}
