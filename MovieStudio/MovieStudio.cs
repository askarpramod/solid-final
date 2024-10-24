using MovieStudio.Finance;
using MovieStudio.Movie;
using MovieStudio.Staff;
using MovieStudio.Thirdparty;
using MovieStudio.Thirdparty.Exceptions;
using System;
using System.Linq;

namespace MovieStudio
{
    public class MovieStudio
    {
        private static readonly long INITIAL_BUDGET = 1000000L;
        private static readonly int POTENTIAL_RISK = 15; // percent

        private readonly IFinanceService _financeService;
        private readonly StaffingService _staffingService;
        private readonly ProductionService _productionService;

        public MovieStudio()
        {
            this._financeService = new MovieFinanceService();

            this._staffingService = new StaffingService();

            this._productionService = new ProductionService();

            PrintMovieArchiveStatistics(this._productionService.LoadMovieDatabase("film_archive.yaml"));
        }

        public Movie.Movie CreateMovie(string recruiterName, string accountantName, MovieDefinition movieDefinition)
        {
            Movie.Movie movie = new Movie.Movie(movieDefinition.MovieName, movieDefinition.MovieGenre, movieDefinition.MovieStaff);

            this._financeService.InitBudget(INITIAL_BUDGET + movieDefinition.Budget);

            this._staffingService.HireNewStaff(new StudioEmployee[] { new Recruiter(recruiterName), new Accountant(accountantName) });

            if (CanBeProduced(this._financeService.GetBudget(), movieDefinition.DaysInProduction, _staffingService))
            {

                this._staffingService.HireNewStaff(movieDefinition.MovieStaff);
                this._productionService.InitMovieProduction(movieDefinition.DaysInProduction);

                while (this._productionService.CanContinueProduction())
                {
                    // produce a movie
                    if (this._productionService.LightsCameraAction(this._staffingService))
                        _productionService.Progress();
                    movie.UpdateContent();
                    // pay salary to every member of a team
                    try
                    {
                        this._financeService.PaySalary(this._staffingService);
                    }
                    catch (BudgetIsOverException)
                    {
                        var value = (1 - _productionService.GetProgress() * 1.0 / movieDefinition.DaysInProduction) * 100;
                        Console.WriteLine($"Movie production failed. Budget is over. Current progress is {value}");
                        return movie;
                    }
                }

                movie.Success();
                PrintProducedMovieStatistics(movieDefinition);
                _productionService.MovieArchive.Add(movie);
            }
            else
            {
                throw new InsufficientBudgetException("Movie cannot be produced - budget is insufficient");
            }

            return movie;
        }

        private void PrintProducedMovieStatistics(MovieDefinition movieDefinition)
        {
            long budgetSpent = movieDefinition.Budget + INITIAL_BUDGET - this._financeService.GetBudget();
            Console.WriteLine($"Budget: {movieDefinition.Budget + INITIAL_BUDGET} initial, {budgetSpent} spent, {this._financeService.GetBudget()} economy\n");

            this._staffingService.Staff.ForEach(person =>
            {
                if (person is Actor)
                {
                    Console.WriteLine($"Actor: {person.Name}, earned money:{person.EarnedMoney}, salary: {person.Salary}\n");
                    return;
                }
                if (person is CameraMan)
                {
                    Console.WriteLine($"Cameraman: {person.Name}, earned money:{person.EarnedMoney}, salary: {person.Salary}\n");
                    return;
                }
                if (person is Accountant)
                {
                    Console.WriteLine($"Accountant: {person.Name}, earned money:{person.EarnedMoney}, salary: {person.Salary}\n");
                    return;
                }
                Console.WriteLine($"Recruiter: {person.Name}, earned money:{person.EarnedMoney}, salary: {person.Salary} \n");

            });
        }

        private void PrintMovieArchiveStatistics(MovieStatistics movieStatistics)
        {
            if (!movieStatistics.IsEmpty())
            {
                Console.WriteLine($"Total: {movieStatistics.TotalActors} actors, {movieStatistics.TotalCameramen} cameramen, superstars: {string.Join(", ", movieStatistics.Superstars)}\n");
            }
        }

        private bool CanBeProduced(long proposedBudget, int daysInProduction, StaffingService staffingService)
        {
            long estimatedBudget = staffingService.Staff.Select(employee => employee.Salary)
                .Sum(x => x * daysInProduction * (long)Math.Round(100.0 + POTENTIAL_RISK / 100.0));

            return proposedBudget >= estimatedBudget;
        }

    }
}
