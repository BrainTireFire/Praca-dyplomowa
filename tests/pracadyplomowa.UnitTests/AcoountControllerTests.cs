using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace pracadyplomowa.UnitTests;

public class AcoountControllerTests
{
    private readonly Mock<ITokenService> _tokenService;
    private readonly Mock<UserManager<User>> _userManager;
    private readonly Mock<SignInManager<User>> _signInManager;
    private readonly AccountController _controller;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    public AcoountControllerTests()
    {
        _fixture = new Fixture();

        var mockMapper = new MapperConfiguration(mc =>
        {
            mc.AddMaps(typeof(MappingProfiles).Assembly);
        }).CreateMapper().ConfigurationProvider;
        _mapper = new Mapper(mockMapper);

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

        _signInManager = new Mock<SignInManager<User>>(
            _userManager.Object,  // UserManager
            Mock.Of<IHttpContextAccessor>(),  // IHttpContextAccessor
            Mock.Of<IUserClaimsPrincipalFactory<User>>()  // IUserClaimsPrincipalFactory<User>
        );

        // _signInManager = new Mock<SignInManager<User>>(
        //     _userManager.Object,  // UserManager
        //     Mock.Of<IHttpContextAccessor>(),  // IHttpContextAccessor
        //     Mock.Of<IUserClaimsPrincipalFactory<User>>(),  // IUserClaimsPrincipalFactory<User>
        //     Mock.Of<IOptions<IdentityOptions>>(),
        //     Mock.Of<ILogger>(),
        //     Mock.Of<IAuthenticationSchemeProvider>(),
        //     Mock.Of<IUserConfirmation<User>>()
        // );

        _tokenService = new Mock<ITokenService>();

        _controller = new AccountController(
            _userManager.Object,
            _signInManager.Object,
            _tokenService.Object,
            _mapper
        );
    }

    [Fact]
    public async Task Register_ShouldReturnUserDto_WhenUserIsSuccessfullyRegistered()
    {
        // Arrange
        var registerDto = _fixture.Create<RegisterDto>();
        var user = new User { UserName = registerDto.Username.ToLower() };
        var userDto = new UserDto { Username = user.UserName, Token = "testToken" };

        _userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _tokenService.Setup(x => x.CreateToken(It.IsAny<User>()))
            .ReturnsAsync(userDto.Token);

        // Act
        var result = await _controller.Register(registerDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUserDto = Assert.IsType<UserDto>(okResult.Value);
        Assert.Equal(userDto.Username, returnedUserDto.Username);
        Assert.Equal(userDto.Token, returnedUserDto.Token);
    }

}
