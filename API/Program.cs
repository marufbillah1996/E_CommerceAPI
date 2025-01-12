using API.Middleware;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.SeedData;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();//a choice of the lifetime of how long is this service going to be available for. 
                                                                    //Scoped: the service is going to live for as long as the HTTP request. It's scoped to the lifetime of the HTTP request.
                                                                    //Transient: this is effectively scoped to the method level, not the request level.
                                                                    //Singleton: this is going to create the service when the application starts up and will not dispose of the service until the application shuts down.
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddCors();

builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    var conString = builder.Configuration.GetConnectionString("Redis") ?? throw new Exception("Cannot get Redis connection string");
    var configuration = ConfigurationOptions.Parse(conString, true);
    return ConnectionMultiplexer.Connect(configuration);
}); 
builder.Services.AddSingleton<ICartService,CartService>();

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<StoreContext>();  

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//middleware - a middleware is just software that runs of potentially can run on the request as it's coming through.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x=>x.AllowAnyHeader().AllowAnyHeader()
    .WithOrigins("http://localhost:4200","https://localhost:4200"));

app.MapControllers();

app.MapGroup("api").MapIdentityApi<AppUser>(); // api/login

//for seeding data
try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

app.Run();
