// ReviewsController.cs
// Authors: Araf Al-Jami
// Created: 23-06-2021 3:54 PM
// Updated: 23-06-2021 6:25 PM

namespace RestaurantsApi.WebService.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using RestaurantsApi.Common;
    using RestaurantsApi.Dtos;
    using RestaurantsApi.Services.Abstractions;
    using RestaurantsApi.ViewModels;

    [ApiController]
    [Route("[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly ILogger<ReviewsController> logger;
        private readonly IReviewService reviewService;

        public ReviewsController(ILogger<ReviewsController> logger, IReviewService reviewService)
        {
            this.logger = logger;
            this.reviewService = reviewService;
        }

        [HttpGet("", Name = "GetAllReviews")]
        [Authorize]
        public Task<PageResponse<ReviewViewModel>> GetAllReviews([FromQuery] GetReviewsQueryDto queryDto)
        {
            return this.reviewService.GetReviewsAsync(queryDto.PageSize, queryDto.PageNumber);
        }

        [HttpGet("restaurants/{id}/highest", Name = "GetHighest")]
        [Authorize]
        public Task<ReviewViewModel> GetHighest(Guid id)
        {
            return this.reviewService.GetHighestRatedReviewAsync(id);
        }

        [HttpGet("restaurants/{id}/lowest", Name = "GetLowest")]
        [Authorize]
        public Task<ReviewViewModel> GetLowest(Guid id)
        {
            return this.reviewService.GetLowestRatedReviewAsync(id);
        }

        [HttpGet("restaurants/{id}/latest", Name = "GetLatest")]
        [Authorize]
        public Task<ReviewViewModel> GetLatest(Guid id)
        {
            return this.reviewService.GetLatestReviewAsync(id);
        }

        [HttpGet("{id}", Name = "GetReview")]
        [Authorize]
        public Task<ReviewViewModel> Get(Guid id)
        {
            return this.reviewService.GetReviewAsync(id);
        }

        [HttpGet("restaurants/{restaurantId}/user/{userId}", Name = "GetUsersReview")]
        [Authorize]
        public Task<ReviewViewModel> GetUsersReview(Guid restaurantId, Guid userId)
        {
            return this.reviewService.GetReviewByRestaurantAndUserIdAsync(restaurantId, userId);
        }

        [HttpPost]
        [Authorize(Roles = Constants.UserRole)]
        public async Task<IActionResult> Create([FromBody]CreateReviewDto createReviewDto)
        {
            ReviewViewModel reviewViewModel = await this.reviewService.CreateReviewAsync(createReviewDto);
            return CreatedAtRoute("GetReview", new {reviewViewModel.Id}, reviewViewModel);
        }

        [HttpPost("reply", Name = "ReplyReview")]
        [Authorize(Roles = Constants.RestaurantOwnerRole + "," + Constants.AdminRole)]
        public async Task<IActionResult> ReplyReview([FromBody] ReplyReviewDto replyReviewDto)
        {
            ReviewViewModel reviewViewModel = await this.reviewService.ReplyReviewAsync(replyReviewDto);
            return CreatedAtRoute("GetReview", new {reviewViewModel.Id}, reviewViewModel);
        }

        [HttpDelete("{id}", Name = "DeleteReview")]
        [Authorize]
        public Task<bool> DeleteReview(Guid id)
        {
            return this.reviewService.DeleteReviewAsync(id);
        }

        [HttpPut]
        [Authorize(Roles = Constants.UserRole + "," + Constants.AdminRole)]
        public async Task<IActionResult> Edit([FromBody]EditReviewDto editReviewDto)
        {
            await this.reviewService.EditReviewAsync(editReviewDto);
            return Ok();
        }
    }
}
