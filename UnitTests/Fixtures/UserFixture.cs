using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Model;

namespace UnitTests.Fixtures
{
    public class UserFixture
    {
        public static List<User> GetTesUserList() => new()
        {
            new User
            {
                Id = 1,
                Username = "Alireezaad",
                Password = "password",
                Email = "Alireezaad@gmail.com",
            },
            new User
            {
                Id = 2,
                Username = "Alibabapour",
                Password = "password",
                Email = "Babapour@gmail.com"
            },
            new User
            {
                Id = 3,
                Username = "MasoudMK",
                Password = "password",
                Email = "Masoud@Yahoo.com"
            }
        };
    }
}
