using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TalentHub.UserService.Api.Controllers;
using TalentHub.UserService.Api.Models.Person;
using TalentHub.UserService.Application.DTO.Person;
using TalentHub.UserService.Application.Interfaces;
using Xunit;

namespace TalentHub.UserService.Api.Tests;

public class PersonControllerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IPersonService> _serviceMock;
    private readonly PersonController _controller;

    public PersonControllerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _serviceMock = new Mock<IPersonService>();
        _controller = new PersonController(_mapperMock.Object, _serviceMock.Object);
    }

    [Fact]
    public async Task CreatePersonAsync_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Invalid model");

        // Act
        var result = await _controller.CreatePersonAsync(new CreatePersonModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task CreatePersonAsync_ShouldReturnBadRequest_WhenMappingFails()
    {
        // Arrange
        _mapperMock.Setup(x => x.Map<CreatePersonDto>(It.IsAny<CreatePersonModel>())).Returns((CreatePersonDto)null);

        // Act
        var result = await _controller.CreatePersonAsync(new CreatePersonModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task CreatePersonAsync_ShouldReturnOk_WhenPersonIsCreatedSuccessfully()
    {
        // Arrange
        var createPersonDto = new CreatePersonDto();
        _mapperMock.Setup(x => x.Map<CreatePersonDto>(It.IsAny<CreatePersonModel>())).Returns(createPersonDto);
        _serviceMock.Setup(x => x.CreatePersonAsync(It.IsAny<CreatePersonDto>())).ReturnsAsync(Guid.NewGuid());

        // Act
        var result = await _controller.CreatePersonAsync(new CreatePersonModel());

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetPersonAsync_ShouldReturnNotFound_WhenPersonIsNotFound()
    {
        // Arrange
        _serviceMock.Setup(x => x.GetPersonByIdAsync(It.IsAny<Guid>())).ReturnsAsync((PersonDto)null);

        // Act
        var result = await _controller.GetPersonAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task GetPersonAsync_ShouldReturnOk_WhenPersonIsFound()
    {
        // Arrange
        var personDto = new PersonDto();
        _serviceMock.Setup(x => x.GetPersonByIdAsync(It.IsAny<Guid>())).ReturnsAsync(personDto);
        _mapperMock.Setup(x => x.Map<PersonModel>(It.IsAny<PersonDto>())).Returns(new PersonModel());

        // Act
        var result = await _controller.GetPersonAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task UpdatePersonAsync_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Error", "Invalid model");

        // Act
        var result = await _controller.UpdatePersonAsync(new UpdatePersonModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task UpdatePersonAsync_ShouldReturnBadRequest_WhenMappingFails()
    {
        // Arrange
        _mapperMock.Setup(x => x.Map<UpdatePersonDto>(It.IsAny<UpdatePersonModel>())).Returns((UpdatePersonDto)null);

        // Act
        var result = await _controller.UpdatePersonAsync(new UpdatePersonModel());

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task UpdatePersonAsync_ShouldReturnOk_WhenPersonIsUpdatedSuccessfully()
    {
        // Arrange
        var updatePersonDto = new UpdatePersonDto();
        _mapperMock.Setup(x => x.Map<UpdatePersonDto>(It.IsAny<UpdatePersonModel>())).Returns(updatePersonDto);
        _serviceMock.Setup(x => x.UpdatePersonAsync(It.IsAny<UpdatePersonDto>())).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdatePersonAsync(new UpdatePersonModel());

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task DeletePersonAsync_ShouldReturnNotFound_WhenPersonIsNotFound()
    {
        // Arrange
        _serviceMock.Setup(x => x.DeletePersonAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        // Act
        var result = await _controller.DeletePersonAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task DeletePersonAsync_ShouldReturnNoContent_WhenPersonIsDeletedSuccessfully()
    {
        // Arrange
        _serviceMock.Setup(x => x.DeletePersonAsync(It.IsAny<Guid>())).ReturnsAsync(true);

        // Act
        var result = await _controller.DeletePersonAsync(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}
