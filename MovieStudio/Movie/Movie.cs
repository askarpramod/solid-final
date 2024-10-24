using MovieStudio.Staff.Team;
using MovieStudio.Thirdparty;
using System.Collections.Generic;

namespace MovieStudio.Movie
{
    public class BaseMovie
    {
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public int DaysInProduction { get; set; }
        public bool IsFinished { get; private set; }

        public BaseMovie(string name, Genre genre)
        {
            Name = name;
            Genre = genre;
            IsFinished = false;
            DaysInProduction = 0;
        }

        public void Success()
        {
            IsFinished = true;
        }

        public void UpdateContent()
        {
            DaysInProduction++;
        }

        public override string ToString()
        {
            return $"Movie {Name} [{Genre}], status: {(IsFinished ? "finished" : "in production")}, days in production: {DaysInProduction}";
        }
    }

    public class Movie : BaseMovie
    {
        public Dictionary<string, int> Crew { get; set; }
        public List<string> Superstars { get; set; }

        public Movie(string name, Genre genre, StudioStaff staff) : base(name, genre)
        {
            Crew = new Dictionary<string, int>();
            Superstars = new List<string>();
            SetCrewFromStaffCollection(staff);
        }

        public void SetCrewFromStaffCollection(StudioStaff crew)
        {
            Crew["Actor"] = crew.Actors.Count;
            Crew["Cameraman"] = crew.Cameramen.Count;

            foreach (var actor in crew.Actors)
            {
                if (actor.IsSuperStar())
                {
                    Superstars.Add(actor.Name);
                }
            }
        }
    }
}
