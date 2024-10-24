using MovieStudio.Staff;
using MovieStudio.Thirdparty;
using System.Linq;

namespace MovieStudio.Finance
{
    public class MovieFinanceService : IFinanceService
    {
        private MovieBudget _movieBudget;

        public void DecreaseBudget(long paidSum)
        {
            _movieBudget.BudgetMoney = _movieBudget.BudgetMoney - paidSum;
        }
        public void InitBudget(long initialSum)
        {
            this._movieBudget = new MovieBudget(initialSum);
        }

        public void PaySalary(StaffingService staffingService)
        {
            Accountant accountant = (Accountant)staffingService.Staff.Where(person => person is Accountant).FirstOrDefault();

            if ((accountant != null))
            {
                foreach (StudioEmployee person in staffingService.Staff)
                {
                    accountant.Pay(person, this);
                }
            }
        }

        public long GetBudget()
        {
            return _movieBudget.BudgetMoney;
        }
    }
}
