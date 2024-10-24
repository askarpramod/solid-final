using MovieStudio.Interfaces;

namespace MovieStudio.Movie
{
    public class MovieProductionSchedule : IMovieProductionSchedule
    {
        public int DaysInProduction { get; private set; }

        public MovieProductionSchedule(int daysInProduction)
        {
            DaysInProduction = daysInProduction;
        }

        public void SetDays(int days)
        {
            DaysInProduction = days;
        }

        public void DecrementDays()
        {
            if (DaysInProduction > 0)
            {
                DaysInProduction--;
            }
        }
    }
}
