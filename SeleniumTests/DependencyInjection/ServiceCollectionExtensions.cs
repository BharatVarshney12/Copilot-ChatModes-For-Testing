using Microsoft.Extensions.DependencyInjection;
using SeleniumTests.Driver;
using SeleniumTests.Pages;

namespace SeleniumTests.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring dependency injection services
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Selenium WebDriver services to the dependency injection container
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configureOptions">Optional action to configure driver options</param>
        /// <returns>The service collection for chaining</returns>
        public static IServiceCollection AddSeleniumServices(
            this IServiceCollection services,
            Action<DriverOptions>? configureOptions = null)
        {
            // Register driver options
            var options = new DriverOptions();
            configureOptions?.Invoke(options);
            services.AddSingleton(options);

            // Register driver factory as scoped (new instance per test)
            services.AddScoped<IDriverFactory, DriverFactory>();

            // Register page objects
            services.AddTransient<LoginPage>();
            services.AddTransient<DashboardPage>();

            return services;
        }
    }
}
