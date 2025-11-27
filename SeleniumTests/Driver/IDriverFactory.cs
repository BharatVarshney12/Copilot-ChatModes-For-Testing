using OpenQA.Selenium;

namespace SeleniumTests.Driver
{
    /// <summary>
    /// Interface for creating and managing WebDriver instances
    /// </summary>
    public interface IDriverFactory
    {
        /// <summary>
        /// Creates a new WebDriver instance based on the configured options
        /// </summary>
        /// <returns>A new IWebDriver instance</returns>
        IWebDriver CreateDriver();

        /// <summary>
        /// Gets the current WebDriver instance
        /// </summary>
        IWebDriver CurrentDriver { get; }

        /// <summary>
        /// Quits and disposes the current WebDriver instance
        /// </summary>
        void QuitDriver();
    }
}
