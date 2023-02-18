// IReviewService.cs
// Authors: Araf Al-Jami
// Created: 27-06-2021 5:48 PM
// Updated: 27-06-2021 5:48 PM

namespace RestaurantsApi.Services.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using RestaurantsApi.Dtos;
    using RestaurantsApi.ViewModels;

    public interface IReviewService
    {
        Task<ReviewViewModel> GetHighestRatedReviewAsync(Guid restaurantId);

        Task<ReviewViewModel> GetLowestRatedReviewAsync(Guid restaurantId);

        Task<ReviewViewModel> GetLatestReviewAsync(Guid restaurantId);

        Task<ReviewViewModel> CreateReviewAsync(CreateReviewDto createReviewDto);

        Task<ReviewViewModel> GetReviewAsync(Guid id);

        Task<ReviewViewModel> GetReviewByRestaurantAndUserIdAsync(Guid restaurantId, Guid userId);

        Task<PageResponse<ReviewViewModel>> GetReviewsAsync(int pageSize, int pageNumber);

        Task<ReviewViewModel> ReplyReviewAsync(ReplyReviewDto replyReviewDto);

        Task<bool> DeleteReviewAsync(Guid id);

        Task EditReviewAsync(EditReviewDto editReviewDto);
    }
}
