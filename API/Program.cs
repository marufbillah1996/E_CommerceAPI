using Core.Interfaces;
using Infrastructure.Data;
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
                                                                    // Lea
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();

app.MapControllers();

app.Run();
