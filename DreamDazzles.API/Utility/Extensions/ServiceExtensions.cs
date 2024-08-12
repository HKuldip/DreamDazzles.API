using DreamDazzle.Repository.Interface;
using DreamDazzle.Repository.Repositories;
using DreamDazzles.API.Utility.Helper;
using DreamDazzles.Repository.Interface;
using DreamDazzles.Repository.Repositories;
using DreamDazzles.Service.Interface.Product;
using DreamDazzles.Service.Interface.User;
using DreamDazzles.Service.Service;
using LoanCentral.API.Utility.Helper;
using LoanCentral.API.Utility.Model;

namespace DreamDazzles.API.Utility.Extensions;
public static class ServiceExtensions
{
    private static readonly string JWTSectionName = "jwt";
    private static readonly string appsettingsSectionName = "appsettings";
    //Logging
    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        //services.AddSingleton<ILoggerManager, LoggerManager>();        
    }
    public static IServiceCollection AddJwt(this IServiceCollection services)
    {
        IConfiguration configuration;
        using (var serviceProvider = services.BuildServiceProvider())
        {
            configuration = serviceProvider.GetService<IConfiguration>();
        }
        var section = configuration.GetSection(JWTSectionName);
        var options = configuration.GetOptions<JwtOptions>(JWTSectionName);

        var section1 = configuration.GetSection(appsettingsSectionName);
        var options1 = configuration.GetOptions<AppSettings>(appsettingsSectionName);

        services.Configure<JwtOptions>(section);
        services.Configure<AppSettings>(section1);
        return services;
    }

    public static void ConfigureDIServices(this IServiceCollection services)
    {
        //var automapper = new MapperConfiguration(item => item.AddProfile(new AutoMappings()));
        //IMapper mapper = automapper.CreateMapper();
        //services.AddSingleton(mapper);
        //services.AddEmailClient();

        services.AddTransient<IAuthenticationService, AuthenticationService>();

        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IProductRepository, ProductRepository>();

        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<IUsersRepository, UsersRepository>();

        //services.AddTransient<IAccountSettingService, AccountSettingService>();
        //services.AddTransient<IAccountSettingRepository, AccountSettingRepository>();
    }
}

public static class Extensions
{
    public static string Underscore(this string value)
        => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));

    public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
    {
        var model = new TModel();
        configuration.GetSection(section).Bind(model);

        return model;
    }
}
