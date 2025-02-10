using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TalentHub.UserService.Api.Abstractions;
using TalentHub.UserService.Api.Controllers;
using TalentHub.UserService.Api.Models.Employer;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.DTO.Employer;
using Xunit;

namespace TalentHub.UserService.Api.Tests;

public class EmployerControllerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<INotificationProducer> _producerMock;
    private readonly Mock<IEmployerService> _serviceMock;
    private readonly EmployerController _controller;

    public EmployerControllerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _producerMock = new Mock<INotificationProducer>();
        _serviceMock = new Mock<IEmployerService>();
        _controller = new EmployerController(_mapperMock.Object, _producerMock.Object, _serviceMock.Object);
    }

    [Fact]
    public async Task CreateEmployerAsync_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Invalid model");

        // Act
        var result = await _controller.CreateEmployerAsync(new CreateEmployerModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task CreateEmployerAsync_ShouldReturnBadRequest_WhenMappingFails()
    {
        // Arrange
        _mapperMock.Setup(x => x.Map<CreateEmployerDto>(It.IsAny<CreateEmployerModel>())).Returns((CreateEmployerDto)null);

        // Act
        var result = await _controller.CreateEmployerAsync(new CreateEmployerModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task CreateEmployerAsync_ShouldReturnOk_WhenEmployerIsCreatedSuccessfully()
    {
        // Arrange
        var createEmployerDto = new CreateEmployerDto();
        _mapperMock.Setup(x => x.Map<CreateEmployerDto>(It.IsAny<CreateEmployerModel>())).Returns(createEmployerDto);
        _serviceMock.Setup(x => x.CreateEmployerAsync(It.IsAny<CreateEmployerDto>())).ReturnsAsync(Guid.NewGuid());

        // Act
        var result = await _controller.CreateEmployerAsync(new CreateEmployerModel());

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetEmployerAsync_ShouldReturnNotFound_WhenEmployerIsNotFound()
    {
        // Arrange
        _serviceMock.Setup(x => x.GetEmployerByIdAsync(It.IsAny<Guid>())).ReturnsAsync((EmployerDto)null);

        // Act
        var result = await _controller.GetEmployerAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task GetEmployerAsync_ShouldReturnOk_WhenEmployerIsFound()
    {
        // Arrange
        var employerDto = new EmployerDto();
        _serviceMock.Setup(x => x.GetEmployerByIdAsync(It.IsAny<Guid>())).ReturnsAsync(employerDto);
        _mapperMock.Setup(x => x.Map<EmployerModel>(It.IsAny<EmployerDto>())).Returns(new EmployerModel());

        // Act
        var result = await _controller.GetEmployerAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task UpdateEmployerAsync_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Invalid model");

        // Act
        var result = await _controller.UpdateEmployerAsync(new UpdateEmployerModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task UpdateEmployerAsync_ShouldReturnBadRequest_WhenMappingFails()
    {
        // Arrange
        _mapperMock.Setup(x => x.Map<UpdateEmployerDto>(It.IsAny<UpdateEmployerModel>())).Returns((UpdateEmployerDto)null);

        // Act
        var result = await _controller.UpdateEmployerAsync(new UpdateEmployerModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task UpdateEmployerAsync_ShouldReturnOk_WhenEmployerIsUpdatedSuccessfully()
    {
        // Arrange
        var updateEmployerDto = new UpdateEmployerDto();
        _mapperMock.Setup(x => x.Map<UpdateEmployerDto>(It.IsAny<UpdateEmployerModel>())).Returns(updateEmployerDto);
        _serviceMock.Setup(x => x.UpdateEmployerAsync(It.IsAny<UpdateEmployerDto>())).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateEmployerAsync(new UpdateEmployerModel());

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task DeleteEmployerAsync_ShouldReturnNotFound_WhenEmployerIsNotFound()
    {
        // Arrange
        _serviceMock.Setup(x => x.DeleteEmployerAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteEmployerAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteEmployerAsync_ShouldReturnNoContent_WhenEmployerIsDeletedSuccessfully()
    {
        // Arrange
        _serviceMock.Setup(x => x.DeleteEmployerAsync(It.IsAny<Guid>())).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteEmployerAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}
