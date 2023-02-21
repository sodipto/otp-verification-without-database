using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json.Serialization;
using otp_verify_without_database.Utils;

namespace otp_verify_without_database
{
    public class StartUp
    {
        public StartUp(IConfiguration configuration, IWebHostEnvironment env)
        {
            Console.WriteLine("Env================" + env.EnvironmentName);
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables();

            Configuration = builder.Build();
            GlobalConfig.SetConfiguration(configuration);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Cors
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            #endregion

            #region Controller json options
            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.PropertyNamingPolicy = null;
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = GlobalConfig.GetConfiguration("Swagger:Title"),
                    Version = GlobalConfig.GetConfiguration("Swagger:Version")
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                 });
            });
            #endregion

            #region Routing
            services.AddRouting(options => options.LowercaseUrls = true);
            #endregion

            #region Payload error configure
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            #endregion

            #region Health
            services.AddHealthChecks();
            #endregion

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Cors
            app.UseCors(x => x
               .SetIsOriginAllowed(origin => true)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());

            #endregion

        
            #region Route not found and unautorized Error
            app.UseStatusCodePages(new StatusCodePagesOptions()
            {
                HandleAsync = async (context) =>
                {
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                    if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                    {
                        var errorDto = ErrorHelper.GetErrorResponse(404, "RouteNotFoundException", "Route not found.");

                        await context.HttpContext.Response.WriteAsJsonAsync(errorDto, options: options);
                    }
                    else if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                    {
                        var errorDto = ErrorHelper.GetErrorResponse(401, "UnAuthorizedException", "You are not logged in or not authorized.");

                        await context.HttpContext.Response.WriteAsJsonAsync(errorDto, options: options);
                    }
                }
            });
            #endregion

            #region Swagger
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OTP Verify Without Database v1"));
            }
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
