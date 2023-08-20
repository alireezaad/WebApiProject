using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Config;
using WebApi.Model.DBContext;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddSingleton<MyDBContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    var connectionString = builder.Configuration.GetConnectionString("MyApiConnectionString");
    builder.Services.AddDbContext<MyDBContext>(opt => opt.UseSqlServer(connectionString));
   // builder.Services.AddScoped<UserApiOptions>();
    services.Configure<UserApiOptions>(builder.Configuration.GetSection("UserApiOptions"));
    builder.Services.AddApiVersioning(opt => {
        opt.AssumeDefaultVersionWhenUnspecified = true;
        opt.DefaultApiVersion = new ApiVersion(1, 0);
        opt.ApiVersionReader = ApiVersionReader.Combine(
            new QueryStringApiVersionReader("api-version"),
            new HeaderApiVersionReader("header-ver"),
            new MediaTypeApiVersionReader("ver"));
    });

    builder.Services.AddTransient<IUserServices, UserServices>();
    builder.Services.AddHttpClient<IUserServices,UserServices>();

}