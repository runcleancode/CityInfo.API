using CityInfoNew.Repositories.Extensions;
using CityInfoNew.Services.Extensions;
using CityInfoNew.WebApi.Extensions;
using CityInfoNew.WebApi.Extensions.Swagger;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Logging
builder.Host.ConfigureLogging();

//Controllers & Content Negotiation
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
    options.Filters
        .Add(new ProducesAttribute("application/json", "application/xml"));
})
.AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();

//Data Access Layer (Repositories)
builder.Services.AddRepositoryServices(builder.Configuration);

//Business Logic Layer (Services)
builder.Services.AddBusinessServices(builder.Configuration);

//Presentation / Infrastructure Services
builder.Services.AddWebApiServices(builder.Configuration);
builder.Services.AddProblemDetails();

var app = builder.Build();

//Seed the database before running the application
await app.SeedDatabaseAsync();

//Global Exception Handler
app.UseExceptionHandler();

app.UseSerilogRequestLogging();

//Swagger UI Configuration
app.UseConfiguredSwaggerUi();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();