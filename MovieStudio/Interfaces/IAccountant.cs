using MovieStudio.Thirdparty;

namespace MovieStudio.Interfaces
{
    public interface IAccountant
    {
        void Pay(StudioEmployee person, IBudgetInitializer budgetInitializer, IBudgetManager budgetManager);
    }
}
