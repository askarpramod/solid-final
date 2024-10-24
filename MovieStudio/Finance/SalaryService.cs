using MovieStudio.Interfaces;
using MovieStudio.Staff;
using MovieStudio.Thirdparty;
using System.Linq;

namespace MovieStudio.Finance
{
    public class SalaryService : ISalaryService
    {
        public void PaySalary(IStaffManagementService staffManagementService, IBudgetManager budgetManager , IBudgetInitializer budgetInitializer)
        {
            var accountant = staffManagementService.Staff.OfType<IAccountant>().FirstOrDefault();

            if (accountant != null)
            {
                foreach (var person in staffManagementService.Staff.OfType<StudioEmployee>())
                {
                    accountant.Pay(person, budgetInitializer,budgetManager);
                }
            }
        }
    }
}
