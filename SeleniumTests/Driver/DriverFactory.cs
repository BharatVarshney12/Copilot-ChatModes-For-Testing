using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTests.Driver
{
    /// <summary>
    /// Factory class for creating and managing WebDriver instances with dependency injection support
    /// </summary>
    public class DriverFactory : IDriverFactory, IDisposable
    {
        private readonly DriverOptions _options;
        private IWebDriver? _driver;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the DriverFactory with the specified options
        /// </summary>
        /// <param name="options">Configuration options for the WebDriver</param>
        public DriverFactory(DriverOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Gets the current WebDriver instance
        /// </summary>
        public IWebDriver CurrentDriver => _driver ?? throw new InvalidOperationException("Driver has not been created. Call CreateDriver() first.");

        /// <summary>
        /// Creates a new WebDriver instance based on the configured browser type
        /// </summary>
        /// <returns>A new IWebDriver instance</returns>
        public IWebDriver CreateDriver()
        {
            if (_driver != null)
            {
                QuitDriver();
            }

            _driver = _options.BrowserType switch
            {
                BrowserType.Chrome => CreateChromeDriver(),
                BrowserType.Firefox => CreateFirefoxDriver(),
                BrowserType.Edge => CreateEdgeDriver(),
                _ => throw new ArgumentException($"Unsupported browser type: {_options.BrowserType}")
            };

            ConfigureDriver(_driver);
            return _driver;
        }

        /// <summary>
        /// Quits and disposes the current WebDriver instance
        /// </summary>
        public void QuitDriver()
        {
            if (_driver != null)
            {
                try
                {
                    _driver.Quit();
                }
                finally
                {
                    _driver.Dispose();
                    _driver = null;
                }
            }
        }

        private IWebDriver CreateChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var options = new ChromeOptions();

            if (_options.Headless)
            {
                options.AddArgument("--headless=new");
            }

            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            return new ChromeDriver(options);
        }

        private IWebDriver CreateFirefoxDriver()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            var options = new FirefoxOptions();

            if (_options.Headless)
            {
                options.AddArgument("--headless");
            }

            return new FirefoxDriver(options);
        }

        private IWebDriver CreateEdgeDriver()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            var options = new EdgeOptions();

            if (_options.Headless)
            {
                options.AddArgument("--headless=new");
            }

            return new EdgeDriver(options);
        }

        private void ConfigureDriver(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_options.ImplicitWaitSeconds);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_options.PageLoadTimeoutSeconds);

            if (_options.MaximizeWindow)
            {
                driver.Manage().Window.Maximize();
            }
        }

        /// <summary>
        /// Disposes the DriverFactory and quits the WebDriver
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    QuitDriver();
                }
                _disposed = true;
            }
        }
    }
}
