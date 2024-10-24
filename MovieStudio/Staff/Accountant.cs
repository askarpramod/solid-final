using MovieStudio.Finance;
using MovieStudio.Interfaces;
using MovieStudio.Thirdparty;
using MovieStudio.Thirdparty.Exceptions;

namespace MovieStudio.Staff
{
    public class Accountant : StudioEmployee, IAccountant
    {
        public Accountant(string name) : base(name, JobSalary.ACCOUNTANT) { }

        public void Pay(StudioEmployee person, IBudgetInitializer budgetInitializer,IBudgetManager budgetManager)
        {
            long salary = person.Salary;
            person.PaySalary(salary);
            if ((budgetManager.GetBudget() - salary) < 0)
            {
                budgetInitializer.InitializeBudget(0);
                throw new BudgetIsOverException();
            }
            budgetManager.DecreaseBudget(salary);
        }
    }
}
