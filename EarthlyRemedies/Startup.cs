﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EarthlyRemedies.Helpers;
using EarthlyRemedies.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EarthlyRemedies.Models;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;

// namespace EarthlyRemedies
// {
//   public class Startup
//   {
//     public Startup(IConfiguration configuration)
//     {
//       Configuration = configuration;
//     }

//     public IConfiguration Configuration { get; }

//     // This method gets called by the runtime. Use this method to add services to the container.
//     public void ConfigureServices(IServiceCollection services)
//     {
//       services.AddDbContext<EarthlyRemediesContext>(opt =>
//           opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
//       services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

//     }

//     // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//     public void Configure(IApplicationBuilder app, IHostingEnvironment env)
//     {
//       if (env.IsDevelopment())
//       {
//         app.UseDeveloperExceptionPage();
//       }
//       else
//       {
//         // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//         app.UseHsts();
//       }

//       // app.UseHttpsRedirection();
//       app.UseMvc();
//     }
//   }
// }

namespace EarthlyRemedies
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
      services.AddCors();
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      services.AddDbContext<EarthlyRemediesContext>(opt =>
      opt.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "Earthly Remedies",
          Version = "v1",
          Description = "A database of user-posted natural and home remedies",
          Contact = new OpenApiContact
          {
            Name = "Julia, DJ & Jason",
          }
        });
      });

      // configure strongly typed settings objects
      var appSettingsSection = Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettingsSection);

      // configure jwt authentication
      var appSettings = appSettingsSection.Get<AppSettings>();
      var key = Encoding.ASCII.GetBytes(appSettings.Secret);
      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });

      // configure DI for application services
      services.AddScoped<IUserService, UserService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      // global cors policy
      app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

      app.UseAuthentication();

      app.UseSwagger();

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Earthly Remedies V1");
        c.RoutePrefix = string.Empty;
      });

      app.UseMvc();

    }
  }
}