namespace MovieStudio.Interfaces
{
    public interface IBudget
    {
        long BudgetMoney { get; }
        void Decrease(long amount);
    }
}
