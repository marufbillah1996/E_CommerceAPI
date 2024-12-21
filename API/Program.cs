using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.SeedData;
using Microsoft.EntityFrameworkCore;

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

app.MapControllers();
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
