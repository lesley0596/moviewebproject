
using System;
using System.Linq;
using Xunit;

using MMS.Data.Services;
using MMS.Data.Entities;

namespace MMS.Test;

// ==================== MovieService Tests =============================
[Collection("Sequential")]
public class MovieServiceTests
{
    private readonly IMovieService svc;

    public MovieServiceTests()
    {
        // general arrangement
        svc = new MovieServiceDb();

        // ensure data source is empty before each test
        svc.Initialise();
    }

    // ========================== GET ALL MOVIES TESTS  =========================

    [Fact]
    public void GetAllMovies_WhenNoneExist_ShouldReturn0()
    {
        // act
        var movie = svc.GetMovies(null);
        var count = movie.Count;

        // assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void GetMovies_With2Added_ShouldReturnCount2()
    {
        // arrange
        var movie1 = svc.AddMovie(
            new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
            );

        var movie2 = svc.AddMovie(
            new Movie { Title = "yyy", Year = 2001, Director = "yyy", Cast = "yyy", MovieDuration = 68, Composer = "yyy", PosterUrl = "yyy", PlotSummary = "http://photo.com/yyy.jpg", Genre = Genre.Romance }
            );

        // act
        var movies = svc.GetMovies(null);
        var count = movies.Count();

        // assert
        Assert.Equal(2, count);
    }

    // ======================= GET SINGLE MOVIE TESTS ======================


    [Fact]
    public void GetMovie_WhenNoneExist_ShouldReturnNull()
    {
        // act
        var movie = svc.GetMovie(1);

        // assert
        Assert.Null(movie);
    }

    [Fact]
    public void GetMovie_WhenAdded_ShouldReturnMovie()
    {
        // arrange
        var movie = svc.AddMovie(
            new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
            );

        // act
        var newMovie = svc.GetMovie(movie.Id);

        // assert
        Assert.NotNull(newMovie);
        Assert.Equal(movie.Id, newMovie.Id);
    }

    [Fact]
    public void GetMovie_WithReview_RetrievesMovieAndReviews()
    {
        // arrange
        var movie = svc.AddMovie(
           new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
           );
        svc.CreateReview(movie.Id, "comment", 5);

        // act      
        var anothermovie = svc.GetMovie(movie.Id);

        // assert
        Assert.NotNull(anothermovie);
        Assert.Equal(1, anothermovie.Reviews.Count);
    }

    [Fact]
    public void Movie_SearchForMovie_ReturnsOneMovie()
    {
          // arrange
        var movie = svc.AddMovie(
          new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
          );

        // act
        var searchResult = svc.SearchMovies("xxx");

        // assert
        Assert.Equal(1, searchResult.Count);                  
    }

     [Fact]
    public void Movie_SearchForMovies_ReturnsMoreThanOneMovie()
    {
          // arrange
        var movie = svc.AddMovie(
          new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
          );
           var movie2 = svc.AddMovie(
          new Movie { Title = "yyy", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
          );

        // act
        var searchResult = svc.SearchMovies("xxx");

        // assert
        Assert.Equal(2, searchResult.Count);                  
    }

    [Fact]
    public void Movie_SearchForMovie_WhenNoMatch_ReturnsZeroMovies()
    {
        // arrange
        var movie = svc.AddMovie(
          new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
          );

        // act
        var searchResult = svc.SearchMovies("yyy");

        // assert
        Assert.Equal(0, searchResult.Count);
    }



    // ========================= ADD MOVIE TESTS =============================

    [Fact]
    public void AddMovie_WhenValid_ShouldAddMovie()
    {
        // arrange
        var addedMovie = svc.AddMovie(
          new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
            );

        // act - retrieve newly added movie
        var m = svc.GetMovie(addedMovie.Id);

        // assert that movie is not null
        Assert.NotNull(m);

        // assert that properties are set properly??
        Assert.Equal(m.Id, m.Id);
        Assert.Equal("xxx", m.Title);
        Assert.Equal(2000, m.Year);
        Assert.Equal("xxx", m.Director);
        Assert.Equal("xxx", m.Cast);
        Assert.Equal(60, m.MovieDuration);
        Assert.Equal("xxx", m.Composer);
        Assert.Equal("http://photo.com/xxx.jpg", m.PosterUrl);
        Assert.Equal("xxx", m.PlotSummary);
        Assert.Equal(Genre.Action, m.Genre);

    }

    [Fact] // Add Movie duplicate tests
    public void AddMovie_WhenDuplicateTitleandYear_ShouldReturnNull()
    {
        // arrange
        var movie1 = svc.AddMovie(
            new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
            );

        // act
        var movie2 = svc.AddMovie(
            new Movie { Title = "xxx", Year = 2000, Director = "yyy", Cast = "yyy", MovieDuration = 120, Composer = "yyy", PosterUrl = "http://photo.com/yyy.jpg", PlotSummary = "yyy", Genre = Genre.Romance }
            );

        // assert
        Assert.NotNull(movie1);
        Assert.Null(movie2);
    }

    [Fact] // Add movie with invalid year
    public void AddMovie_WhenInvalidYear_ShouldReturnNull()
    {
        // act 
        var added = svc.AddMovie(
            new Movie { Title = "xxx", Year = 20508, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
            );

        // assert
        Assert.Null(added);
    }

    [Fact] // Add movie with invalid movie duration
    public void AddMovie_WhenInvalidDuration_ShouldReturnNull()
    {
        // act 
        var added = svc.AddMovie(
            new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = -1, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
            );

        // assert
        Assert.Null(added);
    }


    // ========================= UPDATE MOVIE TESTS ==========================

    [Fact]
    public void UpdateMovie_ThatExists_ShouldSetAllProperties()
    {
        // arrange
        var movie = svc.AddMovie(
          new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
            );

        // act - updating all properties of movie except the Id
        var updatedMovie = svc.UpdateMovie(
         new Movie { Id = movie.Id, Title = "yyy", Year = 2020, Director = "yyy", Cast = "yyy", MovieDuration = 120, Composer = "yyy", PosterUrl = "http://photo.com/yyy.jpg", PlotSummary = "yyy", Genre = Genre.Action }
            );

        // updated movie is reloaded from database into new object update
        var update = svc.GetMovie(movie.Id);

        // assert
        Assert.NotNull(update);

        // assert properties set correctly
        Assert.Equal(updatedMovie.Id, update.Id);
        Assert.Equal(updatedMovie.Title, update.Title);
        Assert.Equal(updatedMovie.Year, update.Year);
        Assert.Equal(updatedMovie.Director, update.Director);
        Assert.Equal(updatedMovie.Cast, update.Cast);
        Assert.Equal(updatedMovie.MovieDuration, update.MovieDuration);
        Assert.Equal(updatedMovie.Composer, update.Composer);
        Assert.Equal(updatedMovie.PosterUrl, update.PosterUrl);
        Assert.Equal(updatedMovie.PlotSummary, update.PlotSummary);
        Assert.Equal(updatedMovie.Genre, update.Genre);
    }

    [Fact]
    public void UpdateMovie_WhenDuplicateTitleAndYear_ShouldReturnNull()
    {
        // arrange
        var movie1 = svc.AddMovie(
            new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
            );

        var movie2 = svc.AddMovie(
            new Movie { Title = "yyy", Year = 2020, Director = "yyy", Cast = "yyy", MovieDuration = 120, Composer = "yyy", PosterUrl = "http://photo.com/yyy.jpg", PlotSummary = "yyy", Genre = Genre.Romance }
            );

        // act - updating movie2 with duplicate title and year of movie1
        var updated = svc.UpdateMovie(
            new Movie { Title = movie1.Title, Year = movie1.Year, Director = movie2.Director, Cast = movie2.Cast, MovieDuration = movie2.MovieDuration, Composer = movie2.Composer, PosterUrl = movie2.PosterUrl, PlotSummary = movie2.PlotSummary, Genre = movie2.Genre }
        );

        // assert
        Assert.NotNull(movie1);
        Assert.NotNull(movie2);
        Assert.Null(updated);
    }

    [Fact]
    public void UpdateMovie_WhenInvalidYear_ShouldReturnNull()
    {
        // arrange
        var added = svc.AddMovie(
            new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Romance }
            );

        // act - update year with invalid value
        var updated = svc.UpdateMovie(
            new Movie { Title = "xxx", Year = 1660, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
            );

        // assert
        Assert.NotNull(added);
        Assert.Null(updated);
    }

    [Fact]
    public void UpdateMovie_WhenInvalidDuration_ShouldReturnNull()
    {
        // arrange
        var added = svc.AddMovie(
            new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
            );

        // act - update year with invalid value
        var updated = svc.UpdateMovie(
            new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = -1, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
            );

        // assert
        Assert.NotNull(added);
        Assert.Null(updated);
    }

    // ===================================== DELETE MOVIE TESTS ==============================================

    [Fact]
    public void DeleteMovie_ThatExists_ShouldReturnTrue()
    {
        // arrange 
        var movie = svc.AddMovie(
            new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
            );

        // act
        var deleted = svc.DeleteMovie(movie.Id);

        // try to retrieve deleted movie
        var movie1 = svc.GetMovie(movie.Id);

        // assert
        Assert.True(deleted); // delete movie should return true
        Assert.Null(movie1);      // movie1 should be null
    }

    [Fact]
    public void DeleteMovie_ThatDoesntExist_ShouldReturnFalse()
    {
        // act 	
        var deleted = svc.DeleteMovie(0);

        // assert
        Assert.False(deleted);
    }




    // ====================== Review Tests ===================================
    [Fact]
    public void CreateReview_ForExistingMovie_ShouldBeCreated()
    {
        // arrange
        var movie = svc.AddMovie(
          new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
          );
        var r = new Review { MovieId = movie.Id, CreatedOn = DateTime.Now, Statement = "ok", Rating = 7 };

        // act
        svc.CreateReview(r);

        // assert
        Assert.NotNull(r);
        Assert.Equal(movie.Id, r.MovieId);
        //Assert.True(r.Active); 
    }

    [Fact]
    public void GetReview_WhenExists_ShouldReturnReviewandMovie()
    {
        // arrange
        var movie = svc.AddMovie(
          new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
          );
        var r = svc.CreateReview(movie.Id, "Dummy Review 1", 5);

        // act
        var review = svc.GetReview(r.Id);

        // assert
        Assert.NotNull(review);
        Assert.NotNull(review.Movie);
        Assert.Equal(movie.Title, review.Movie.Title);
    }

    [Fact]
    public void Review_UpdateReview_ShouldUpdateCommentandRating()
    {
        // arrange
        var movie = svc.AddMovie(
          new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
          );
        var r = svc.CreateReview(movie.Id, "Dummy Review 1", 5);


        // act
        svc.UpdateReview(r.Id, "DUMMY REVIEW", 4);
        var u = svc.GetReview(r.Id);

        // assert
        Assert.NotNull(u);
        Assert.Equal(u.MovieId, movie.Id);
        Assert.Equal(4, u.Rating);
        Assert.Equal("DUMMY REVIEW", u.Statement);
    }

    [Fact] // --- GetReviews When two added should return two 
    public void Review_GetReviews_WhenTwoAdded_ShouldReturnTwo()
    {
        // arrange
        var movie = svc.AddMovie(
          new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
          );
        var r1 = svc.CreateReview(movie.Id, "Dummy Review 1", 5);
        var r2 = svc.CreateReview(movie.Id, "Dummy Review 2", 3);

        // act
        var reviews = svc.GetAllReviews();

        // assert
        Assert.Equal(2, reviews.Count);
    }

    [Fact]
    public void DeleteReview_WhenExists_ShouldReturnTrue()
    {
        // arrange
        var movie = svc.AddMovie(
        new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
        );
        var r = svc.CreateReview(movie.Id, "Dummy Ticket", 5);

        // act
        var deleted = svc.DeleteReview(r.Id);// delete review   

        // assert
        Assert.True(deleted);// review should be deleted
    }

    [Fact]
    public void DeleteReview_WhenNonExistant_ShouldReturnFalse()
    {
        // arrange

        // act
        var deleted = svc.DeleteReview(1);// delete non-existent review   

        // assert
        Assert.False(deleted);// review should not be deleted
    }

    [Fact]
    public void DeleteReview_WhenValid_ShouldBeRemovedFromMovie()
    {
        // arrange
        var movie = svc.AddMovie(
         new Movie { Title = "xxx", Year = 2000, Director = "xxx", Cast = "xxx", MovieDuration = 60, Composer = "xxx", PosterUrl = "http://photo.com/xxx.jpg", PlotSummary = "xxx", Genre = Genre.Action }
         );
        var r = svc.CreateReview(movie.Id, "Dummy Ticket", 5);

        // act
        svc.DeleteReview(r.Id);// delete the review
        var ns = svc.GetMovie(movie.Id); // reload the movie

        // assert
        Assert.NotNull(ns);
        Assert.Equal(0, ns.Reviews.Count);
    }


}

// ==================== UserService Tests =============================
[Collection("Sequential")]
public class UserServiceTests
{
    private readonly IUserService svc;

    public UserServiceTests()
    {
        // general arrangement
        svc = new UserServiceDb();

        // ensure data source is empty before each test
        svc.Initialise();
    }

    // ========================== User Tests =========================

    [Fact] // --- Register Valid User test
    public void User_Register_WhenValid_ShouldReturnUser()
    {
        // arrange 
        var reg = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);

        // act
        var user = svc.GetUserByEmail(reg.Email);

        // assert
        Assert.NotNull(reg);
        Assert.NotNull(user);
    }

    [Fact] // --- Register Duplicate Test
    public void User_Register_WhenDuplicateEmail_ShouldReturnNull()
    {
        // arrange 
        var s1 = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);

        // act
        var s2 = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);

        // assert
        Assert.NotNull(s1);
        Assert.Null(s2);
    }

    [Fact] // --- Authenticate Invalid Test
    public void User_Authenticate_WhenInValidCredentials_ShouldReturnNull()
    {
        // arrange 
        var s1 = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);

        // act
        var user = svc.Authenticate("xxx@email.com", "guest");
        // assert
        Assert.Null(user);

    }

    [Fact] // --- Authenticate Valid Test
    public void User_Authenticate_WhenValidCredentials_ShouldReturnUser()
    {
        // arrange 
        var s1 = svc.Register("XXX", "xxx@email.com", "admin", Role.admin);

        // act
        var user = svc.Authenticate("xxx@email.com", "admin");

        // assert
        Assert.NotNull(user);
    }

}


