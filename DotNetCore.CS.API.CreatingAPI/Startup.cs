using AutoMapper;
using DotNetCore.CS.API.CreatingAPI.DbContexts;
using DotNetCore.CS.API.CreatingAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DotNetCore.CS.API.CreatingAPI
{
  public class Startup
  {


    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers(setupAction =>
      {
        //setting this true return the 406 to user instead of returning default formats
        setupAction.ReturnHttpNotAcceptable = true;
      }).AddXmlDataContractSerializerFormatters();//This add the support format Serializer


      //using AppDomain.currentDomain.getassemblies() we are telling our system to 
      //scan for all assemblies to add profiles
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();

      services.AddDbContext<CourseLibraryContext>(options =>
      {
        options.UseSqlServer(
            @"Server=(localdb)\mssqllocaldb;Database=CourseLibraryDB;Trusted_Connection=True;");
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler(appBuilder =>
        {
          appBuilder.Run(async context =>
         {
           context.Response.StatusCode = 500;
           await context.Response.WriteAsync("An unexpected exception has happened");
         });
        });

        //This is also a good place to write custom log for your developer or any logger which may be available
      }


      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
