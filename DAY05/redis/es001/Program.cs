var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisOutputCache(options =>
    {
        options.Configuration = "";
    }
);

builder.Services.AddOutputCache(options =>
{
    // Optional configuration

    options.AddBasePolicy(builder => { builder.Expire(TimeSpan.FromSeconds(100)); });

    options.AddPolicy("ExpireImages", builder => { builder.Expire(TimeSpan.FromSeconds(600)); });



});

builder.Services.AddControllers();
builder.Services.AddResponseCaching();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/cached", () => "Info in redis output cache" + DateTime.Now).CacheOutput();

app.MapGet("/gravatar", Gravatar.WriteGravatar).CacheOutput("ExpireImages");

// Uso di Redis Output Caching Middleware Service
app.UseOutputCache();

app.UseResponseCaching();

app.MapControllers();

app.Run();
