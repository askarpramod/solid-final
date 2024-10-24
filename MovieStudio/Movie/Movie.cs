using MovieStudio.Staff;
using MovieStudio.Staff.Team;
using MovieStudio.Thirdparty;
using System.Collections.Generic;

namespace MovieStudio.Movie
{
    public class Movie
    {      
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public int DaysInProduction { get; set; }
        public bool IsFinished { get; private set; }
        public Dictionary<string, int> Crew { get; set; }
        public List<string> Superstars { get; set; }

        public Movie()
        {
        }

        public Movie(string name, Genre genre, StudioStaff staff)
        {
            this.Name = name;
            this.Genre = genre;
            this.IsFinished = false;
            this.DaysInProduction = 0;
            this.Crew = new Dictionary<string, int>();
            this.Superstars = new List<string>();
            this.SetCrewFromStaffCollection(staff);
        }
        public void SetCrewFromStaffCollection(StudioStaff crew)
        {
            this.Crew.Add("Actor", crew.Actors.Count);
            this.Crew.Add("Cameraman", crew.Cameramen.Count);

            foreach (Actor actor in crew.Actors)
            {
                if (actor.IsSuperStar())
                {
                    Superstars.Add(actor.Name);
                }
            }
        }

        public void Success()
        {
            this.IsFinished = true;
        }

        public void UpdateContent()
        {
            this.DaysInProduction++;
        }


        public override string ToString()
        {
            return string.Format($"Movie {Name} [{Genre}], status: {(IsFinished ? "finished" : "in production")}, days in production:{DaysInProduction}");
        }


    }
}
