using System;
using MovieStudio.Interfaces;

namespace MovieStudio.Finance
{
    public class MovieBudget : IBudget
    {
        private long _budgetMoney;

        public long BudgetMoney => _budgetMoney;

        public MovieBudget(long budgetMoney)
        {
            if (budgetMoney < 0)
                throw new ArgumentException("Initial budget cannot be negative.");

            _budgetMoney = budgetMoney;
        }

        public void Decrease(long amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Decrease amount must be positive.");

            if (amount <= _budgetMoney)
            {
                _budgetMoney -= amount;
            }
            else
            {
                throw new InvalidOperationException("Insufficient budget.");
            }
        }
    }
}
