using MovieStudio.Interfaces;
using MovieStudio.Staff;
using MovieStudio.Thirdparty;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.Serialization;

namespace MovieStudio.Movie
{
    public class ProductionService
    {
        private MovieProductionSchedule _productionSchedule;
        public List<Movie> MovieArchive { get; }

        public ProductionService()
        {
            MovieArchive = new List<Movie>();
        }
        public virtual MovieStatistics LoadMovieDatabase(string fileName)
        {

            var movies = ReadArchivedMovie(fileName);

            try
            {
                AddMoviesToArchive(movies);
                return GetArchiveStatistics();
            }
            catch (IOException)
            {
                Console.WriteLine("Movie archive is damaged or empty");
                return new MovieStatistics(new List<Movie>());
            }

        }

        private List<Movie> ReadArchivedMovie(string filename)
        {
            var resource = ReadResourceFile(filename);

            var deserializer = new DeserializerBuilder().Build();

            var movie = deserializer.Deserialize<List<Movie>>(resource);

            return movie;
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
            MovieStatistics movieStatistics = new MovieStatistics(MovieArchive);

            Console.WriteLine($"Movies in archive: { MovieArchive.Count}");
            MovieArchive.ForEach(movie =>
            {
                string currentMovieGenre = movie.Genre.ToString();
                movieStatistics.MovieGenres.Add(
                        currentMovieGenre,
                        movieStatistics.MovieGenres.FirstOrDefault(x => x.Key.Contains(currentMovieGenre)).Value);

            });
            MovieArchive.ForEach(movie =>
            {
                movieStatistics.IncActorsCount(movie.Crew.FirstOrDefault(x => x.Key.Contains("Actor")).Value);
                movieStatistics.IncCameramenCount(movie.Crew.FirstOrDefault(x => x.Key.Contains("Cameraman")).Value);
                movieStatistics.AddSuperStars(movie.Superstars);
            });
            return movieStatistics;
        }

        private List<Movie> AddMoviesToArchive(List<Movie> movies)
        {
            MovieArchive.AddRange(movies);
            return MovieArchive;
        }

        public void InitMovieProduction(int daysInProduction)
        {
            this._productionSchedule = new MovieProductionSchedule(daysInProduction);
        }

        public bool CanContinueProduction()
        {
            return _productionSchedule.DaysInProduction > 0;
        }

        public void Progress()
        {
            _productionSchedule.DaysInProduction = _productionSchedule.DaysInProduction - 1;
        }

        public bool LightsCameraAction(StaffingService staffingService)
        {
            List<StudioEmployee> studioEmployees = staffingService.Staff;

            var allActorsPerformed = studioEmployees.Where(staff => staff is Actor).All(actor => ((IEmployeeFunctionality)actor).Act() && ((IEmployeeFunctionality)actor).Shoot());
            var allCameraMenPerformed = studioEmployees.Where(staff => staff is CameraMan).All(cameraman => ((IEmployeeFunctionality)cameraman).Act() && ((IEmployeeFunctionality)cameraman).Shoot());
            
            return allActorsPerformed && allCameraMenPerformed;
        }

        public int GetProgress()
        {
            return _productionSchedule.DaysInProduction;
        }

    }
}
