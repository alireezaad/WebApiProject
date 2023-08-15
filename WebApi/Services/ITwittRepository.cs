

using WebApi.Model;
using WebApi.Model.ViewModel;

namespace WebApi.Services
{
    public interface ITwittRepsitory
    {
        IEnumerable<Twitt> GetAll();
        Twitt Get(int id);
        void Add(TwittViewModel twitt);
        void Update(TwittViewModel twitt);
        void Delete(TwittViewModel twitt);
        void Delete(int id);
        void SaveChanges();

    }
}
