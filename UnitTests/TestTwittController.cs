using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Services;
using WebAPISample.Controllers;
using Xunit;

namespace UnitTest
{
    public class TestTwittController
    {
        [Fact]
        public void GetAllTest()
        {
            var moq = new Mock<ITwittRepsitory>().Object;
            var controller = new TwittController(moq);

            var result =  (OkObjectResult) controller.GetAll();

            result.StatusCode.Should().Be(200);
        }
    }
}