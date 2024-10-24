using MovieStudio.Finance;
using MovieStudio.Interfaces;
using System;

namespace MovieStudio
{
    public class BudgetInitializer : IBudgetInitializer
    {
        private readonly long _initialBudget;
        private readonly IBudgetManager _budgetManager;
        private MovieBudget _movieBudget;

        public BudgetInitializer(IBudgetManager budgetManager, long initialBudget = 1000000L)
        {
            _budgetManager = budgetManager;
            _initialBudget = initialBudget;
        }

        public void InitializeBudget(long additionalBudget)
        {
            if (additionalBudget < 0)
                throw new ArgumentException("Initial sum cannot be negative.");

            _movieBudget = new MovieBudget(_initialBudget + additionalBudget);
        }
    }
}
