using MovieStudio.Interfaces;
using MovieStudio.Staff;
using MovieStudio.Thirdparty;

namespace MovieStudio
{
    public class StaffManager : IStaffManager
    {
        private readonly IStaffHiringService _staffHiringService;

        public StaffManager(IStaffHiringService staffHiringService)
        {
            _staffHiringService = staffHiringService;
        }

        public void HireStaff(string recruiterName, string accountantName)
        {
            var recruiter = new Recruiter(recruiterName);
            var accountant = new Accountant(accountantName);
            _staffHiringService.HireNewStaff(new StudioEmployee[] { recruiter, accountant });
        }
    }
}
