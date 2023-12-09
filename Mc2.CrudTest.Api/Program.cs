using Mc2.CrudTest.Api.Filters;
using Mc2.CrudTest.Application;
using Mc2.CrudTest.Infrastructure;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using Mc2.CrudTest.Infrastructure.Persistence;
using System.Reflection;
using FluentValidation;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Superheroes",
        Description = "Demo API - Clean Architecture Solution in .NET 6",
    });
});

#pragma warning disable CS0618 // Type or member is obsolete
//builder.Services.AddControllersWithViews(options =>
//            options.Filters.Add<ApiExceptionFilterAttribute>())
//                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

#pragma warning restore CS0618 // Type or member is obsolete
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
});
//.AddFluentValidation(configurationExpression: x => x.AutomaticValidationEnabled = true);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scopeProvider = scope.ServiceProvider;
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    //  await SeedSampleDataAsync(dbContext);
}

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
