using Microsoft.AspNetCore.Mvc;

using MMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using MMS.Web.Controllers;
using MMS.Data.Services;

namespace MMS.Web.Controllers;

public class ReviewController : BaseController
{
    private readonly IMovieService svc;
    public ReviewController()
    {
        svc = new MovieServiceDb();
    }

    // GET /Review/index
    [Authorize(Roles = "admin,manager")]
    public IActionResult Index()
    {
        var reviews = svc.GetAllReviews();
        return View(reviews);
    }



    // GET /review/create
    [Authorize]
    public IActionResult Create()
    {
        var reviews = svc.GetAllReviews();

        var rvm = new ReviewViewModel
        {
            Movies = new SelectList(reviews, "Id", "Name")
        };

        // render blank form passing view model as a a parameter
        return View(rvm);
    }

    // POST /review/create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("MovieId")] ReviewViewModel rvm)
    {
        if (ModelState.IsValid)
        {
            var review = svc.CreateReview(rvm.MovieId, rvm.Statement, rvm.Rating);
            if (review is null)
            {
                Alert("Review not created", AlertType.warning);
            }
            else
            {
                Alert("Review Created", AlertType.success);
            }
            return RedirectToAction(nameof(Index));
        }

        // before sending viewmodel back (due to validation issues) repopulate the select list            
        rvm.Movies = new SelectList(svc.GetMovies(null, null), "Id", "Name");

        return View(rvm);
    }
}
