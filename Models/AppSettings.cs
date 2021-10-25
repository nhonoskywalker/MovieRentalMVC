using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRentalAppUI.Models
{
    public class AppSettings
    {
        public MovieRentalApi MovieRentalApi { get; set; }
    }

    public class MovieRentalApi
    {
        public string BaseUrl { get; set; }
    }
}
