//using Dexef.Storage;
using InventoryApp.BLL;
using InventoryApp.BLL.Mapping;
using InventoryApp.Core.Infrastructure;
using InventoryApp.DTO.Setting.Files;
using InventoryApp.Repositroy.Base;
using InventoryApp.BLL.StaticEnums;
using Microsoft.Data.SqlClient;
using EntityFrameworkCore.Scaffolding.Handlebars.Internal;
using InventoryApp.BLL.Products;
using InventoryApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
#region Configuration
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
});

var env = builder.Environment.EnvironmentName;
builder.WebHost.UseIIS();
builder.WebHost.UseDefaultServiceProvider(options => options.ValidateScopes = false);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();
#endregion


builder.Services.AddControllers();
#region config sec
builder.Services.Configure<FileStorageSetting>(builder.Configuration.GetSection(AppsettingsEnum.FileStorageSetting.ToString()));
#endregion config sec
#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
#endregion

#region HttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
#endregion
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region BLL

foreach (var implementationType in typeof(BaseBLL).Assembly.GetTypes().Where(s => s.IsClass && s.Name.EndsWith("BLL") && !s.IsAbstract))
{
    foreach (var interfaceType in implementationType.GetInterfaces())
    {
        builder.Services.AddSingleton(interfaceType, implementationType);
    }
}




#endregion BLL



//#region DexefStorageManager
//builder.Services.AddScoped<IDexefStorageManager, DexefStorageManager>();
//#endregion DexefStorageManager
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#region Repository
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase(databaseName: "MyInMemoryDatabase"), ServiceLifetime.Singleton);

builder.Services.AddSingleton<IDbFactory, DbFactory>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseInMemoryDatabase(databaseName: "MyInMemoryDatabase"));
//builder.Services.AddScoped<IDbFactory, DbFactory>(s => new DbFactory(new SqlConnection(builder.Configuration.GetConnectionString("DexefGeneralSettingConnection"))));
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion Repository

#region Mapper

builder.Services.AddAutoMapper(typeof(EntityToDtoMappingProfile), typeof(DtoToEntityMappingProfile), typeof(DtoMappingProfile));

#endregion Mapper

#region Repository

builder.Services.AddSingleton(typeof(IRepository<>), typeof(BaseRepository<>));

#endregion Repository



#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
#endregion

builder.Services.AddRouting(options => options.ConstraintMap.Add("apiVersion", typeof(ApiVersionRouteConstraint)));
// In the ConfigureServices method:
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v2" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
// In the Configure method:
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
});
app.UseCors(policyName: "CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();

//#region Localization 
//app.UseMiddleware<LocalizationMiddleware>();
//#endregion

app.MapControllers();



app.Run();