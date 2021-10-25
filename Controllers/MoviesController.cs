using Microsoft.AspNetCore.Mvc;
using MovieRentalAppUI.Models;
using MovieRentalAppUI.Services;
using MRAPP.Insfrastructure.Messages.LogIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentalAppUI.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRentalService _movieRentalService;

        [BindProperty]
        public MovieModel MovieModel { get; set; }

        public MoviesController(IMovieRentalService movieRentalService)
        {
            _movieRentalService = movieRentalService;
        }

        public IActionResult Index()
        {
            var isSignedIn = _movieRentalService.IsLoggedIn();

            var res = isSignedIn ? _movieRentalService.GetMoviesAsync().Result : new List<MovieModel>();
            return View("Index", res);
        }

        #region API Calls
        [HttpGet]
        public IActionResult Getall()
        {
            var res = _movieRentalService.GetMoviesAsync().Result;
            return Json(new { data = res});
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            MovieModel = new MovieModel();

            if (id == null)
            {
                //create
                return View(MovieModel);
            }

            //update
            MovieModel = await _movieRentalService.GetMovieByIdAsync(id);
            if (MovieModel == null)
            {
                return NotFound();
            }
            return View(MovieModel);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert()
        {
            MovieModel result = null;

            if (ModelState.IsValid)
            {

                if (MovieModel.Id == null)
                {
                    result = await _movieRentalService.AddMovieAsync(MovieModel);
                }
                else
                {
                    result = await _movieRentalService.UpdateMovieAsync(MovieModel);
                }

                return RedirectToAction("Index");
            }

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            _= await _movieRentalService.DeleteMovieAsync(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInRequest request)
        {
            var result = await _movieRentalService.LoginAsync(request);

            if (result.IsSuccessful)
            {
                HttpContext.Session.Set("userToken", Encoding.Default.GetBytes(result.Data));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
