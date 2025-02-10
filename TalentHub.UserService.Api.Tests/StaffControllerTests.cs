using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TalentHub.UserService.Api.Controllers;
using TalentHub.UserService.Api.Models.Staff;
using TalentHub.UserService.Application.Abstractions;
using TalentHub.UserService.Application.DTO.Staff;
using Xunit;

namespace TalentHub.UserService.Api.Tests;

public class StaffControllerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IStaffService> _serviceMock;
    private readonly StaffController _controller;

    public StaffControllerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _serviceMock = new Mock<IStaffService>();
        _controller = new StaffController(_mapperMock.Object, _serviceMock.Object);
    }

    [Fact]
    public async Task CreateStaffAsync_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Invalid model");

        // Act
        var result = await _controller.CreateStaffAsync(new CreateStaffModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task CreateStaffAsync_ShouldReturnBadRequest_WhenMappingFails()
    {
        // Arrange
        _mapperMock.Setup(x => x.Map<CreateStaffDto>(It.IsAny<CreateStaffModel>())).Returns((CreateStaffDto)null);

        // Act
        var result = await _controller.CreateStaffAsync(new CreateStaffModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task CreateStaffAsync_ShouldReturnOk_WhenStaffIsCreatedSuccessfully()
    {
        // Arrange
        var createStaffDto = new CreateStaffDto();
        _mapperMock.Setup(x => x.Map<CreateStaffDto>(It.IsAny<CreateStaffModel>())).Returns(createStaffDto);
        _serviceMock.Setup(x => x.CreateStaffAsync(It.IsAny<CreateStaffDto>())).ReturnsAsync(Guid.NewGuid());

        // Act
        var result = await _controller.CreateStaffAsync(new CreateStaffModel());

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetStaffAsync_ShouldReturnNotFound_WhenStaffIsNotFound()
    {
        // Arrange
        _serviceMock.Setup(x => x.GetStaffByIdAsync(It.IsAny<Guid>())).ReturnsAsync((StaffDto)null);

        // Act
        var result = await _controller.GetStaffAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task GetStaffAsync_ShouldReturnOk_WhenStaffIsFound()
    {
        // Arrange
        var staffDto = new StaffDto();
        _serviceMock.Setup(x => x.GetStaffByIdAsync(It.IsAny<Guid>())).ReturnsAsync(staffDto);
        _mapperMock.Setup(x => x.Map<StaffModel>(It.IsAny<StaffDto>())).Returns(new StaffModel());

        // Act
        var result = await _controller.GetStaffAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task UpdateStaffAsync_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Invalid model");

        // Act
        var result = await _controller.UpdateStaffAsync(new UpdateStaffModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task UpdateStaffAsync_ShouldReturnBadRequest_WhenMappingFails()
    {
        // Arrange
        _mapperMock.Setup(x => x.Map<UpdateStaffDto>(It.IsAny<UpdateStaffModel>())).Returns((UpdateStaffDto)null);

        // Act
        var result = await _controller.UpdateStaffAsync(new UpdateStaffModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task UpdateStaffAsync_ShouldReturnOk_WhenStaffIsUpdatedSuccessfully()
    {
        // Arrange
        var updateStaffDto = new UpdateStaffDto();
        _mapperMock.Setup(x => x.Map<UpdateStaffDto>(It.IsAny<UpdateStaffModel>())).Returns(updateStaffDto);
        _serviceMock.Setup(x => x.UpdateStaffAsync(It.IsAny<UpdateStaffDto>())).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateStaffAsync(new UpdateStaffModel());

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task DeleteStaffAsync_ShouldReturnNotFound_WhenStaffIsNotFound()
    {
        // Arrange
        _serviceMock.Setup(x => x.DeleteStaffAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteStaffAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteStaffAsync_ShouldReturnNoContent_WhenStaffIsDeletedSuccessfully()
    {
        // Arrange
        _serviceMock.Setup(x => x.DeleteStaffAsync(It.IsAny<Guid>())).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteStaffAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}
