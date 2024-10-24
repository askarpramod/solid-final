using MovieStudio.Interfaces;
using MovieStudio.Thirdparty;
using System;

namespace MovieStudio.Staff
{
    public class CameraMan : StudioEmployee, IEmployeeFunctionality
    {
        public CameraMan(string name) : base(name, JobSalary.CAMERA_MAN)
        {

        }

        public bool Shoot()
        {
            return new Random().NextDouble() > 0.04;
        }

        public StudioEmployee Hire(string name, string personType)
        {
            return null;
        }

        public void Pay(StudioEmployee person, IFinanceService financeService)
        {

        }

        public bool Act()
        {
            return true;
        }
    }
}
