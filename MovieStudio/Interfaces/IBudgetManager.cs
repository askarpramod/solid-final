namespace MovieStudio.Interfaces
{
    public interface IBudgetManager
    {
        void DecreaseBudget(long amount);
        long GetBudget();
    }
}
