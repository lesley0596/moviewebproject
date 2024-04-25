using System.ComponentModel.DataAnnotations;
using MMS.Data.Validators;

namespace MMS.Data.Entities;


public enum Genre
{
    Action, Adventure, Comedy, Drama, Family, Fantasy, Horror, Romance,
};

public class Movie
{
    // class attributes 
    public int Id { get; set; } // primary key

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public String Title { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public String Director { get; set; }

    [Required]
    [Range(1889, 2026, ErrorMessage = "Year must be after 1888.")]
    public int Year { get; set; }

    [StringLength(200, MinimumLength = 1)]
    public string Cast { get; set; }

    [Required]
    [Range(1, 999)]
    public int MovieDuration { get; set; }

    [StringLength(100, MinimumLength = 1)]
    public String Composer { get; set; }

    [UrlResource]
    public String PosterUrl { get; set; }


    [StringLength(500, MinimumLength = 10)]
    public String PlotSummary { get; set; }

    [Required]
    public Genre Genre { get; set; }

    // read only properties
    public int Rating => (int)AverageRating();
    private double AverageRating()
    {
        if (ReviewsCount > 0)
        {
            return Reviews.Average(a => a.Rating);
        }
        else
        {
            return 0;
        }
    }

    public double StarRating => CalculateStarRating();

    private double CalculateStarRating()
    {
        if (Rating > 0)
        {
            // calculate whole stars
            var average = AverageRating();

            // calculate the number of half stars
            var remainder = average - (int)average;

            if (remainder < 0.5) 
            {
                return (int)average;
            }
            // if the remainder of Rating is 0 then only whole stars needed
            else     
            {
                return (int)average +0.5;
            }
        }
        else
        {
            return 0;
        }
    }

    public int ReviewsCount => Reviews.Count;

    // 1-N Relationship
    public IList<Review> Reviews { get; set; } = new List<Review>();



}
