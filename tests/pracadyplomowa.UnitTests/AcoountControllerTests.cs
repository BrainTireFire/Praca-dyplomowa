using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using pracadyplomowa.Token.Services;

namespace pracadyplomowa.UnitTests;

public class AcoountControllerTests
{
    private readonly Mock<ITokenService> _tokenService;
    private readonly Mock<IAccountRepository> _accountRepository;
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

        _tokenService = new Mock<ITokenService>();
        _accountRepository = new Mock<IAccountRepository>();

        _controller = new AccountController(
            _tokenService.Object,
            _accountRepository.Object
        );
    }

    [Fact]
    public async Task Register_ShouldReturnUserDto_WhenUserIsSuccessfullyRegistered()
    {
        // Arrange
        var registerDto = _fixture.Create<RegisterDto>();
        var user = new User { UserName = registerDto.Username.ToLower() };
        var userDto = new UserDto { Username = user.UserName }; //Token = "testToken" };

        _accountRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>()))
            .ReturnsAsync((User)null!);
        _accountRepository.Setup(x => x.RegisterUserAsync(It.IsAny<RegisterDto>(), It.IsAny<string>()))
            .ReturnsAsync((IdentityResult.Success, user));
        _accountRepository.Setup(x => x.AddUserToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        // _tokenService.Setup(x => x.CreateToken(It.IsAny<User>()))
        //     .ReturnsAsync(userDto.Token);

        // Act
        var result = await _controller.Register(registerDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUserDto = Assert.IsType<UserDto>(okResult.Value);
        Assert.Equal(userDto.Username, returnedUserDto.Username);
        // Assert.Equal(userDto.Token, returnedUserDto.Token);
    }

    [Fact]
    public async Task Register_ShouldReturn400BadRequest_WhenAlreadyExists()
    {
        // Arrange
        var registerDto = _fixture.Create<RegisterDto>();
        var user = new User { UserName = registerDto.Username.ToLower() };

        _accountRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>()))
            .ReturnsAsync(user);

        // Act
        var result = await _controller.Register(registerDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Username already exists on this username: " + registerDto.Username, badRequestResult.Value);
    }

    [Fact]
    public async Task Login_ShouldReturnUserDto_WhenUserCorrectlyLogin()
    {
        // Arrange
        var loginDto = _fixture.Create<LoginDto>();
        var loginResult = _fixture.Create<LoginResult>();
        var user = new User { UserName = loginDto.Username.ToLower() };
        var userDto = new UserDto { Username = user.UserName }; //, Token = "testToken" };

        _accountRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>()))
            .ReturnsAsync(user);
        //TODO
        // _accountRepository.Setup(x => x.LoginUserAsync("username1", It.IsAny<string>()))
        //     .ReturnsAsync(loginResult);
        // _tokenService.Setup(x => x.CreateToken(It.IsAny<User>()))
        //     .ReturnsAsync(userDto.Token);

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUserDto = Assert.IsType<UserDto>(okResult.Value);
        Assert.Empty(loginResult.ErrorMessage);
        Assert.Equal(userDto.Username, returnedUserDto.Username);
        // Assert.Equal(userDto.Token, returnedUserDto.Token);
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenUserUnauthorized()
    {
        // Arrange
        var loginDto = _fixture.Create<LoginDto>();
        _accountRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>()))
            .ReturnsAsync((User)null!);

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        Assert.IsType<UnauthorizedResult>(result.Result);
    }


}
