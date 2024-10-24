using MovieStudio.Interfaces;
using MovieStudio.Staff.Team;
using MovieStudio.Thirdparty;
using System.Collections.Generic;

namespace MovieStudio.Staff
{
    public class StaffingService : IStaffHiringService, IStaffManagementService
    {
        public List<StudioEmployee> Staff { get; }

        public StaffingService()
        {
            Staff = new List<StudioEmployee>();
        }

        public void HireNewStaff(StudioEmployee[] persons)
        {
            Staff.AddRange(persons);
        }

        public void HireNewStaff(StudioStaff movieDefinition)
        {
            Staff.AddRange(movieDefinition.Actors);
            Staff.AddRange(movieDefinition.Cameramen);
        }
    }
}
