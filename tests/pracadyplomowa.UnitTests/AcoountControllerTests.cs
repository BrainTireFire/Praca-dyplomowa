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
        var userDto = new UserDto { Username = user.UserName, Token = "testToken" };

        _accountRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>()))
            .ReturnsAsync((User)null);
        _accountRepository.Setup(x => x.RegisterUserAsync(It.IsAny<RegisterDto>(), It.IsAny<string>()))
            .ReturnsAsync((IdentityResult.Success, user));
        _accountRepository.Setup(x => x.AddUserToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
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
