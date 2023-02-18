// GenericBCryptHasher.cs
// Authors: Araf Al-Jami
// Created: 24-06-2021 3:15 PM
// Updated: 24-06-2021 3:15 PM

namespace RestaurantsApi.IdentityServer
{
    using BCrypt.Net;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// This class is responsible for hashing password.
    /// </summary>
    /// <typeparam name="T">ClassType.</typeparam>
    public class GenericBCryptHasher<T> : IPasswordHasher<T>
        where T : class
    {
        public GenericBCryptHasher()
        {
        }

        /// <summary>
        /// This method hash password.
        /// </summary>
        /// <param name="user">user.</param>
        /// <param name="password">password.</param>
        /// <returns>string.</returns>
        public string HashPassword(T user, string password)
        {
            return BCrypt.HashPassword(password);
        }

        /// <summary>
        /// This method verifies hashed password.
        /// </summary>
        /// <param name="user">user.</param>
        /// <param name="hashedPassword">hashedPassword.</param>
        /// <param name="providedPassword">providedPassword.</param>
        /// <returns>PasswordVerificationResult.</returns>
        public PasswordVerificationResult VerifyHashedPassword(T user, string hashedPassword, string providedPassword)
        {
            bool result = BCrypt.Verify(providedPassword, hashedPassword);
            return result ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
