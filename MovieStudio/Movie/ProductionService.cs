using MovieStudio.Interfaces;
using MovieStudio.Movie;
using MovieStudio.Staff;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.Serialization;

namespace MovieStudio
{
    public class ProductionService : IProductionService
    {
        private readonly IMovieProductionSchedule _productionSchedule;
        public List<Movie.Movie> MovieArchive { get; private set; }

        public ProductionService(IMovieProductionSchedule productionSchedule)
        {
            MovieArchive = new List<Movie.Movie>();
            _productionSchedule = productionSchedule;
        }

        public virtual MovieStatistics LoadMovieDatabase(string fileName)
        {
            var movies = ReadArchivedMovie(fileName);
            AddMoviesToArchive(movies);
            return GetArchiveStatistics();
        }

        private List<Movie.Movie> ReadArchivedMovie(string filename)
        {
            var resource = ReadResourceFile(filename);
            var deserializer = new DeserializerBuilder().Build();
            return deserializer.Deserialize<List<Movie.Movie>>(resource);
        }

        private string ReadResourceFile(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = assembly.GetManifestResourceNames()
                    .Single(str => str.EndsWith(name));

            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private MovieStatistics GetArchiveStatistics()
        {
            var movieStatistics = new MovieStatistics(MovieArchive);
            foreach (var movie in MovieArchive)
            {
                string currentMovieGenre = movie.Genre.ToString();
                movieStatistics.MovieGenres.TryGetValue(currentMovieGenre, out int genreCount);
                movieStatistics.MovieGenres[currentMovieGenre] = genreCount + 1;

                movieStatistics.IncActorsCount(movie.Crew.FirstOrDefault(x => x.Key.Contains("Actor")).Value);
                movieStatistics.IncCameramenCount(movie.Crew.FirstOrDefault(x => x.Key.Contains("Cameraman")).Value);
                movieStatistics.AddSuperStars(movie.Superstars);
            }
            return movieStatistics;
        }

        private void AddMoviesToArchive(List<Movie.Movie> movies)
        {
            MovieArchive.AddRange(movies);
        }

        public void InitMovieProduction(int daysInProduction)
        {
            _productionSchedule.SetDays(daysInProduction);
        }

        public bool CanContinueProduction()
        {
            return _productionSchedule.DaysInProduction > 0;
        }

        public void Progress()
        {
            _productionSchedule.DecrementDays();
        }

        public bool LightsCameraAction(IStaffManagementService staffManagementService)
        {
            return staffManagementService.Staff.OfType<ICameraman>().All(cameraman => cameraman.Shoot());
        }

        public int GetProgress()
        {
            return _productionSchedule.DaysInProduction;
        }
    }
}
