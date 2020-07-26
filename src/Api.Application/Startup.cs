using System;
using Api.CrossCutting.DependencyInjection;
using Api.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
               ConfigureService.ConfigureDependenciesService(services);
               ConfigureRepository.ConfigureDependenciesRepository(services);

               services.AddControllers();

               services.AddSwaggerGen(s => 
               {
                    s.SwaggerDoc("v1", new OpenApiInfo
                    {
                         Version = "v1",
                         Title = "Curso de API com AspNetCore 3.1",
                         Description = "Arquitetura DDD",
                         TermsOfService = new Uri("https://github.com/aislanmi/dotnetcore-webapi-ddd"),
                         Contact = new OpenApiContact
                         {
                              Name = "Aislan Michel Moreira Freitas",
                              Email = "aislan.michel92@gmail.com",
                              Url = new Uri("https://github.com/aislanmi")
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
