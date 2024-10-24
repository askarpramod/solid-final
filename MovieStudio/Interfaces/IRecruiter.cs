using MovieStudio.Thirdparty;

namespace MovieStudio.Interfaces
{
    public interface IRecruiter
    {
        StudioEmployee Hire(string name, string personType);
    }
}
