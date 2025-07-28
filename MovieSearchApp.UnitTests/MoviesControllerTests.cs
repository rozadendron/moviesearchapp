using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieSearchApp.Server.Controllers;

public class MoviesControllerTests
{
    private MoviesController CreateControllerWithHttpResponse(string responseContent)
    {
        var handlerMock = new Mock<HttpMessageHandler>();     

        var httpClient = new HttpClient(handlerMock.Object);
        var factoryMock = new Mock<IHttpClientFactory>();
        factoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

        return new MoviesController(factoryMock.Object);
    }

    [Fact]
    public async Task Search_ReturnsBadRequest_WhenTitleIsNullOrWhitespace()
    {
        var controller = CreateControllerWithHttpResponse("{}");
        var result = await controller.Search(null);
        Assert.IsType<BadRequestObjectResult>(result);

        result = await controller.Search("");
        Assert.IsType<BadRequestObjectResult>(result);

        result = await controller.Search("   ");
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsBadRequest_WhenImdbIdIsNullOrWhitespace()
    {
        var controller = CreateControllerWithHttpResponse("{}");
        var result = await controller.Details(null);
        Assert.IsType<BadRequestObjectResult>(result);

        result = await controller.Details("");
        Assert.IsType<BadRequestObjectResult>(result);

        result = await controller.Details("   ");
        Assert.IsType<BadRequestObjectResult>(result);
    }      
}
