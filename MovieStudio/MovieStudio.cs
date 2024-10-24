using MovieStudio.Interfaces;
using MovieStudio.Movie;
using MovieStudio.Staff.Team;
using MovieStudio.Thirdparty.Exceptions;

namespace MovieStudio
{
    public class MovieStudio
    {
        private readonly IBudgetInitializer _budgetInitializer; // Injected BudgetInitializer
        private readonly IBudgetEvaluator _budgetEvaluator;     // Injected BudgetEvaluator
        private readonly IStaffHiringService _staffHiringService;
        private readonly IProductionService _productionService;
        private readonly IRecruiter _recruiter;
        private readonly IAccountant _accountant;
        private readonly IStaffManager _staffManager;

        public MovieStudio(
            IBudgetInitializer budgetInitializer,
            IBudgetEvaluator budgetEvaluator,
            IStaffHiringService staffHiringService,
            IProductionService productionService,
            IRecruiter recruiter,
            IAccountant accountant,
            IStaffManager staffManager)
        {
            _budgetInitializer = budgetInitializer;
            _budgetEvaluator = budgetEvaluator;
            _staffHiringService = staffHiringService;
            _productionService = productionService;
            _recruiter = recruiter;
            _accountant = accountant;
            _staffManager = staffManager;
        }

        public Movie.Movie CreateMovie(string recruiterName, string accountantName, MovieDefinition movieDefinition)
        {
            _budgetInitializer.InitializeBudget(movieDefinition.Budget);
            _staffManager.HireStaff(recruiterName, accountantName);

            if (!_budgetEvaluator.CanBeProduced(movieDefinition))
                throw new InsufficientBudgetException("Movie cannot be produced - budget is insufficient");

            StartProduction(movieDefinition);
            return CompleteMovie(movieDefinition);
        }

        private void StartProduction(MovieDefinition movieDefinition)
        {
            _productionService.InitMovieProduction(movieDefinition.DaysInProduction);
        }

        private Movie.Movie CompleteMovie(MovieDefinition movieDefinition)
        {
            // Logic to mark movie as complete
            return new Movie.Movie(movieDefinition.MovieName, movieDefinition.MovieGenre, new StudioStaff());
        }
    }
}
