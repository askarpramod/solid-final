using MovieStudio.Staff.Team;
using MovieStudio.Thirdparty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStudio.Interfaces
{
    public interface IStaffHiringService
    {
        void HireNewStaff(StudioEmployee[] persons);
        void HireNewStaff(StudioStaff movieDefinition);
    }
}
