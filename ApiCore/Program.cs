
using ApiCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddDbContext<VueDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("CadenaMySql"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"))
        .EnableSensitiveDataLogging() // <-- These two calls are optional but help
        .EnableDetailedErrors()
        );


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

{
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());


    // custom jwt auth middleware
    //app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();

    app.UseAuthentication();
    app.UseRouting();
    app.UseAuthorization();
    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
}
app.Run();
