using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using Asp.Versioning.Conventions;
using System;
using DreamDazzles.API.Utility.Extensions;
using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using DreamDazzle.Model.Data;
using DreamDazzle.Model.User;
using Microsoft.AspNetCore.Identity;


try
{


    string[]? origins = new string[3];
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    var services = builder.Services;

    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    //services.AddSwaggerGen();
    services.AddDbContext<MainDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    services.ConfigureDIServices();
    services.AddIdentity<AspNetUsers, AspNetRoles>()
.AddDefaultTokenProviders();
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddHttpContextAccessor();
    services.AddApiVersioning(config =>
    {
        config.DefaultApiVersion = new ApiVersion(1, 0);
        config.AssumeDefaultVersionWhenUnspecified = true;
        config.ReportApiVersions = true;
        config.ApiVersionReader = new UrlSegmentApiVersionReader();// new HeaderApiVersionReader("api-version");
    });

    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("V1", new OpenApiInfo
        {
            Version = "V1",
            Title = builder.Configuration["app:name"],
            Description = builder.Configuration["app:desc"]
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "Bearer Authentication with JWT Token",
            Type = SecuritySchemeType.Http
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Id = "Bearer", Type = ReferenceType.SecurityScheme
                    }
                },
                new List < string > ()
            }});
    });
    origins = builder.Configuration.GetSection("app:CORSOrigins")?.GetChildren()?.Select(x => x.Value)?.ToArray();
    services.AddCors(options =>
    {
        options.AddPolicy("CORSPolicy", policy =>
        {
            policy.WithOrigins(origins);
            policy.WithMethods("GET", "POST", "DELETE", "PUT", "PATCH");
            policy.AllowAnyHeader(); // <--- list the allowed headers here
            policy.AllowAnyOrigin();
        });
    });
 
    var app = builder.Build();
    {
        app.UseCors("CORSPolicy");
        var versionSet = app.NewApiVersionSet()
                        .HasApiVersion(2.0)
                        .ReportApiVersions()
                        .Build();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = string.Empty;
            options.SwaggerEndpoint("/swagger/V1/swagger.json", builder.Configuration["app:name"] + ": " + app.Environment.EnvironmentName);
            options.DocExpansion(DocExpansion.None);//This will not expand all the API's.
        });


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            //app.UseSwaggerUI();

        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        //app.UseStaticFiles(new StaticFileOptions()
        //{
        //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
        //    RequestPath = new PathString("/wwwroot")

        //    #region CodeLevelExample        
        //    //var folderName = Path.Combine("wwwroot", "store");
        //    //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //    #endregion
        //});

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }


}
catch (Exception ex)
{

    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    //NLog.LogManager.Shutdown();
}