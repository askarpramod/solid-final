using MovieStudio.Interfaces;
using MovieStudio.Thirdparty;

namespace MovieStudio.Staff
{
    public class Recruiter : StudioEmployee, IRecruiter
    {
        public Recruiter(string name) : base(name, JobSalary.RECRUITER) { }

        public StudioEmployee Hire(string name, string personType)
        {
            return EmployeeFactory.CreateEmployee(name, personType);
        }
    }
}
