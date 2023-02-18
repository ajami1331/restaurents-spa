// PageResponse.cs
// Authors: Araf Al-Jami
// Created: 05-07-2021 4:17 PM
// Updated: 05-07-2021 4:17 PM

namespace RestaurantsApi.ViewModels
{
    using System.Collections;
    using System.Collections.Generic;

    public class PageResponse<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int PageCount { get; set; }

        public int PageNumber { get; set; }
    }
}
