using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TalentHub.UserService.Api.Controllers;
using TalentHub.UserService.Api.Models.UserSettings;
using TalentHub.UserService.Application.DTO.UserSettings;
using TalentHub.UserService.Application.Interfaces;
using Xunit;

namespace TalentHub.UserService.Api.Tests;

public class UserSettingsControllerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUserSettingsService> _serviceMock;
    private readonly UserSettingsController _controller;

    public UserSettingsControllerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _serviceMock = new Mock<IUserSettingsService>();
        _controller = new UserSettingsController(_mapperMock.Object, _serviceMock.Object);
    }

    [Fact]
    public async Task CreateUserSettingsAsync_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Invalid model");

        // Act
        var result = await _controller.CreateUserSettingsAsync(new CreateUserSettingsModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task CreateUserSettingsAsync_ShouldReturnBadRequest_WhenMappingFails()
    {
        // Arrange
        _mapperMock.Setup(m => m.Map<CreateUserSettingsDto>(It.IsAny<CreateUserSettingsModel>()))
            .Returns((CreateUserSettingsDto)null);

        // Act
        var result = await _controller.CreateUserSettingsAsync(new CreateUserSettingsModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task CreateUserSettingsAsync_ShouldReturnOk_WhenUserSettingsAreCreatedSuccessfully()
    {
        // Arrange
        var dto = new CreateUserSettingsDto();
        _mapperMock.Setup(m => m.Map<CreateUserSettingsDto>(It.IsAny<CreateUserSettingsModel>()))
            .Returns(dto);
        _serviceMock.Setup(s => s.CreateUserSettingsAsync(It.IsAny<CreateUserSettingsDto>()))
            .ReturnsAsync(Guid.NewGuid());

        // Act
        var result = await _controller.CreateUserSettingsAsync(new CreateUserSettingsModel());

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetUserSettingsAsync_ShouldReturnNotFound_WhenUserSettingsAreNotFound()
    {
        // Arrange
        _serviceMock.Setup(s => s.GetUserSettingsByIdAsync(It.IsAny<Guid>())).ReturnsAsync((UserSettingsDto)null);

        // Act
        var result = await _controller.GetUserSettingsAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task GetUserSettingsAsync_ShouldReturnOk_WhenUserSettingsAreFound()
    {
        // Arrange
        var dto = new UserSettingsDto();
        _serviceMock.Setup(s => s.GetUserSettingsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dto);
        _mapperMock.Setup(m => m.Map<UserSettingsModel>(It.IsAny<UserSettingsDto>())).Returns(new UserSettingsModel());

        // Act
        var result = await _controller.GetUserSettingsAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task UpdateUserSettingsAsync_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Invalid model");

        // Act
        var result = await _controller.UpdateUserSettingsAsync(new UpdateUserSettingsModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task UpdateUserSettingsAsync_ShouldReturnBadRequest_WhenMappingFails()
    {
        // Arrange
        _mapperMock.Setup(m => m.Map<UpdateUserSettingsDto>(It.IsAny<UpdateUserSettingsModel>()))
            .Returns((UpdateUserSettingsDto)null);

        // Act
        var result = await _controller.UpdateUserSettingsAsync(new UpdateUserSettingsModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task UpdateUserSettingsAsync_ShouldReturnOk_WhenUserSettingsAreUpdatedSuccessfully()
    {
        // Arrange
        var dto = new UpdateUserSettingsDto();
        _mapperMock.Setup(m => m.Map<UpdateUserSettingsDto>(It.IsAny<UpdateUserSettingsModel>())).Returns(dto);
        _serviceMock.Setup(s => s.UpdateUserSettingsAsync(dto)).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateUserSettingsAsync(new UpdateUserSettingsModel());

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task DeleteUserSettingsAsync_ShouldReturnNotFound_WhenUserSettingsAreNotFound()
    {
        // Arrange
        _serviceMock.Setup(s => s.DeleteUserSettingsAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteUserSettingsAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeleteUserSettingsAsync_ShouldReturnNoContent_WhenUserSettingsAreDeletedSuccessfully()
    {
        // Arrange
        _serviceMock.Setup(s => s.DeleteUserSettingsAsync(It.IsAny<Guid>())).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteUserSettingsAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}
