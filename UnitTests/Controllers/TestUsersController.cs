using Castle.Components.DictionaryAdapter.Xml;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Fixtures;
using UnitTests.Helper;
using WebApi.Config;
using WebApi.Controllers;
using WebApi.Model;
using WebApi.Model.DBContext;
using WebApi.Services;
using Xunit;
using static System.Net.WebRequestMethods;
using FakeItEasy;
namespace UnitTests.Controllers
{
    public class TestUsersController
    {
        [Fact]
        public async void Get_OnSuccessReturnStatusCode200()
        {
            //arrange 
            //var moq = new Mock<IUserServices>();
            //var lictMoq = new Mock<IEnumerable<User>>();
            //moq.Setup(x => x.GetAll()).ReturnsAsync(lictMoq.Object);

            var serviceMock = A.Fake<IUserServices>();
            var outputMock = A.Fake<List<User>>();
            A.CallTo(() => serviceMock.GetAll()).Returns(outputMock);
            var controller = new UsersController(serviceMock);

            //act 
            var result = (OkObjectResult)await controller.GetAll();
            //var listT = result.Value as List<User>; // ?? new List<Twitt>();


            // assert 
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<List<User>>();
            //result.Value.Should().Be(new List<Twitt>());
            //Assert.IsType<List<User>>(listT);
            //Assert.Equal(new List<User>(), listT);
        }

        [Fact]
        public async void Get_OnSuccess_InvokeUserServicesExactlyOnce()
        {
            //arrange
            var moq = new Mock<IUserServices>();
            var listUsers = moq.Setup(service => service.GetAll()).ReturnsAsync(UserFixture.GetTesUserList);
            var controller = new UsersController(moq.Object);

            //act
            var result = (OkObjectResult)await controller.GetAll();

            //assert
            moq.Verify(service => service.GetAll(),
                Moq.Times.Once()
                );

            //result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async void Get_OnSuccess_ReturnListOfUsers()
        {
            //arrange 
            var moq = new Mock<IUserServices>();
            var controller = new UsersController(moq.Object);

            //act
            var result = (OkObjectResult)await controller.GetAll();

            //assert
            result.Value.Should().BeOfType<List<User>>();
            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task Get_OnNotFound404()
        {
            //arrange
            var moq = new Mock<IUserServices>();
            var controller = new UsersController(moq.Object);
            var mock = MockHttpMessageHandler<User>.Setup404NotFound();
            var client = new HttpClient(mock.Object);
            var mockDbSet = new Mock<DbSet<User>>();
            var optionsBuilder = new DbContextOptionsBuilder<MyDBContext>();
            var options = optionsBuilder.UseSqlServer("query to connect database (connection string)").Options;
            var mockContext = new Mock<MyDBContext>(options);
            mockContext.Setup(x => x.users).Returns(mockDbSet.Object);

            var config = Options.Create(new UserApiOptions()
            {
                Endpoint = "dodool.com"
            });
            var service = new UserServices((MyDBContext)mockContext.Object, client, config);

            //act 
            var result =(OkObjectResult) await controller.GetAll();
            var list = (List<User>)result.Value;

            //assert
            list.Count.Should().Be(0);
        }

    }
}