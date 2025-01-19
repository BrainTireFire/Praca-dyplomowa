using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa;
using pracadyplomowa.Authorization.AuthorizationHandlers;
using pracadyplomowa.Authorization.AuthorizationPolicyProviders;
using pracadyplomowa.Repository;
using pracadyplomowa.Repository.Class;
using pracadyplomowa.Repository.Race;
using pracadyplomowa.Errors;
using pracadyplomowa.Hubs;
using pracadyplomowa.Middleware;
using pracadyplomowa.Repository.Item;
using pracadyplomowa.Repository.Board;
using pracadyplomowa.Repository.Field;
using pracadyplomowa.Services.Board;
using pracadyplomowa.Token.Services;
using System.Text.Json.Serialization;
using pracadyplomowa.Repository.Encounter;
using pracadyplomowa.Repository.UnitOfWork;
using pracadyplomowa.RequestHelpers;
using pracadyplomowa.Services.Encounter;
using pracadyplomowa.Services.Item;
using pracadyplomowa.Services;
using pracadyplomowa.Services.Websockets.Connection;
using pracadyplomowa.Services.Websockets.Notification;

var builder = WebApplication.CreateBuilder(args);


// builder.Services.AddDbContext<AppDbContext>(opt =>
// {
//     opt.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnectionSQlite"));
// });
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection"));
});
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new EffectBlueprintJsonConverter());
    });
builder.Services.AddSignalR();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IEncounterService, EncounterService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IConnectionService, ConnectionService>(); 

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<IFieldRepository, FieldRepository>();
builder.Services.AddScoped<IPowerRepository, PowerRepository>();
builder.Services.AddScoped<IEffectBlueprintRepository, EffectBlueprintRepository>();
builder.Services.AddScoped<IEffectInstanceRepository, EffectInstanceRepository>();
builder.Services.AddScoped<IEffectGroupRepository, EffectGroupRepository>();
builder.Services.AddScoped<IItemFamilyRepository, ItemFamilyRepository>();
builder.Services.AddScoped<IImmaterialResourceBlueprintRepository, ImmaterialResourceBlueprintRepository>();
builder.Services.AddScoped<IItemCostRequirementRepository, ItemCostRequirementRepository>();
builder.Services.AddScoped<IEquipmentSlotRepository, EquipmentSlotRepository>();
builder.Services.AddScoped<IEncounterRepository, EncounterRepository>();
builder.Services.AddScoped<IParticipanceDataRepository, ParticipanceDataRepository>();

builder.Services.AddScoped<IAuthorizationHandler, OwnershipHandler>();
// builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, OwnershipPolicyProvider>();


// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value != null && e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value?.Errors)
            .Select(x => x.ErrorMessage).ToArray();

        var errorResponse = new ApiValidationErrorResponse()
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("http://localhost:5173");
    });
});

var app = builder.Build();

// // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");


//app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<SessionHub>("/session");
app.MapHub<NotificationHub>("/notifications");
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManger = services.GetRequiredService<RoleManager<Role>>();
    var dbContext = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(userManager, roleManger);
    await Seed.SeedEquipmentSlots(context);
    await Seed.SeedItemFamilies(dbContext);
    await Seed.SeedItems(dbContext);
    await Seed.SeedLanguages(dbContext);
    await Seed.SeedRaces(dbContext);
    await Seed.SeedClasses(dbContext);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
