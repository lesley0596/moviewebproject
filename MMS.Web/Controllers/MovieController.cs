using System.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MMS.Data.Entities;
using MMS.Data.Services;
using MMS.Web.Models;

namespace MMS.Web.Controllers;

public class MovieController : BaseController
{
    private IMovieService svc;

    public MovieController()
    {
        svc = new MovieServiceDb();
    }

    // method to convert a MovieViewModel object into a Movie object
    public static Movie ToMovie(MovieViewModel mvm)
    {
        if (mvm != null)
        {
            var m = new Movie
            {
                // map the data properties of a MovieViewModel to a new Movie object
                Id = mvm.Id,
                Title = mvm.Title,
                Director = mvm.Director,
                MovieDuration = mvm.MovieDuration,
                PosterUrl = mvm.PosterUrl,
                PlotSummary = mvm.PlotSummary,
                Year = mvm.Year,
                Cast = mvm.Cast,
                Genre = mvm.Genre,
                //Reviews = mvm.Reviews                 
            };

            return m;
        }
        return null;
    }

    // method to convert a Movie object into a MovieViewModel object
    public static MovieViewModel ToMovieViewModel(Movie m)
    {
        if (m != null)
        {
            var mvm = new MovieViewModel
            {
                // map the data properties of a Movie to a new MovieViewModel object
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                MovieDuration = m.MovieDuration,
                PosterUrl = m.PosterUrl,
                PlotSummary = m.PlotSummary,
                Year = m.Year,
                Rating = m.Rating,
                //ReviewsCount = m.ReviewsCount,
                Cast = m.Cast,
                Genre = m.Genre,
                //Reviews = m.Reviews.ToList(),
            };
            return mvm;
        }
        return null;
    }

    // method to convert a Review object into a ReviewViewModel object
    public static ReviewViewModel ToReviewViewModel(Review r)
    {
        if (r != null)
        {
            var mvm = new ReviewViewModel
            {
                Id = r.Id,
                MovieId = r.MovieId,
                CreatedOn = r.CreatedOn,
                Statement = r.Statement,
                Rating = r.Rating
            };
            return mvm;
        }
        return null;
    }

    // method to convert a ReviewViewModel object into a Review object
    public static Review ToReview(ReviewViewModel rvm)
    {
        if (rvm != null)
        {
            var r = new Review
            {
                Id = rvm.Id,
                MovieId = rvm.MovieId,
                CreatedOn = rvm.CreatedOn,
                Statement = rvm.Statement,
                Rating = rvm.Rating

            };
            return r;
        }
        return null;
    }


    public IActionResult Index(MovieSearchViewModel search = null, string orderBy = null, string direction=null)
    {
        if (search != null && !string.IsNullOrEmpty(search.Query))
        {
            // Search movies if search parameter is provided
            search.Movies = svc.SearchMovies(search.Query);
            return View(search);
        }
        else
        {
            // Get movies based on orderBy parameter
            var list = svc.GetMovies(orderBy, direction);
            var viewModel = new MovieSearchViewModel
            {
                Movies = list,
            };
            return View(viewModel);
        }
    }

    // // GET /movie
    // [HttpGet("movie/index")]
    // public IActionResult Index(string orderBy)
    // {
    //     var list = svc.GetMovies(orderBy);
    //     var viewModel = new MovieSearchViewModel
    //     {
    //         Movies = list,
    //     };
    //     // // render Index view with the list
    //     return View(viewModel);
    // }

    // GET /movie/details
    public IActionResult Details(int id)
    {
        var m = svc.GetMovie(id);

        // check if m is null and return NotFound()
        if (m == null)
        {
            return NotFound();
        }

        // pass movie as parameter to the view
        return View(m);
    }

    //  GET /movie/create
    [Authorize(Roles = "admin, contributor")]
    public IActionResult Create()
    {
        // display blank form to create a movie
        return View(new MovieViewModel());
    }

    // POST /movie/create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin, contributor")]
    public IActionResult Create(MovieViewModel mvm)
    {

        // complete post action to add movie
        if (ModelState.IsValid)
        {
            var m = ToMovie(mvm);

            svc.AddMovie(m);
            Alert("Movie created successfully.", AlertType.success);
            //mvm.Id = m.Id;
            return RedirectToAction(nameof(Index));
        }

        // redisplay form for editing if validation errors
        Alert("Failed to add new movie. Please enter details again.", AlertType.danger);
        return View(mvm);
    }

    // GET /movie/edit/
    [Authorize(Roles = "admin, contributor")]
    public IActionResult Edit(int id)
    {
        // load the movie using the service
        var m = svc.GetMovie(id);

        // check if m is null and return NotFound()
        if (m == null)
        {
            Alert("Unable to edit movie. Please try again", AlertType.danger);
            return NotFound();
        }

        // pass movie to view for editing
        return View(m);
    }

    // POST /movie/edit/
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin, contributor")]
    public IActionResult Edit(int id, Movie m)
    {
        // complete POST action to save movie changes
        if (ModelState.IsValid)
        {
            // pass data to service to update - check if update was successful
            svc.UpdateMovie(m);

            Alert("Movie updated successfully.", AlertType.success);
            return RedirectToAction(nameof(Index));
        }

        // redisplay the form for editing as validation errors
        Alert("Failed to edit the movie details. Please try again.", AlertType.danger);
        return View(m);
    }

    // GET /movie/delete/
    [Authorize(Roles = "admin")]
    public IActionResult Delete(int id)
    {
        // load the student using the service
        var m = svc.GetMovie(id);

        // check the returned movie is not null and if so return NotFound()
        if (m == null)
        {
            return NotFound();
        }

        // pass movie to view for deletion confirmation
        Alert($"Confirm delete {m.Title}", AlertType.danger);
        return View(m);
    }

    // POST /movie/delete/
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public IActionResult DeleteConfirm(int id)
    {
        // delete movie via service
        svc.DeleteMovie(id);

        Alert("The movie has been deleted!", AlertType.success);

        // redirect to the index view
        return RedirectToAction(nameof(Index));
    }

    // // GET /movie/index
    // [HttpGet("movie")]
    // public IActionResult Index(MovieSearchViewModel search)
    // {

    //     search.Movies = svc.SearchMovies(search.Query);
    //     return View(search);
    // }


    // ========================= Movie Review Management ==========================

    // GET /movie/reviewcreate/{id}
    [Authorize(Roles = "admin, contributor")]
    public IActionResult ReviewCreate(int id)
    {
        var movie = svc.GetMovie(id);
        if (movie == null)
        {
            Alert("Movie not found", AlertType.warning);
            return RedirectToAction(nameof(Index));
        }

        // create a ticket view model and set foreign key
        var review = new Review { MovieId = id };
        // render blank form
        return View(review);
    }

    // POST /movie/reviewcreate
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin, contributor")]
    public IActionResult ReviewCreate(Review r)
    {
        if (ModelState.IsValid)
        {
            var review = svc.CreateReview(r);
            if (review is not null)
            {
                Alert("Review Created Successfully", AlertType.success);
            }
            else
            {
                Alert("Review could not be created", AlertType.warning);
            }

            // redirect to display movie - note how Id is passed
            return RedirectToAction(nameof(Details), new { Id = review.MovieId }
            );
        }
        // redisplay the form for editing
        return View(r);
    }

    // GET /movie/reviewedit/{id}
    [Authorize(Roles = "admin, contributor")]
    public IActionResult ReviewEdit(int id)
    {
        var review = svc.GetReview(id);
        if (review == null)
        {
            Alert("Review not found", AlertType.warning);
            return RedirectToAction(nameof(Index));
        }
        return View(review);
    }

    // POST /movie/reviewedit
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin, contributor")]
    public IActionResult ReviewEdit(int id, Review r)
    {
        if (ModelState.IsValid)
        {
            var review = svc.UpdateReview(id, r.Statement, r.Rating);
            if (review is not null)
            {
                Alert("Review updated Successfully", AlertType.success);
            }
            else
            {
                Alert("Review could not be updated", AlertType.warning);
            }

            return RedirectToAction(
                nameof(Details), new { Id = review.MovieId }
            );
        }
        // redisplay the form for editing
        return View(r);
    }

    // GET /movie/reviewdelete/{id}
    [Authorize(Roles = "admin")]
    public IActionResult ReviewDelete(int id)
    {
        // load the ticket using the service
        var review = svc.GetReview(id);
        // check the returned Ticket is not null and if so return NotFound()
        if (review == null)
        {
            Alert("Review not found", AlertType.warning);
            return RedirectToAction(nameof(Index)); ;
        }

        // pass ticket to view for deletion confirmation
        return View(review);
    }

    // POST /movie/reviewdeleteconfirm/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public IActionResult ReviewDeleteConfirm(int id, int movieId)
    {
        // TBC delete ticket via service
        var deleted = svc.DeleteReview(id);
        if (deleted)
        {
            Alert("Review deleted succcessfully", AlertType.success);
        }
        else
        {
            Alert("Review could not  be deleted", AlertType.warning);
        }

        // redirect to the student details view
        return RedirectToAction(nameof(Details), new { Id = movieId });
    }

}
















