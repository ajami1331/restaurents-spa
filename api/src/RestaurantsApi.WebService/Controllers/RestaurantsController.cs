// RestaurantsController.cs
// Authors: Araf Al-Jami
// Created: 23-06-2021 3:55 PM
// Updated: 23-06-2021 6:25 PM

namespace RestaurantsApi.WebService.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using RestaurantsApi.Common;
    using RestaurantsApi.Dtos;
    using RestaurantsApi.Services.Abstractions;
    using RestaurantsApi.ViewModels;

    [ApiController]
    [Route("[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly ILogger<RestaurantsController> logger;
        private readonly IRestaurantService restaurantService;

        public RestaurantsController(ILogger<RestaurantsController> logger, IRestaurantService restaurantService)
        {
            this.logger = logger;
            this.restaurantService = restaurantService;
        }

        [HttpGet(Name = "GetAll")]
        [Authorize]
        public Task<PageResponse<RestaurantViewModel>> GetAll([FromQuery] GetRestaurantsQueryDto queryDto)
        {
            return this.restaurantService.GetAllRestaurantsAsync(queryDto.PageSize, queryDto.PageNumber, queryDto.LowerBound, queryDto.UpperBound);
        }

        [HttpGet("{id}", Name = "GetRestaurant")]
        [Authorize]
        public Task<RestaurantViewModel> GetRestaurant(Guid id)
        {
            return this.restaurantService.GetRestaurantAsync(id);
        }

        [HttpDelete("{id}", Name = "DeleteRestaurant")]
        [Authorize(Roles = Constants.AdminRole)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.restaurantService.DeleteRestaurantAsync(id);
            return Ok();
        }

        [HttpGet("{id}/name", Name = "GetRestaurantName")]
        [Authorize]
        public Task<string> GetRestaurantName(Guid id)
        {
            return this.restaurantService.GetRestaurantNameAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = Constants.RestaurantOwnerRole)]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            RestaurantViewModel restaurantViewModel = await this.restaurantService.CreateRestaurantAsync(createRestaurantDto);
            return CreatedAtRoute("GetRestaurant", new {restaurantViewModel.Id}, restaurantViewModel);
        }

        [HttpPut]
        [Authorize(Roles = Constants.RestaurantOwnerRole + "," + Constants.AdminRole)]
        public async Task<IActionResult> Edit([FromBody] EditRestaurantDto editRestaurantDto)
        {
            await this.restaurantService.EditRestaurantAsync(editRestaurantDto);
            return Ok();
        }
    }
}
