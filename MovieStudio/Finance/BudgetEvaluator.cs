using MovieStudio.Interfaces;
using System.Linq;

namespace MovieStudio
{
    public class BudgetEvaluator : IBudgetEvaluator
    {
        private const int POTENTIAL_RISK = 15;
        private readonly IBudgetManager _budgetManager;
        private readonly IStaffManagementService _staffManagementService;

        public BudgetEvaluator(IBudgetManager budgetManager, IStaffManagementService staffManagementService)
        {
            _budgetManager = budgetManager;
            _staffManagementService = staffManagementService;
        }

        public bool CanBeProduced(MovieDefinition movieDefinition)
        {
            double estimatedBudget = EstimateBudget(movieDefinition.DaysInProduction);
            return _budgetManager.GetBudget() >= estimatedBudget;
        }

        private double EstimateBudget(int daysInProduction)
        {
            return _staffManagementService.Staff.Sum(employee =>
                employee.Salary * daysInProduction * (1 + POTENTIAL_RISK / 100.0));
        }
    }
}
