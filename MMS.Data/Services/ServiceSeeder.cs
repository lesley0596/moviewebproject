using Microsoft.EntityFrameworkCore.Internal;
using MMS.Data.Entities;
namespace MMS.Data.Services;

public static class ServiceSeeder
{

    // default seeder using Db versions of services
    public static void Seed()
    {
        IUserService usvc = new UserServiceDb();
        IMovieService rsvc = new MovieServiceDb();

        usvc.Initialise();

        SeedUsers(usvc);
        SeedMovies(rsvc);
    }

    // use this method FIRST to seed the database with dummy test data using an IUserService
    private static void SeedUsers(IUserService svc)
    {
        // Note: do not call initialise here

        svc.Register("admin", "admin@mail.com", "password", Role.admin);
        svc.Register("guest", "guest@mail.com", "password", Role.guest);
        svc.Register("contributor", "contributor@mail.com", "password", Role.contributor);
        svc.Register("lesley2305", "lesley@mail.com", "password", Role.contributor);

    }

    // use this method SECOND to seed the database with dummy test data using an IRecipeService
    private static void SeedMovies(IMovieService svc)
    {
        // Note: do not call initialise here

        // add relevant movie seed data 
        var movie1 = svc.AddMovie(new Movie
        {
            Title = "Avatar",
            Year = 2009,
            Director = "James Cameron",
            Cast = "Sam Worthington, Zoe Saldana, Sigourney Weaver",
            MovieDuration = 162,
            Composer = "James Horner",
            Genre = Genre.Fantasy,
            PlotSummary = "Jake, a paraplegic marine, replaces his brother on the Na'vi-inhabited Pandora for a corporate mission. He is accepted by the natives as one of their own, but he must decide where his loyalties lie.",
            PosterUrl = "https://image.tmdb.org/t/p/original/kyeqWdyUXW608qlYkRqosgbbJyK.jpg"
        });


        var movie2 = svc.AddMovie(new Movie
        {
            Title = "The Martian",
            Year = 2015,
            Director = "Ridley Scott",
            Cast = "Matt Damon, Jessica Chastain, Kristen Wiig",
            MovieDuration = 144,
            Composer = "Harry Gregson-Williams",
            Genre = Genre.Adventure,
            PlotSummary = "An astronaut becomes stranded on Mars after his team assume him dead, and must rely on his ingenuity to find a way to signal to Earth that he is alive and can survive until a potential rescue.",
            PosterUrl = "https://image.tmdb.org/t/p/original/5BHuvQ6p9kfc091Z8RiFNhCwL4b.jpg"
        });

        var movie3 = svc.AddMovie(new Movie
        {
            Title = "Howl's Moving Castle",
            Year = 2004,
            Director = "Hayao Miyazaki",
            Cast = "Chieko Baisho, Takuya Kimura, Tatsuya Gashuin",
            MovieDuration = 119,
            Composer = "Joe Hisaishi",
            Genre = Genre.Family,
            PlotSummary = "When an unconfident young woman is cursed with an old body by a spiteful witch, her only chance of breaking the spell lies with a self-indulgent yet insecure young wizard and his companions in his legged, walking castle.",
            PosterUrl = "https://image.tmdb.org/t/p/original/yeJ5Nj1k2NA0bBZjkZ10PvQKjTD.jpg"
        });

        var movie4 = svc.AddMovie(new Movie
        {
            Title = "Jurassic Park",
            Year = 1993,
            Director = "Steven Spielberg",
            Cast = "Sam Neill, Laura Dern, Jeff Goldblum",
            MovieDuration = 127,
            Composer = "John Williams",
            Genre = Genre.Adventure,
            PlotSummary = "A pragmatic paleontologist touring an almost complete theme park on an island in Central America is tasked with protecting a couple of kids after a power failure causes the park's cloned dinosaurs to run loose.",
            PosterUrl = "https://image.tmdb.org/t/p/original/oU7Oq2kFAAlGqbU4VoAE36g4hoI.jpg"
        });

        var movie5 = svc.AddMovie(new Movie
        {
            Title = "Barbie",
            Year = 2023,
            Director = "Greta Gerwig",
            Cast = "Margot Robbie, Ryan Gosling, Issa Rae",
            MovieDuration = 114,
            Composer = "Mark Ronson",
            Genre = Genre.Fantasy,
            PlotSummary = "Barbie and Ken are having the time of their lives in the colorful and seemingly perfect world of Barbie Land. However, when they get a chance to go to the real world, they soon discover the joys and perils of living among humans.",
            PosterUrl = "https://image.tmdb.org/t/p/original/iuFNMS8U5cb6xfzi51Dbkovj7vM.jpg"
        });

        var movie6 = svc.AddMovie(new Movie
        {
            Title = "Hot Fuzz",
            Year = 2007,
            Director = "Edgar Wright",
            Cast = "Simon Pegg, Nick Frost, Martin Freeman",
            MovieDuration = 121,
            Composer = "David Arnold",
            Genre = Genre.Comedy,
            PlotSummary = "A skilled London police officer, after irritating superiors with his embarrassing effectiveness, is transferred to a village where the easygoing officers object to his fervor for regulations, as a string of grisly murders strikes the town.",
            PosterUrl = "https://image.tmdb.org/t/p/original/zPib4ukTSdXvHP9pxGkFCe34f3y.jpg"
        });

        var movie7 = svc.AddMovie(new Movie
        {
            Title = "Harry Potter and the Philosopher's Stone",
            Year = 2001,
            Director = "Chris Columbus",
            Cast = "Daniel Radcliffe, Rupert Grint, Emma Watson",
            MovieDuration = 152,
            Composer = "John Williams",
            Genre = Genre.Fantasy,
            PlotSummary = "An orphaned boy enrolls in a school of wizardry, where he learns the truth about himself, his family and the terrible evil that haunts the magical world.",
            PosterUrl = "https://image.tmdb.org/t/p/original/wuMc08IPKEatf9rnMNXvIDxqP4W.jpg"
        });

        var movie8 = svc.AddMovie(new Movie
        {
            Title = "No Time to Die",
            Year = 2021,
            Director = "Cary Joji Fukunaga",
            Cast = "Daniel Craig, Ana de Armas, Rami Malek",
            MovieDuration = 163,
            Composer = "Hans Zimmer",
            Genre = Genre.Action,
            PlotSummary = "James Bond has left active service. His peace is short-lived when Felix Leiter, an old friend from the CIA, turns up asking for help, leading Bond onto the trail of a mysterious villain armed with dangerous new technology.",
            PosterUrl = "https://image.tmdb.org/t/p/original/iUgygt3fscRoKWCV1d0C7FbM9TP.jpg"
        });

        var movie9 = svc.AddMovie(new Movie
        {
            Title = "Monsters, Inc",
            Year = 2001,
            Director = "Pete Docter, David Silverman, Lee Unkrich",
            Cast = "Billy Crystal, John Goodman, Mary Gibbs",
            MovieDuration = 92,
            Composer = "Randy Newman",
            Genre = Genre.Family,
            PlotSummary = "In order to power the city, monsters have to scare children so that they scream. However, the children are toxic to the monsters, and after a child gets through, two monsters realize things may not be what they think.",
            PosterUrl = "https://image.tmdb.org/t/p/original/1dnf03F2imnIOzzjXqlUzCdgfTB.jpg"
        });

        var movie10 = svc.AddMovie(new Movie
        {
            Title = "Back to the Future",
            Year = 1985,
            Director = "Robert Zemeckis",
            Cast = "Michael J.Fox, Christopher Lloyd, Lea Thompson",
            MovieDuration = 116,
            Composer = "Alan Silvestri",
            Genre = Genre.Adventure,
            PlotSummary = "Marty McFly, a 17-year-old high school student, is accidentally sent 30 years into the past in a time-traveling DeLorean invented by his close friend, the maverick scientist Doc Brown.",
            PosterUrl = "https://image.tmdb.org/t/p/original/fNOH9f1aA7XRTzl1sAOx9iF553Q.jpg"
        });

        var movie11 = svc.AddMovie(new Movie
        {
            Title = "King Kong",
            Year = 2005,
            Director = "Peter Jackson",
            Cast = "Naomi Watts, Jack Black, Adrien Brody",
            MovieDuration = 187,
            Composer = "James Newton Howard",
            Genre = Genre.Action,
            PlotSummary = "A greedy film producer assembles a team of moviemakers and sets out for the infamous Skull Island, where they find more than just cannibalistic natives.",
            PosterUrl = "https://image.tmdb.org/t/p/original/qVCZtwBGls866wBb4PAfYtAjyr8.jpg"
        });

        var movie12 = svc.AddMovie(new Movie
        {
            Title = "Alien",
            Year = 1979,
            Director = "Ridley Scott",
            Cast = "Sigourney Weaver, Tom Skerritt, John Hurt",
            MovieDuration = 117,
            Composer = "",
            Genre = Genre.Horror,
            PlotSummary = "The crew of a commercial spacecraft encounters a deadly lifeform after investigating a mysterious transmission of unknown origin.",
            PosterUrl = "https://image.tmdb.org/t/p/original/vfrQk5IPloGg1v9Rzbh2Eg3VGyM.jpg"
        });

        var movie13 = svc.AddMovie(new Movie
        {
            Title = "The Conjuring",
            Year = 2013,
            Director = "James Wan",
            Cast = "Patrick Wilson, Vera Farmiga, Ron Livingston",
            MovieDuration = 112,
            Composer = "Joseph Bishara",
            Genre = Genre.Horror,
            PlotSummary = "Paranormal investigators Ed and Lorraine Warren work to help a family terrorized by a dark presence in their farmhouse.",
            PosterUrl = "https://image.tmdb.org/t/p/original/wVYREutTvI2tmxr6ujrHT704wGF.jpg"
        });

        var movie14 = svc.AddMovie(new Movie
        {
            Title = "The Lord of the Rings: The Fellowship of the Ring",
            Year = 2001,
            Director = "Peter Jackson",
            Cast = "Elijah Wood, Ian McKellen, Orlando Bloom",
            MovieDuration = 178,
            Composer = "Howard Shore",
            Genre = Genre.Adventure,
            PlotSummary = "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.",
            PosterUrl = "https://image.tmdb.org/t/p/original/6oom5QYQ2yQTMJIbnvbkBL9cHo6.jpg"
        });

        var movie15 = svc.AddMovie(new Movie
        {
            Title = "The Terminal",
            Year = 2004,
            Director = "Steven Spielberg",
            Cast = "Tom Hanks, Catherine Zeta-Jones, Stanley Tucci",
            MovieDuration = 128,
            Composer = "John Williams",
            Genre = Genre.Comedy,
            PlotSummary = "An Eastern European tourist unexpectedly finds himself stranded in JFK airport, and must take up temporary residence there.",
            PosterUrl = "https://image.tmdb.org/t/p/original/pXNomqKcKXAQbuWxehb2N3XFKfn.jpg"
        });


        // generate reviews

        var movie1r1 = svc.CreateReview(new Review
        {
            MovieId = movie1.Id,
            CreatedOn = new DateTime(2024, 3, 29),
            Rating = 4,
            Statement = "Loved this movie. Action packed and is a must see for all ages, especially for those seeking an immersive experience. James Cameron provided a cinematic masterpiece unlike anything I had seen before."
        });

        var movie1r2 = svc.CreateReview(new Review
        {
            MovieId = movie1.Id,
            CreatedOn = new DateTime(2024, 3, 20),
            Rating = 5,
            Statement = "As someone who isn't a 'movie buff' this is one of the best movies I have seen. Visually impressive, incredible acting and the soudtrack pulls you into the world of Pandora. A must see for the whole family."
        });

        var movie2r1 = svc.CreateReview(new Review
        {
            MovieId = movie2.Id,
            CreatedOn = new DateTime(2024, 3, 20),
            Rating = 5,
            Statement = "Matt Damon as astronaut Mark Watney is captivating, often at times having some humerous one liners cutting through the suspense of this action packed science-fiction movie. I was kept at the edge of my seat throughout and has become one of my top 5 movies of all time"
        });

        var movie3r1 = svc.CreateReview(new Review
        {
            MovieId = movie3.Id,
            CreatedOn = new DateTime(2024, 4, 01),
            Rating = 4,
            Statement = "Enchanting animation following a cute love story, friendships building and a character learning to believe in herself. Very easy to watch, family friendly. A good watch."
        });

        var movie4r1 = svc.CreateReview(new Review
        {
            MovieId = movie4.Id,
            CreatedOn = new DateTime(2024, 3, 15),
            Rating = 3,
            Statement = "Thrilling action movie, with good acting from the cast. The plot is fairly predictable "
        });
        var movie4r2 = svc.CreateReview(new Review
        {
            MovieId = movie4.Id,
            CreatedOn = new DateTime(2024, 3, 29),
            Rating = 5,
            Statement = "I really emjoy this movie, such a cult classic! So much action and suspense with beatiful imagery throughout."
        });

        var movie5r1 = svc.CreateReview(new Review
        {
            MovieId = movie5.Id,
            CreatedOn = new DateTime(2024, 3, 29),
            Rating = 4,
            Statement = "Not just a fun comedy movie, but covers some really important topics. Ryan Gosling as Ken was surprisingly a great performance and his musical performance was funny and brilliant!"
        });

        var movie6r1 = svc.CreateReview(new Review
        {
            MovieId = movie6.Id,
            CreatedOn = new DateTime(2024, 3, 29),
            Rating = 4,
            Statement = "Very funny movie. Comedic genius of simon pegg and nick frost strikes again!"
        });

        var movie7r1 = svc.CreateReview(new Review
        {
            MovieId = movie7.Id,
            CreatedOn = new DateTime(2024, 3, 29),
            Rating = 5,
            Statement = "Such a magical story that can be enjoyed by young and old. Such a joyful movie and sets up the rest of the sequels perfectly."
        });

        var movie8r1 = svc.CreateReview(new Review
        {
            MovieId = movie8.Id,
            CreatedOn = new DateTime(2024, 3, 29),
            Rating = 3,
            Statement = "The final instalment to Daniel Craig's James Bond is a perfect end to his run as the spy character. The story was a little slow and the villan not as big as other movies, but overall a good end to the franchise."
        });

        var movie9r1 = svc.CreateReview(new Review
        {
            MovieId = movie9.Id,
            CreatedOn = new DateTime(2024, 3, 29),
            Rating = 3,
            Statement = "monsters inc"
        });

        var movie10r1 = svc.CreateReview(new Review
        {
            MovieId = movie10.Id,
            CreatedOn = new DateTime(2024, 1, 1),
            Rating = 4,
            Statement = "One of my favourite movies of all time, so funny and creative. Love the music and could watch it again and again!"
        });


    }




}


