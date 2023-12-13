using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;

namespace Mc2.CrudTest.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        //services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddMediatR(Assembly.GetExecutingAssembly());
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        //services.AddMediatR(typeof(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddScoped<IMediator, Mediator>();
        return services;
    }
}