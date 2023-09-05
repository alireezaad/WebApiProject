//#define Disable_Ctor

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using WebApi.Config;
using WebApi.Model;
using WebApi.Model.DBContext;
using WebApi.Model.DTOs;

namespace WebApi.Services
{

    public class UserServices : IDisposable, IUserServices
    {
        private readonly MyDBContext db;
        private readonly HttpClient client;
        private readonly UserApiOptions _config;

        public UserServices(MyDBContext db)
        {
            this.db = db;

        }
        /// <summary>
        /// Using For unit test only
        /// </summary>
        /// <param name="client"></param>
        /// <param name="apiconfig"></param>
        /// 

#if !Disable_Ctor
        public UserServices(MyDBContext db, HttpClient client, IOptions<UserApiOptions> apiconfig)
        {
            this.db = db;
            this.client = client;
            this._config = apiconfig.Value;
        }

#endif
        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                //var UserResponse = await this.client.GetAsync(_config.Endpoint);
                //if (UserResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                //    return new List<User>();

                //var content = UserResponse.Content;
                //var list = await content.ReadFromJsonAsync<List<User>>();
                //return list.ToList();

                var myTask = Task.Run(() => db.users.ToListAsync());
                return await myTask;

            }
            catch (Exception) { throw; }
        }
        public async Task<User> Get(int id)
        {
            try
            {
                var myTask = Task.Run(() => db.users.FirstOrDefaultAsync(t => t.Id == id));
                return await myTask;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Add(UserDTO userDTO)
        {
            try
            {
                var user = new User()
                {
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Password = userDTO.Password
                };
                var myTask = Task.Run(() => db.users.AddAsync(user));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(UserDTO userDTO)
        {
            try
            {
                var user = new User()
                {
                    Id = userDTO.Id,
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Password = userDTO.Password
                };
                db.users.Update(user);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async void Delete(int id)
        {
            try
            {
                var user = await Get(id);
                db.users.Remove(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(UserDTO userDTO)
        {
            try
            {
                var user = new User()
                {
                    Id = userDTO.Id,
                    Username = userDTO.Email,
                    Password = userDTO.Password,
                    Email = userDTO.Email
                };
                db.users.Remove(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            if (db != null)
            {
                GC.SuppressFinalize(db);
            }
        }
    }
}
