using MovieStudio.Interfaces;
using System;

namespace MovieStudio.Finance
{
    public class BudgetManager : IBudgetManager
    {
        private IBudget _budget;

        public BudgetManager(IBudget budget)
        {
            _budget = budget ?? throw new ArgumentNullException(nameof(budget));
        }

        public void DecreaseBudget(long amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Decrease amount must be positive.");

            _budget.Decrease(amount);
        }

        public long GetBudget() => _budget.BudgetMoney;
    }
}
