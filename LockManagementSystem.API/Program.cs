using LockManagementSystem.Application.Extensions;
using LockManagementSystem.Extensions;
using LockManagementSystem.Infrastructure.Extensions;
using LockManagementSystem.Infrastructure.Persistence;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers()
    .AddNewtonsoftJson(opt 
        => opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureJwtAuthentication(configuration);
builder.Services.ConfigureSwagger();
builder.Services.AddPersistenceInfrastructure(configuration);
builder.Services.RegisterMediatR();
builder.Services.RegisterAutoMapper();
builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MigrateDatabase();
}

app.UseHttpsRedirection();

app.ConfigureExceptionMiddleware();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();