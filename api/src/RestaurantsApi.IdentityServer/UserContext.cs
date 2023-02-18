// UserContext.cs
// Authors: Araf Al-Jami
// Created: 05-07-2021 1:43 PM
// Updated: 05-07-2021 1:43 PM

namespace RestaurantsApi.IdentityServer
{
    using System;

    public class UserContext
    {
        public Guid Id { get; set; }

        public string[] Roles { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }
    }
}
