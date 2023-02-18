// ReviewService.cs
// Authors: Araf Al-Jami
// Created: 27-06-2021 5:48 PM
// Updated: 27-06-2021 5:48 PM

namespace RestaurantsApi.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using RestaurantsApi.Common;
    using RestaurantsApi.Dtos;
    using RestaurantsApi.IdentityServer;
    using RestaurantsApi.Models;
    using RestaurantsApi.Repositories.Contracts;
    using RestaurantsApi.Services.Abstractions;
    using RestaurantsApi.ViewModels;

    public class ReviewService : IReviewService
    {
        private readonly ILogger<ReviewService> logger;
        private readonly IRepository repository;
        private readonly IUserContextProvider userContextProvider;
        private readonly IMapper mapper;

        public ReviewService(ILogger<ReviewService> logger, IRepository repository, IUserContextProvider userContextProvider, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.userContextProvider = userContextProvider;
            this.mapper = mapper;
        }

        public async Task<ReviewViewModel> GetHighestRatedReviewAsync(Guid restaurantId)
        {
            IQueryable<Review> reviews =
                await this.repository.GetItemsAsync<Review>(r => r.RestaurantId.Equals(restaurantId));
            Review review = reviews.OrderBy(r => r.Rating).FirstOrDefault(r => r.RestaurantId.Equals(restaurantId));
            return this.mapper.Map<ReviewViewModel>(review);
        }

        public async Task<ReviewViewModel> GetLowestRatedReviewAsync(Guid restaurantId)
        {
            IQueryable<Review> reviews =
                await this.repository.GetItemsAsync<Review>(r => r.RestaurantId.Equals(restaurantId));
            Review review = reviews.OrderByDescending(r => r.Rating).FirstOrDefault(r => r.RestaurantId.Equals(restaurantId));
            return this.mapper.Map<ReviewViewModel>(review);
        }

        public async Task<ReviewViewModel> GetLatestReviewAsync(Guid restaurantId)
        {
            IQueryable<Review> reviews =
                await this.repository.GetItemsAsync<Review>(r => r.RestaurantId.Equals(restaurantId));
            Review review = reviews.OrderByDescending(r => r.CreatedAt).FirstOrDefault(r => r.RestaurantId.Equals(restaurantId));
            return this.mapper.Map<ReviewViewModel>(review);
        }

        public async Task<ReviewViewModel> CreateReviewAsync(CreateReviewDto dto)
        {
            UserContext userContext = this.userContextProvider.GetUserContext();
            Review review = new Review()
            {
                Id = Guid.NewGuid(),
                Rating = dto.Rating,
                Comment = dto.Comment,
                DateOfVisit = dto.DateOfVisit,
                RestaurantId = dto.RestaurantId,
                CreatedAt = DateTime.Now,
                CreatedBy = userContext.Id,
                ReviewerName = userContext.Name,
                IdsAllowedToRead = new[] { userContext.Id },
                IdsAllowedToWrite = new[] { userContext.Id },
                RolesAllowedToRead = new[] { Constants.UserRole, Constants.AdminRole, Constants.RestaurantOwnerRole + "_" + dto.RestaurantId },
                RolesAllowedToWrite = new[] { Constants.UserRole, Constants.AdminRole, Constants.RestaurantOwnerRole + "_" + dto.RestaurantId },
            };
            await this.repository.SaveAsync(review);
            Restaurant restaurant = await this.repository.GetItemAsync<Restaurant>(r => r.Id.Equals(review.RestaurantId));
            restaurant.RatingSum += review.Rating;
            restaurant.ReviewCount++;
            restaurant.AverageRating = (restaurant.RatingSum * 1.0) / restaurant.ReviewCount;
            await this.repository.UpdateAsync(r => r.Id.Equals(review.RestaurantId), restaurant);
            return this.mapper.Map<ReviewViewModel>(review);
        }

        public async Task<ReviewViewModel> GetReviewAsync(Guid id)
        {
            Review review = await this.repository.GetItemAsync<Review>(r => r.Id.Equals(id));
            return this.mapper.Map<ReviewViewModel>(review);
        }

        public async Task<ReviewViewModel> GetReviewByRestaurantAndUserIdAsync(Guid restaurantId, Guid userId)
        {
            Review review = await this.repository.GetItemAsync<Review>(r => r.RestaurantId.Equals(restaurantId) && r.CreatedBy.Equals(userId));
            return this.mapper.Map<ReviewViewModel>(review);
        }

        public async Task<PageResponse<ReviewViewModel>> GetReviewsAsync(int pageSize, int pageNumber)
        {
            UserContext userContext = this.userContextProvider.GetUserContext();
            IQueryable<Review> reviews = (await this.repository.GetItemsAsync<Review>())
                .Where(r =>
                            (r.RolesAllowedToRead.Any(rr => userContext.Roles.Contains(rr))
                             ||
                             r.IdsAllowedToRead.Contains(userContext.Id))
                );
            int skip = pageSize * pageNumber;
            IEnumerable<ReviewViewModel> result = reviews
                .Skip(skip)
                .Take(pageSize)
                .ToArray()
                .Select(review => this.mapper.Map<ReviewViewModel>(review));
            int totalCount = reviews.Count();
            return new PageResponse<ReviewViewModel>()
            {
                Data = result,
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalCount = totalCount,
                PageCount = (totalCount / pageSize) + (totalCount % pageSize == 0 ? 0 : 1),
            };
        }

        public async Task<ReviewViewModel> ReplyReviewAsync(ReplyReviewDto replyReviewDto)
        {
            UserContext userContext = this.userContextProvider.GetUserContext();
            Review review = await this.repository.GetItemAsync<Review>(r => r.Id.Equals(replyReviewDto.Id));
            review.Reply = replyReviewDto.Reply;
            review.UpdatedBy = userContext.Id;
            review.UpdatedAt = DateTime.Now;
            review.RolesAllowedToRead = new[] { Constants.UserRole, Constants.AdminRole };
            review.RolesAllowedToWrite = new[] { Constants.UserRole, Constants.AdminRole };
            await this.repository.UpdateAsync(r => r.Id.Equals(replyReviewDto.Id), review);
            return this.mapper.Map<ReviewViewModel>(review);
        }

        public async Task<bool> DeleteReviewAsync(Guid id)
        {
            Review review = await this.repository.GetItemAsync<Review>(r => r.Id.Equals(id));
            if (review == null)
            {
                return false;
            }

            Restaurant restaurant = await this.repository.GetItemAsync<Restaurant>(r => r.Id.Equals(review.RestaurantId));
            restaurant.RatingSum -= review.Rating;
            restaurant.ReviewCount--;
            restaurant.ReviewCount = Math.Max(0, restaurant.ReviewCount);
            restaurant.RatingSum = Math.Max(0, restaurant.RatingSum);
            if (restaurant.ReviewCount <= 0)
            {
                restaurant.AverageRating = 0;
            }
            else
            {
                restaurant.AverageRating = (restaurant.RatingSum * 1.0) / restaurant.ReviewCount;
            }

            await this.repository.UpdateAsync(r => r.Id.Equals(review.RestaurantId), restaurant);
            await this.repository.DeleteAsync<Review>(r => r.Id.Equals(id));
            return true;
        }

        public async Task EditReviewAsync(EditReviewDto dto)
        {
            Review oldReview = await this.repository.GetItemAsync<Review>(r => r.Id.Equals(dto.Id));
            Restaurant restaurant = await this.repository.GetItemAsync<Restaurant>(r => r.Id.Equals(oldReview.RestaurantId));
            restaurant.RatingSum -= oldReview.Rating;
            restaurant.ReviewCount--;
            restaurant.ReviewCount = Math.Max(0, restaurant.ReviewCount);
            restaurant.RatingSum = Math.Max(0, restaurant.RatingSum);
            Review newReview = new Review()
            {
                Id = oldReview.Id,
                Rating = dto.Rating,
                Comment = dto.Comment,
                DateOfVisit = dto.DateOfVisit,
                RestaurantId = oldReview.RestaurantId,
                CreatedAt = DateTime.Now,
                CreatedBy = oldReview.CreatedBy,
                ReviewerName = oldReview.ReviewerName,
                IdsAllowedToRead = oldReview.IdsAllowedToRead,
                IdsAllowedToWrite = oldReview.IdsAllowedToWrite,
                RolesAllowedToRead = oldReview.RolesAllowedToRead,
                RolesAllowedToWrite = oldReview.RolesAllowedToWrite,
            };
            await this.repository.UpdateAsync(r => r.Id.Equals(newReview.Id), newReview);
            restaurant.RatingSum += newReview.Rating;
            restaurant.ReviewCount++;
            restaurant.AverageRating = (restaurant.RatingSum * 1.0) / restaurant.ReviewCount;
            await this.repository.UpdateAsync(r => r.Id.Equals(newReview.RestaurantId), restaurant);
        }
    }
}
