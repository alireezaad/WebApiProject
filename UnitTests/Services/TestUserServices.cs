using Castle.Components.DictionaryAdapter.Xml;
using FluentAssertions;
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
using WebApi.Model;
using WebApi.Model.DBContext;
using WebApi.Services;
using Xunit;
using static System.Net.WebRequestMethods;

namespace UnitTests.Services
{
    public class TestUserServices
    {
        [Fact]
        public async Task GetAll_WhenCalled_InvokeHttpRequest()
        {
            //arrange
            var expectedResponse = UserFixture.GetTesUserList();
            var handler = MockHttpMessageHandler<User>.SetBasicGetResourceList(expectedResponse);
            var client = new HttpClient(handler.Object);
            var endpoint = "https://example.com/users";

            var mockDbSet = new Mock<DbSet<User>>();
            var optionsBuilder = new DbContextOptionsBuilder<MyDBContext>();
            var options = optionsBuilder.UseSqlServer("query to connect database (connection string)").Options;
            var mockContext = new Mock<MyDBContext>(options);
            mockContext.Setup(x => x.users).Returns(mockDbSet.Object);

            var config = Options.Create(new UserApiOptions()
            {
                Endpoint = endpoint
            });
            var sut = new UserServices(mockContext.Object, client, config);


            //act
            await sut.GetAll();

            //assert
            handler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );
        }
        [Fact]
        public async Task GetAll_WhenCalled_ReturnListOfUsers()
        {
            //arrange
            var expectedResponse = UserFixture.GetTesUserList();
            var handler = MockHttpMessageHandler<User>.SetBasicGetResourceList(expectedResponse);
            var client = new HttpClient(handler.Object);
            var endpoint = "https://example.com/users";
            var mockDbSet = new Mock<DbSet<User>>();
            var optionsBuilder = new DbContextOptionsBuilder<MyDBContext>();
            var options = optionsBuilder.UseSqlServer("query to connect database (connection string)").Options;
            var mockContext = new Mock<MyDBContext>(options);
            mockContext.Setup(x => x.users).Returns(mockDbSet.Object);
            var config = Options.Create(new UserApiOptions()
            {
                Endpoint = endpoint
            });
            var sut = new UserServices(mockContext.Object, client, config);


            //act
            var result = await sut.GetAll();

            //assert 
            result.Should().BeOfType<List<User>>();
        }
        [Fact]
        public async Task GetAll_WhenCalled_ReturnZeroUser()
        {
            //arrange
            var handler = MockHttpMessageHandler<User>.Setup404NotFound();
            var client = new HttpClient(handler.Object);
            var endpoint = "https://example.com/users";
            var mockDbSet = new Mock<DbSet<User>>();
            var optionsBuilder = new DbContextOptionsBuilder<MyDBContext>();
            var options = optionsBuilder.UseSqlServer("query to connect database (connection string)").Options;
            var mockContext = new Mock<MyDBContext>(options);
            mockContext.Setup(x => x.users).Returns(mockDbSet.Object);

            var config = Options.Create(new UserApiOptions()
            {
                Endpoint = endpoint
            });
            var sut = new UserServices(mockContext.Object, client, config);


            //act
            var result = await sut.GetAll();

            //assert 
            result.ToList().Count.Should().Be(0);

        }
        [Fact]
        public async Task GetAll_WhenCalled_ReturnExpectedNumbersOfUsers()
        {
            //arrange
            var expectedResponse = UserFixture.GetTesUserList();
            var handler = MockHttpMessageHandler<User>.SetBasicGetResourceList(expectedResponse);
            var client = new HttpClient(handler.Object);
            var endpoint = "https://example.com/users";

            var mockDbSet = new Mock<DbSet<User>>();
            var optionsBuilder = new DbContextOptionsBuilder<MyDBContext>();
            var options = optionsBuilder.UseSqlServer("query to connect database (connection string)").Options;
            var mockContext = new Mock<MyDBContext>(options);
            mockContext.Setup(x => x.users).Returns(mockDbSet.Object);

            var config = Options.Create(new UserApiOptions()
            {
                Endpoint = endpoint
            });
            var sut = new UserServices(mockContext.Object, client, config);


            //act
            var result = await sut.GetAll();

            //assert 
            result.ToList().Count.Should().Be(expectedResponse.Count);
        }
        [Fact]
        public async Task GetAll_WhenCalled_InvokeConfiguredWithExternalUrl()
        {
            //arrange
            //var endpoint = "https://jsonplaceholder.typicode.com/users";
            var endpoint = "https://example.com/users";
            var expectedResponse = UserFixture.GetTesUserList();
            var handler = MockHttpMessageHandler<User>.SetBasicGetResourceList(expectedResponse);
            var client = new HttpClient(handler.Object);

            // Mock DbContext
            var mockDbSet =new Mock<DbSet<User>>();
            ////mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(expectedResponse.AsQueryable().Provider);
            ////mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(expectedResponse.AsQueryable().Expression);
            ////mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(expectedResponse.AsQueryable().ElementType);
            ////mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => expectedResponse.GetEnumerator());
            var optionsBuilder = new DbContextOptionsBuilder<MyDBContext>();
            var options = optionsBuilder.UseSqlServer("query to connect database (connection string)").Options;
            var mockContext = new Mock<MyDBContext>(options);
            //mockContext.Setup(x => x.users).Returns(mockDbSet.Object);
            //

            var config = Options.Create(new UserApiOptions()
            {
                Endpoint = endpoint
            });
            var sut = new UserServices(mockContext.Object, client, config);


            //act
            await sut.GetAll();
            var uri = new Uri(endpoint);

            //assert
            handler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.Equals(uri)),
                ItExpr.IsAny<CancellationToken>()
                );
        }
    }
}

