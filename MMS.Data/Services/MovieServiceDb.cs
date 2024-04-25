using System.Data.Common;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MMS.Data.Entities;
using MMS.Data.Repository;

namespace MMS.Data.Services;

// EntityFramework Implementation of IMovieService
public class MovieServiceDb : IMovieService
{
    private readonly DataContext db;

    public MovieServiceDb()
    {
        db = new DataContext();
    }

    public void Initialise()
    {
        db.Initialise(); // recreate database
    }

    // ==================== Movie Management ==================

    // retrieve list of movies
    // can be ordered by title, director, year and genre
    public List<Movie> GetMovies(string orderBy, string direction)
    {
        switch (orderBy, direction)
        {
            case ("title", "asc"):
                return db.Movies.Include(r => r.Reviews).OrderBy(o => o.Title).ToList();
            case ("title", "desc"):
                return db.Movies.Include(r => r.Reviews).OrderByDescending(o => o.Title).ToList();
            case ("director", "asc"):
                return db.Movies.Include(r => r.Reviews).OrderBy(o => o.Director).ToList();
             case ("director", "desc"):
                return db.Movies.Include(r => r.Reviews).OrderByDescending(o => o.Director).ToList();
            case ("year", "asc"):
                return db.Movies.Include(r => r.Reviews).OrderBy(o => o.Year).ToList();
            case ("year", "desc"):
                return db.Movies.Include(r => r.Reviews).OrderByDescending(o => o.Year).ToList();
            case ("genre", "asc"):
                return db.Movies.Include(r => r.Reviews).OrderBy(o => o.Genre).ToList();
            case ("genre", "desc"):
                return db.Movies.Include(r => r.Reviews).OrderByDescending(o => o.Genre).ToList();
            default:
                return db.Movies.Include(r => r.Reviews).ToList();
        }
    }

    // retrieve movie by Id
    public Movie GetMovie(int id)
    {
        return db.Movies.Include(m => m.Reviews).FirstOrDefault(m => m.Id == id);
    }


    // add the new movie return movie if successful otherwise return null
    public Movie AddMovie(Movie m)
    {
        // check the movie to be added doesn't already exist
        var movieExists = db.Movies.FirstOrDefault(n => n.Title == m.Title && n.Year == m.Year);

        // if the movie already exists, return null
        if (movieExists != null) return null;

        // check year is valid // added after test failed that added movie with invalid year
        if (m.Year <= 1888 || m.Year >= DateTime.Now.Year + 2) return null;

        // check duration is valid
        if (m.MovieDuration < 0 || m.MovieDuration > 999) return null;

        // create new movie
        var newMovie = new Movie
        {
            Title = m.Title,
            Year = m.Year,
            Director = m.Director,
            Cast = m.Cast,
            MovieDuration = m.MovieDuration,
            Composer = m.Composer,
            PosterUrl = m.PosterUrl,
            PlotSummary = m.PlotSummary,
            Genre = m.Genre
        };

        // add movie to the context and save changes to the database
        db.Movies.Add(newMovie);
        db.SaveChanges();

        // return movie
        return newMovie;
    }

    // delete the student identified by Id returning true if deleted and false if not found
    public bool DeleteMovie(int id)
    {
        var m = GetMovie(id);
        if (m == null)
        {
            return false;
        }
        db.Movies.Remove(m);
        db.SaveChanges();
        return true;
    }

    // update the movie with new details
    public Movie UpdateMovie(Movie updated)
    {
        // verify the movie exists
        var movie = GetMovie(updated.Id);
        if (movie == null)
        {
            return null;
        }

        // check year is valid // added after test failed that added movie with invalid year
        if (updated.Year <= 1888 || updated.Year >= DateTime.Now.Year + 2) return null;

        // check duration is valid
        if (updated.MovieDuration < 0 || updated.MovieDuration > 999) return null;

        // update details of movie retrieved
        movie.Title = updated.Title;
        movie.Year = updated.Year;
        movie.Director = updated.Director;
        movie.Cast = updated.Cast;
        movie.MovieDuration = updated.MovieDuration;
        movie.Composer = updated.Composer;
        movie.PosterUrl = updated.PosterUrl;
        movie.PlotSummary = updated.PlotSummary;
        movie.Genre = updated.Genre;

        db.SaveChanges();
        return movie;
    }

    // movie search feature
    public IList<Movie> SearchMovies(string query = null)
    {
        // ensure query is not null and convert to lowercase 
        query = query == null ? "" : query.ToLower();

        // search movie // as enumerable to translate linq query to sql database(troubles with it reading ToString on my enum genre)
        var results = db.Movies.AsEnumerable()
                                .Where(m => m.Title.ToLower().Contains(query) ||
                                m.Director.ToLower().Contains(query) ||
                                m.Genre.ToString().ToLower().Contains(query) || // Convert each genre to string                               
                                m.Year.ToString().Contains(query)).ToList();

        return results;
    }



    // =========================== Review Management =====================

    public Review CreateReview(int movieId, string comment, int rating)
    {
        var movie = GetMovie(movieId);
        if (movie == null) return null;

        var review = new Review
        {
            // Id created by Database
            Statement = comment,
            MovieId = movieId,
            // set by default in model but we can override here if required
            CreatedOn = DateTime.Now,
            Rating = rating
        };
        db.Reviews.Add(review);
        db.SaveChanges(); // write to database
        return review;
    }

    // Convenience method to align review creation format used in Student Creation
    public Review CreateReview(Review r)
    {
        return CreateReview(r.MovieId, r.Statement, r.Rating);
    }



    public Review GetReview(int id)
    {
        // return review and related movie or null if not found
        return db.Reviews.Include(r => r.Movie).FirstOrDefault(r => r.Id == id);
    }

    public Review UpdateReview(int id, string comment, int rating)
    {
        var review = GetReview(id);
        if (review == null) return null;

        review.Statement = comment;
        review.Rating = rating;

        db.Reviews.Update(review);
        db.SaveChanges(); // write to database
        return review;
    }



    public bool DeleteReview(int id)
    {
        // find ticket
        var review = GetReview(id);
        if (review == null) return false;

        // remove ticket 
        var result = db.Reviews.Remove(review);

        db.SaveChanges();
        return true;
    }

    // Retrieve all reviews and the movie associated with the review
    public IList<Review> GetAllReviews()
    {
        return db.Reviews.Include(r => r.Movie).ToList();
    }




}
