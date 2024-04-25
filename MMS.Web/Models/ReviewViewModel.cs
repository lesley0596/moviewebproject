using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace MMS.Web.Models;
public class ReviewViewModel
{
    // selectlist of movies (id, name)       
    public SelectList Movies { set; get; }

 [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Statement { get; set; }

        [Required]
        [Range(1 , 5)]
        public int Rating { get; set; }

        [Required]
        public int MovieId { get; set; }


        public string MovieTitle { get; set; }
}