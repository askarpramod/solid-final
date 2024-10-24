using MovieStudio.Thirdparty;
using MovieStudio.Thirdparty.Exceptions;

namespace MovieStudio.Staff
{
    public static class EmployeeFactory
    {
        public static StudioEmployee CreateEmployee(string name, string personType)
        {
            switch (personType.ToLowerInvariant())
            {
                case "accountant":
                    return new Accountant(name);
                case "cameraman":
                    return new CameraMan(name);
                case "superstar":
                    return new Actor(name, true);
                case "actor":
                    return new Actor(name, false);
                default:
                    throw new NoSuchProfessionException(personType);
            }
        }
    }
}
