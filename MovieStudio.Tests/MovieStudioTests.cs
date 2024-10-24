using Xunit;
using MovieStudio;
using MovieStudio.Staff.Team;
using MovieStudio.Staff;
using MovieStudio.Movie;
using MovieStudio.Thirdparty;
using MovieStudio.Thirdparty.Exceptions;

namespace MovieStudioTest
{
    public class MovieStudioTests
    {
        static MovieStudio.MovieStudio movieStudio;
        static string recruiterForTest = "Andrew Carnegie";
        static string accountantForTest = "William Welch Deloitte";

        public MovieStudioTests()
        {
            movieStudio = new MovieStudio.MovieStudio();
        }

        [Fact]
        public void CreateMovie_Titanic_WithValidMovieDefinition_ReturnsTrue_OnSuccessfulCompletion()
        {
            int PRODUCTION_SCHEDULE = 80;
            StudioStaff staff = new StudioStaff(
                    new Actor[]{
                        new Actor("Leo DiCaprio", true), new Actor("Kate Winslet", true),
                        new Actor("Billy Zane", false), new Actor("Kathy Bates", false),
                        new Actor("Frances Fisher", false), new Actor("Bernard Hill", false),
                        new Actor("Jonathan Hyde", false), new Actor("Danny Nucci", false),
                        new Actor("David Warner", false), new Actor("Bill Paxton", false)
                    },
                    new CameraMan[]{
                        new CameraMan("Guy Norman Bee"),
                        new CameraMan("Marcis Cole"),
                        new CameraMan("Tony Guerin")
                    }
            );
            long budget = 1600000000L;
            MovieDefinition titanicMovie = new MovieDefinition(budget, "Titanic", Genre.DRAMA,
                    staff, PRODUCTION_SCHEDULE);
            Movie movie = movieStudio.CreateMovie(recruiterForTest, accountantForTest, titanicMovie);

            Assert.True(movie.IsFinished);
        }

        [Fact]
        public void CreateMovie_StarWars_WithValidMovieDefinition_ReturnsTrue_OnSuccessfulCompletion()
        {
            int PRODUCTION_SCHEDULE = 80;
            StudioStaff staff = new StudioStaff(
                    new Actor[]{
                        new Actor("Mark Hamill", true), new Actor("Harrison Ford", true),
                        new Actor("Carrie Fischer", false), new Actor("Billy Dee Williams", false),
                        new Actor("Anthony Daniels", false), new Actor("David Prowse", false),
                        new Actor("Peter Mayhew", false)
                    },
                    new CameraMan[]{
                        new CameraMan("John Campbell"),
                        new CameraMan("Bill Neil")
                    }
            );
            long budget = 6000000000L;
            MovieDefinition starWars3Movie = new MovieDefinition(budget, "Star Wars: Episode VI � Return of the Jedi",
                    Genre.SCIFI, staff, PRODUCTION_SCHEDULE);
            Movie movie = movieStudio.CreateMovie(recruiterForTest, accountantForTest, starWars3Movie);
            Assert.True(movie.IsFinished);
        }

        [Fact]
        public void CreateMovie_ShouldCreateEmptyMovie_WithEmptyMovieDefinition_ReturnsTrue()
        {
            int PRODUCTION_SCHEDULE = 1;
            StudioStaff staff = new StudioStaff(
                    new Actor[] { },
                    new CameraMan[] { }
            );
            long budget = 1L;
            MovieDefinition emptyMovie = new MovieDefinition(budget, "Noname", Genre.COMEDY,
                    staff, PRODUCTION_SCHEDULE);
            Movie movie = movieStudio.CreateMovie(recruiterForTest, accountantForTest, emptyMovie);
            Assert.True(movie.IsFinished);
        }


        [Fact]
        public void CreateMovie_WhenBudgetExceeds_ReturnsFalse_OnUnsuccessfulCompletion()
        {
            int PRODUCTION_SCHEDULE = 200;
            StudioStaff staff = new StudioStaff(
                    new Actor[]{
                        new Actor("Taylor Kitsch", false),
                        new Actor("Lynn Collins", false),
                        new Actor("Samantha Morton", false),
                        new Actor("Mark Strong", true),
                        new Actor("Ciar�n Hinds", false),
                        new Actor("Dominic West", false),
                        new Actor("James Purefoy", false),
                        new Actor("Willem Dafoe", true)
                    },
                    new CameraMan[]{
                        new CameraMan("Carver Christians"),
                        new CameraMan("Scott Bourke"),
                        new CameraMan("Quentin Herriot"),
                        new CameraMan("Brandon Wyman")
                    }
            );
            long budget = 100000000L;
            MovieDefinition johnCarterMovie = new MovieDefinition(budget, "John Carter", Genre.FANTASY,
                    staff, PRODUCTION_SCHEDULE);
            Movie movie = movieStudio.CreateMovie(recruiterForTest, accountantForTest, johnCarterMovie);
            Assert.False(movie.IsFinished);
        }

        [Fact]
        public void CreateMovie_WhenInsufficientBudget_Throws_InsufficientBudgetException()
        {
            var message = "Movie cannot be produced - budget is insufficient";

            int PRODUCTION_SCHEDULE = 250;
            StudioStaff staff = new StudioStaff(
                    new Actor[]{
                        new Actor("Channing Tatum", true),
                        new Actor("Taylor Kitsch", true),
                        new Actor("Keanu Reeves", true),
                        new Actor("Josh Holloway", true),
                        new Actor("L�a Seydoux", true),
                        new Actor("Hugh Jackman", true),
                        new Actor("Rebecca Ferguson", false),
                        new Actor("Abbey Leee", true)
                    },
                    new CameraMan[]{
                        new CameraMan("Carver Christians"),
                        new CameraMan("Scott Bourke"),
                        new CameraMan("Quentin Herriot"),
                        new CameraMan("Brandon Wyman")
                    }
            );
            long budget = 100000000L;
            MovieDefinition johnCarterMovie = new MovieDefinition(budget, "Gambit", Genre.FANTASY,
                    staff, PRODUCTION_SCHEDULE);
            var exception = Assert.Throws<InsufficientBudgetException>(() => movieStudio.CreateMovie(recruiterForTest, accountantForTest, johnCarterMovie));
            Assert.Equal(message, exception.Message);

        }
    }
}
