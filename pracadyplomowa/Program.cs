using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa;
using pracadyplomowa.Authorization.AuthorizationHandlers;
using pracadyplomowa.Authorization.AuthorizationPolicyProviders;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnectionSQlite"));
});
// builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
// {
//     opt.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection"));
// });
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<IAuthorizationHandler, OwnershipHandler>();
// builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, OwnershipPolicyProvider>();


// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppIdentityDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManger = services.GetRequiredService<RoleManager<Role>>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(userManager, roleManger);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
