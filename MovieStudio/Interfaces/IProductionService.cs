using MovieStudio.Movie;
using System.Collections.Generic;

namespace MovieStudio.Interfaces
{
    public interface IProductionService
    {
        List<Movie.Movie> MovieArchive { get; }
        MovieStatistics LoadMovieDatabase(string fileName);
        void InitMovieProduction(int daysInProduction);
        bool CanContinueProduction();
        void Progress();
        bool LightsCameraAction(IStaffManagementService staffManagementService);
    }
}
