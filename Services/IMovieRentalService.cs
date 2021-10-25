using MovieRentalAppUI.Models;
using MRAPP.Insfrastructure.Messages.LogIn;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRentalAppUI.Services
{
    public interface IMovieRentalService
    {
        Task<IEnumerable<MovieModel>> GetMoviesAsync();

        Task<MovieModel> GetMovieByIdAsync(Guid? id);

        Task<MovieModel> AddMovieAsync(MovieModel model);

        Task<MovieModel> UpdateMovieAsync(MovieModel model);

        Task<MovieModel> DeleteMovieAsync(Guid id);

        Task<LogInResponse> LoginAsync(LogInRequest request);

        bool IsLoggedIn();
        string GetUser();
        void LogOut();
    }
}
