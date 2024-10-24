using System.Collections.Generic;

namespace MovieStudio.Movie
{
    public class MovieStatistics
    {
        public Dictionary<string, int> MovieGenres { get; }
        public int TotalActors { get; set; }
        public int TotalCameramen { get; set; }
        public List<string> Superstars { get; set; }


        public MovieStatistics(List<Movie> movieArchive)
        {
            MovieGenres = new Dictionary<string, int>();
            TotalActors = 0;
            TotalCameramen = 0;
            Superstars = new List<string>();
        }
        public bool IsEmpty()
        {
            return TotalActors + TotalCameramen + Superstars.Count > 0;
        }
        public void IncActorsCount(int actorsCount)
        {
            TotalActors += actorsCount;
        }

        public void IncCameramenCount(int cameramenCount)
        {
            TotalCameramen += cameramenCount;
        }
        public void AddSuperStars(List<string> superstars)
        {
            this.Superstars.AddRange(superstars);
        }
    }
}
