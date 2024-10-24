using Xunit;
using MovieStudio;
using MovieStudio.Staff.Team;
using MovieStudio.Staff;
using MovieStudio.Movie;
using MovieStudio.Thirdparty;
using MovieStudio.Thirdparty.Exceptions;
using MovieStudio.Interfaces;
using Moq;

namespace MovieStudioTest
{
    public class MovieStudioTests
    {
        private readonly Mock<IBudgetManager> _mockBudgetManager;
        private readonly Mock<IStaffManagementService> _mockStaffManagementService;
        private readonly Mock<IStaffHiringService> _mockStaffHiringService;
        private readonly Mock<IMovieProductionSchedule> _mockProductionSchedule;

        private readonly BudgetInitializer _budgetInitializer;
        private readonly BudgetEvaluator _budgetEvaluator;
        private readonly StaffingService _staffingService;
        private readonly ProductionService _productionService;
        private readonly Recruiter _recruiter;
        private readonly Accountant _accountant;
        private readonly StaffManager _staffManager;
        private readonly MovieStudio.MovieStudio _movieStudio;

        public MovieStudioTests()
        {
            // Mock external dependencies
            _mockBudgetManager = new Mock<IBudgetManager>();
            _mockStaffManagementService = new Mock<IStaffManagementService>();
            _mockStaffHiringService = new Mock<IStaffHiringService>();
            _mockProductionSchedule = new Mock<IMovieProductionSchedule>();

            // Initialize concrete classes with mocked dependencies
            _budgetInitializer = new BudgetInitializer(_mockBudgetManager.Object);
            _budgetEvaluator = new BudgetEvaluator(_mockBudgetManager.Object, _mockStaffManagementService.Object);
            _staffingService = new StaffingService();
            _productionService = new ProductionService(_mockProductionSchedule.Object);
            _recruiter = new Recruiter("Recruiter1");
            _accountant = new Accountant("Accountant1");
            _staffManager = new StaffManager(_mockStaffHiringService.Object);

            // Initialize the class under test (MovieStudio)
            _movieStudio = new MovieStudio.MovieStudio(
                _budgetInitializer,
                _budgetEvaluator,
                _mockStaffHiringService.Object,  // No constructor params, so mock
                _productionService,
                _recruiter,
                _accountant,
                _staffManager
            );
        }
        /*
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
            Movie movie = _movieStudio.CreateMovie(_recruiter.Name, _accountant.Name, titanicMovie);

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
            Movie movie = _movieStudio.CreateMovie(_recruiter.Name, _accountant.Name, starWars3Movie);
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
            Movie movie = _movieStudio.CreateMovie(_recruiter.Name, _accountant.Name, emptyMovie);
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
            Movie movie = _movieStudio.CreateMovie(_recruiter.Name, _accountant.Name, johnCarterMovie);
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
            var exception = Assert.Throws<InsufficientBudgetException>(() => _movieStudio.CreateMovie(_recruiter.Name, _accountant.Name, johnCarterMovie));
            Assert.Equal(message, exception.Message);

        }
        */
    }
}
