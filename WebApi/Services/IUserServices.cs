

using WebApi.Model;
using WebApi.Model.DTOs;

namespace WebApi.Services
{
    public interface IUserServices
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(int id);
        void Add(UserDTO viewModel);
        void Update(UserDTO viewModel);
        void Delete(UserDTO viewModel);
        void Delete(int id);
        void SaveChanges();

    }
}
