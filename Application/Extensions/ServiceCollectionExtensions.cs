using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IJwtService, JwtService>();
            service.AddScoped<IUserService, UserService>();

            return service;
        }
    }
}
