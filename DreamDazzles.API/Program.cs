using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using Asp.Versioning.Conventions;
using System;
using DreamDazzles.API.Utility.Extensions;
using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using DreamDazzle.Model.Data;
using Serilog;
using DreamDazzles.Service;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using DreamDazzle.Model;
using Microsoft.IdentityModel.Logging;
using DreamDazzles.Service.Emails;
using DreamDazzles.Service.Interface;
using DreamDazzles.Service.Service;


var Envplatform = string.Empty;
StaticLogger.EnsureInitialized();
Log.Information("Dream Dazzles API is booting up...");

try
{


    string[]? origins = new string[3];
    var builder = WebApplication.CreateBuilder(args);

    Envplatform = builder.Configuration["app:name"];

    // Add services to the container.
    // Clear default logging providers and configure your logger (e.g., Serilog).
    builder.Logging.ClearProviders();
    // Configure your logger here (e.g., Serilog, NLog, etc.).
    #region ConfigSerilog
    Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
    builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));
    #endregion
    var services = builder.Services;
    IdentityModelEventSource.ShowPII = true;
    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    //services.AddSwaggerGen();
    var configuration = builder.Configuration;
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
    services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<MainDBContext>()
        .AddDefaultTokenProviders();

    services.Configure<IdentityOptions>(options =>
    {
        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
    });



    builder.Services.Configure<IdentityOptions>(opts=>opts.SignIn.RequireConfirmedEmail = true);

    //add email config

    var emailconfig = configuration.GetSection("EmailConfigration").Get<EmailConfigration>();
    builder.Services.AddSingleton(emailconfig);
    builder.Services.AddScoped<IEmailService, EmailService>();


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
        app.Logger.LogInformation("PublicApi App created...");
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

    StaticLogger.EnsureInitialized();
    Log.Fatal(ex.InnerException + " Ex message " + ex.Message + ", Platoform name :" + Envplatform, "Unhandled exception");
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    //NLog.LogManager.Shutdown();
    StaticLogger.EnsureInitialized();
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}