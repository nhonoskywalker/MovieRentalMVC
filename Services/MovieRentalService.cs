using Microsoft.AspNetCore.Http;
using MovieRentalAppUI.Models;
using MRAPP.Insfrastructure.Messages.LogIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieRentalAppUI.Services
{
    public class MovieRentalService : IMovieRentalService
    {
        private readonly HttpClient httpClient;
        private readonly HttpContext _httpContext;
        private readonly ITokenService _tokenService;

        public MovieRentalService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
        {
            httpClient = httpClientFactory.CreateClient("movieRentalApi");
            _httpContext = httpContextAccessor.HttpContext;
            _tokenService = tokenService;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetUserToken());
        }

        public async Task<MovieModel> DeleteMovieAsync(Guid id)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"/api/movies/{id}");
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(new { }), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequest);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ResponseModel<MovieModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result.Data;
        }

        public async Task<IEnumerable<MovieModel>> GetMoviesAsync()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/movies");
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(new { }), Encoding.UTF8, "application/json");          

            var response = await httpClient.SendAsync(httpRequest);

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ResponseModel<IEnumerable<MovieModel>>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result.Data;
        }

        public async Task<MovieModel> GetMovieByIdAsync(Guid? id)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/movies/{id}");
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(new { }), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequest);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ResponseModel<MovieModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result.Data;
        }

        public async Task<MovieModel> AddMovieAsync(MovieModel model)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/api/movies");
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequest);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<MovieModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

        public async Task<MovieModel> UpdateMovieAsync(MovieModel model)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, "/api/movies");
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequest);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<MovieModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

        public async Task<LogInResponse> LoginAsync(LogInRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/api/login");
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequest);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LogInResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(GetUserToken()) ? true : false;
        }

        public string GetUser()
        {
            var claims = _tokenService.GetTokenClaims(GetUserToken());
            if (claims == null)
            {
                return "Hooman";
            }

            var user = claims.First(claim => claim.Type == "unique_name").Value;

            return user;
        }

        private string GetUserToken()
        {
            byte[] userToken;
            var userTokenString = string.Empty;
            userToken = _httpContext.Session.Get("userToken");

            if (userToken != null)
            {
                userTokenString = Encoding.UTF8.GetString(userToken);
            }

            return userTokenString;
        }

        public void LogOut()
        {
            _httpContext.Session.Clear();
        }
    }
}
