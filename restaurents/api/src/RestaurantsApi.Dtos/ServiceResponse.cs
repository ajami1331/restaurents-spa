// ServiceResponse.cs
// Authors: Araf Al-Jami
// Created: 05-07-2021 10:08 PM
// Updated: 05-07-2021 10:08 PM

namespace RestaurantsApi.Dtos
{
    public class ServiceResponse<T>
    {
        public ServiceResponse(T data)
        {
            Data = data;
        }

        public ServiceResponse(string error)
        {
            Error = error;
        }

        public T Data { get; }

        public string Error { get; }
    }
}
