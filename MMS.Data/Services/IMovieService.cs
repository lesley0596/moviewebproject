using System;
using System.Collections.Generic;
using System.Data.Common;
using MMS.Data.Entities;

namespace MMS.Data.Services;

// This interface describes the operations that a MovieService class should implement
public interface IMovieService
{
    void Initialise();

    // ------------------------Movie Managment------------------------------            

    List<Movie> GetMovies(string orderBy, string direction);
    IList<Movie> SearchMovies(string query = null);
    Movie GetMovie(int id);
    Movie AddMovie(Movie m);
    Movie UpdateMovie(Movie updated);
    bool DeleteMovie(int id);

    // ------------------------Review Managment--------------------------

    Review CreateReview(int movieId, string comment, int rating);
    Review CreateReview(Review r);
    Review UpdateReview(int id, string comment, int rating);
    Review GetReview(int id);
    bool DeleteReview(int id);
    IList<Review>GetAllReviews();

}