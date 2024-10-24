namespace MovieStudio.Interfaces
{
    public interface IMovieProductionSchedule
    {
        int DaysInProduction { get; }
        void SetDays(int days);
        void DecrementDays();
    }
}
