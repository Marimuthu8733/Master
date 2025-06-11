using System.Text.Json.Serialization;
using WebApi;
using WebApi.Helpers;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
 
    services.AddDbContext<DataContext>();
    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    }).AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    
    // Add output caching for improved performance
    services.AddOutputCache(options => 
    {
        options.AddPolicy("UserCache", builder => 
            builder.Expire(TimeSpan.FromMinutes(10))
                  .SetVaryByQuery("id")
                  .Tag("users"));
    });

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();
    
    // Add output caching middleware
    app.UseOutputCache();

    app.MapControllers();
}

app.Run("http://localhost:4000");
