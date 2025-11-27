using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SeleniumTests.DependencyInjection;
using SeleniumTests.Driver;

namespace SeleniumTests.Tests
{
    /// <summary>
    /// Base test class that sets up the dependency injection container for all tests
    /// </summary>
    public abstract class BaseTest
    {
        private IServiceScope? _scope;
        protected IServiceProvider ServiceProvider { get; private set; } = null!;
        protected IDriverFactory DriverFactory { get; private set; } = null!;

        /// <summary>
        /// Sets up the dependency injection container before each test
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            var services = new ServiceCollection();

            // Configure services with custom options
            ConfigureServices(services);

            var rootProvider = services.BuildServiceProvider();
            _scope = rootProvider.CreateScope();
            ServiceProvider = _scope.ServiceProvider;

            // Get the driver factory and create a new driver
            DriverFactory = ServiceProvider.GetRequiredService<IDriverFactory>();
            DriverFactory.CreateDriver();
        }

        /// <summary>
        /// Override this method to customize service configuration
        /// </summary>
        /// <param name="services">The service collection to configure</param>
        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddSeleniumServices(options =>
            {
                options.BrowserType = BrowserType.Chrome;
                options.Headless = TestConfig.Headless;
                options.ImplicitWaitSeconds = 10;
                options.PageLoadTimeoutSeconds = 30;
                options.MaximizeWindow = true;
            });
        }

        /// <summary>
        /// Gets a service from the dependency injection container
        /// </summary>
        /// <typeparam name="T">The service type</typeparam>
        /// <returns>The service instance</returns>
        protected T GetService<T>() where T : notnull
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        /// <summary>
        /// Cleans up the WebDriver after each test
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            DriverFactory?.QuitDriver();
            _scope?.Dispose();
        }
    }
}
