using System;
using System.Collections.Generic;
using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Mappings;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Api.Application
{
     public class Startup
     {
          public Startup(IConfiguration configuration)
          {
               Configuration = configuration;
          }

          public IConfiguration Configuration { get; }

          public void ConfigureServices(IServiceCollection services)
          {
               services.AddControllers();

               ConfigureService.ConfigureDependenciesService(services);
               ConfigureRepository.ConfigureDependenciesRepository(services);

               var config = new AutoMapper.MapperConfiguration(conf => 
               {
                    conf.AddProfile(new DtoToModelProfile());
                    conf.AddProfile(new EntityToDtoProfile());
                    conf.AddProfile(new ModelToEntityProfile());
               });

               IMapper mapper = config.CreateMapper();
               services.AddSingleton(mapper);

               var signingConfigurations = new SigningConfigurations();
               services.AddSingleton(signingConfigurations);

               var tokenConfiguration = new TokenConfiguration();
               new ConfigureFromConfigurationOptions<TokenConfiguration>(
                    Configuration
                    .GetSection("TokenConfigurations"))
                    .Configure(tokenConfiguration);

               services.AddSingleton(tokenConfiguration);

               services.AddAuthentication(authOptions => 
               {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               }).AddJwtBearer(bearerOptions => 
               {
                    var paramsValidation = bearerOptions.TokenValidationParameters;

                    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                    paramsValidation.ValidAudience = tokenConfiguration.Audience;
                    paramsValidation.ValidIssuer = tokenConfiguration.Issuer;
                    paramsValidation.ValidateIssuerSigningKey = true;
                    paramsValidation.ValidateLifetime = true;
                    paramsValidation.ClockSkew = TimeSpan.Zero;
               });

               services.AddAuthorization(auth => 
               {
                    auth.AddPolicy("Bearer", 
                         new AuthorizationPolicyBuilder()
                         .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                         .RequireAuthenticatedUser().Build());
               });

               services.AddSwaggerGen(s => 
               {
                    s.SwaggerDoc("v1", new OpenApiInfo
                    {
                         Version = "v1",
                         Title = "API com AspNetCore 3.1 e Arquitetura DDD",
                         Description = "apenas o teste do teste",
                         TermsOfService = new Uri("https://github.com/aislanmi/dotnetcore-webapi-ddd"),
                         Contact = new OpenApiContact
                         {
                              Name = "Aislan Michel Moreira Freitas",
                              Email = "aislan.michel92@gmail.com",
                              Url = new Uri("https://github.com/aislanmi")
                         }
                    });

                    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                         Description = "Entre com o Token",
                         Name = "Authorization",
                         In = ParameterLocation.Header,
                         Type = SecuritySchemeType.ApiKey
                    });

                    s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                         {
                              new OpenApiSecurityScheme()
                              {
                                   Reference = new OpenApiReference() {
                                        Id = "Bearer",
                                        Type = ReferenceType.SecurityScheme
                                   }
                              }, new List<string>()
                         }
                    });
               });
          }

          public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
          {
               if (env.IsDevelopment())
               {
                    app.UseDeveloperExceptionPage();
               }

               app.UseSwagger();
               app.UseSwaggerUI(s => 
               {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso de API com AspNetCore 3.1");
                    s.RoutePrefix = string.Empty;
               });

               app.UseRouting();

               app.UseAuthorization();

               app.UseEndpoints(endpoints =>
               {
                    endpoints.MapControllers();
               });
          }
     }
}
