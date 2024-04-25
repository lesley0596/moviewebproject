using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MMS.Data.Entities;


public class MovieViewModel
{
    public MovieViewModel()

    {
        Genres = new SelectList(Enum.GetValues(typeof(Genre)).Cast<Genre>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");
    }

    public SelectList Genres { get; set; }

    
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

    [Url]
    public String PosterUrl { get; set; }


    [StringLength(500, MinimumLength = 10)]
    public String PlotSummary { get; set; }

    public int Rating { get; set; }

     public double StarRating { get; set; }

    [Required]
    public Genre Genre { get; set; }



}
