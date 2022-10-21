using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class SwaggerConfiguration
    {
        internal static IServiceCollection AddSwaggerGenConfiguration(this IServiceCollection service)
        {
            service.AddSwaggerGen(opts =>
            {
                opts.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new()
                {
                    Description = "JWT Authorization Header",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                opts.AddSecurityRequirement(new ()
                {
                    {
                        new()
                        {
                            Reference = new()
                                {
                                    Id = JwtBearerDefaults.AuthenticationScheme,
                                    Type = ReferenceType.SecurityScheme
                                }
                        },
                        new List<string>()
                    }
                });
            });

            return service;
        }
    }
}