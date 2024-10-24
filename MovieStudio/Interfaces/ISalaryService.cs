using MovieStudio.Staff;

namespace MovieStudio.Interfaces
{
    public interface ISalaryService
    {
        void PaySalary(IStaffManagementService staffManagementService, IBudgetManager budgetManager, IBudgetInitializer budgetInitializer);
    }
}
