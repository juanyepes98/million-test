using Api.Common;
using Api.Controllers.Property;
using Application.Common.DTOs;
using Application.Property.DTOs;
using Application.Property.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test.Controllers;

public class PropertyControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly PropertyController _controller;

    public PropertyControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new PropertyController(_mediatorMock.Object);
    }
    
    [Fact]
    public async Task GetAll_ShouldReturnOkResult_WithExpectedData()
    {
        // Arrange
        var expectedResult = new PagedResultDto<PropertyRowDto>
        {
            Items = new List<PropertyRowDto>
            {
                new()
                {
                    Id = "1", Name = "Casa Verde",
                    PropertyImages = new List<PropertyImage> { new PropertyImage { File = "file.jpg", Enabled = true } }
                }
            },
            TotalCount = 1,
            Page = 1,
            PageSize = 10
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllPropertiesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.GetAll(1, 10, "Casa", "Calle", 500000, 2000000);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var apiResponse = Assert.IsType<ApiResponse<PagedResultDto<PropertyRowDto>>>(okResult.Value);

        Assert.True(apiResponse.Success);
        Assert.Equal(1, apiResponse.Content?.TotalCount);
        Assert.Equal("Casa Verde", apiResponse.Content?.Items.First().Name);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenPropertyExists()
    {
        // Arrange
        var expectedProperty = new PropertyDto
        {
            Id = "123",
            Name = "Casa Azul",
            Address = "Calle Falsa 123",
            Price = 1500000
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetPropertyByIdQuery>(q => q.Id == "123"), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProperty);

        // Act
        var result = await _controller.GetById("123");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var apiResponse = Assert.IsType<ApiResponse<PropertyDto>>(okResult.Value);

        Assert.True(apiResponse.Success);
        Assert.Equal("123", apiResponse.Content?.Id);
        Assert.Equal("Casa Azul", apiResponse.Content?.Name);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenPropertyDoesNotExist()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetPropertyByIdQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new KeyNotFoundException("Property not found"));

        // Act
        var result = await _controller.GetById("999");

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var apiResponse = Assert.IsType<ApiResponse<string>>(notFoundResult.Value);

        Assert.False(apiResponse.Success);
        Assert.Equal("Property not found", apiResponse.Message);
    }
}