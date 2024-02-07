using AutoFixture;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace pracadyplomowa.UnitTests;

public class AdminControllerTests
{
    private readonly Mock<UserManager<User>> _userManager;
    private readonly AdminController _controller;
    private readonly Fixture _fixture;

    public AdminControllerTests()
    {
        _fixture = new Fixture();

        _userManager = new Mock<UserManager<User>>(
            Mock.Of<IUserStore<User>>(),
            null,  // Mock.Of<IUserValidator<User>>(),
            null,  // Mock.Of<IPasswordValidator<User>>(),
            null,  // Mock.Of<ILookupNormalizer>(),
            null,  // Mock.Of<IdentityErrorDescriber>(),
            null,  // Mock.Of<IServiceProvider>(),
            null,  // Mock.Of<IOptions<IdentityOptions>>(),
            null   // Mock.Of<ILogger<UserManager<User>>>(),
        );

        _controller = new AdminController(_userManager.Object);
    }

    [Fact]
    public async Task GetUsersWithRoles_WithNoParams_ShouldReturnListUsersWithRoles()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, UserName = "userTest1", UserRoles = [new UserRole { Role = new Role { Name = "Admin" } }] },
            new User { Id = 2, UserName = "userTest2", UserRoles = [new UserRole { Role = new Role { Name = "User" } }] },
        }.AsQueryable();

        _userManager.Setup(x => x.Users)
            .Returns(users);

        // Act
        var result = await _controller.GetUsersWithRoles();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUsers = Assert.IsType<List<object>>(okResult.Value);
        Assert.Equal(users.Count(), returnedUsers.Count);
    }
}
