using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Driver;

namespace SeleniumTests.Pages
{
    /// <summary>
    /// Base class for all page objects providing common functionality
    /// </summary>
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;

        /// <summary>
        /// Initializes a new instance of the BasePage with the injected driver factory
        /// </summary>
        /// <param name="driverFactory">The driver factory instance</param>
        protected BasePage(IDriverFactory driverFactory)
        {
            Driver = driverFactory.CurrentDriver;
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Gets the current page URL
        /// </summary>
        public string CurrentUrl => Driver.Url;

        /// <summary>
        /// Gets the current page title
        /// </summary>
        public string PageTitle => Driver.Title;

        /// <summary>
        /// Waits for an element to be visible
        /// </summary>
        /// <param name="locator">The element locator</param>
        /// <returns>The visible element</returns>
        protected IWebElement WaitForElementVisible(By locator)
        {
            return Wait.Until(driver =>
            {
                var element = driver.FindElement(locator);
                return element.Displayed ? element : null;
            })!;
        }

        /// <summary>
        /// Waits for an element to be clickable
        /// </summary>
        /// <param name="locator">The element locator</param>
        /// <returns>The clickable element</returns>
        protected IWebElement WaitForElementClickable(By locator)
        {
            return Wait.Until(driver =>
            {
                var element = driver.FindElement(locator);
                return element.Displayed && element.Enabled ? element : null;
            })!;
        }
    }
}
