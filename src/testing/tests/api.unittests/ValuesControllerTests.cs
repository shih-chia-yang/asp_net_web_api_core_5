using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace api.unittests
{
    public class ValuesControllerTests
    {
        [Fact]
        public async Task TestGet()
        {
            //arrange
            
            var controller = new ValuesController();
            //Act
            IActionResult actionResult = await controller.Get();
            OkObjectResult ok = actionResult as OkObjectResult;
            List<string> actual = ok.Value as List<string>;
            //Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(ok);
            Assert.Equal(2, actual.Count);
            Assert.Equal("value1", actual[0]);
            Assert.Equal("value2", actual[1]);
        }

        [Fact]
        public async Task TestPost()
        {
            //Given
            var controller = new ValuesController();
            //When
            IActionResult actionResult = await controller.Post("Test value");
            //Then
            Assert.NotNull(actionResult);
            CreatedResult result = actionResult as CreatedResult;
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }
    }
}
