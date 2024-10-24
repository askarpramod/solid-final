using MovieStudio.Thirdparty;
namespace MovieStudio.Interfaces
{
    public interface IEmployeeFunctionality
    {
        void Pay(StudioEmployee person, IFinanceService financeService);

        // Actor job
        bool Act();

        // CameraMan job
        bool Shoot();               
       
        // Recruiter job
        StudioEmployee Hire(string name, string personType);

    }
}
