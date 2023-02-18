// IUserContextProvider.cs
// Authors: Araf Al-Jami
// Created: 05-07-2021 1:37 PM
// Updated: 05-07-2021 1:37 PM

namespace RestaurantsApi.Services.Abstractions
{
    using System.Threading.Tasks;
    using RestaurantsApi.IdentityServer;

    public interface IUserContextProvider
    {
        UserContext GetUserContext();
    }
}
