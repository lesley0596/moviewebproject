
using System.Dynamic;

namespace MMS.Data.Entities;
public class Review
{     
    //class attributes
     public int Id { get; set; }
     public String Statement {get; set;}
     public DateTime CreatedOn {get; set;} = DateTime.Now;
     public int Rating {get; set;}
     
    
    // Foreign key for Movie model
    public int MovieId{get; set;}

    // Navigation property
    public Movie Movie{get; set;}


}
