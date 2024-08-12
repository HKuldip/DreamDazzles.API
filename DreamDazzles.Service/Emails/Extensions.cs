using DreamDazzles.Service.Emails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoanCentral.BAL.Emails
{
    public static class Extensions
    {
        private static readonly string EmailSettingsSectionName = "AppSettings:Smtp";


        public static IServiceCollection AddEmailClient(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.Configure<EmailSettingsOptions>(configuration.GetSection(EmailSettingsSectionName));
            return services.AddSingleton<IEmailSender, EmailSender>();
        }

        public static string Underscore(this string value)
        => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model); //namespace Microsoft.Extensions.Configuration.Binder
            //or
            //var name = configuration.GetSection(section).Get<TModel>();
            return model;
        }
    }
}
