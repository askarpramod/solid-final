using MovieStudio.Staff;

namespace MovieStudio.Thirdparty
{
    public interface IFinanceService
    {
        void InitBudget(long initialSum);

        void DecreaseBudget(long paidSum);

        void PaySalary(StaffingService staffingService);

        long GetBudget();

    }
}
