using OpenQA.Selenium;
using SeleniumTests.Driver;

namespace SeleniumTests.Pages
{
    /// <summary>
    /// Page Object for the Dashboard page
    /// </summary>
    public class DashboardPage : BasePage
    {
        // Locators
        private readonly By _welcomeMessage = By.ClassName("welcome-message");
        private readonly By _logoutButton = By.Id("logout");
        private readonly By _userProfile = By.Id("user-profile");

        /// <summary>
        /// Initializes a new instance of the DashboardPage with the injected driver factory
        /// </summary>
        /// <param name="driverFactory">The driver factory instance</param>
        public DashboardPage(IDriverFactory driverFactory) : base(driverFactory)
        {
        }

        /// <summary>
        /// Checks if the welcome message is displayed
        /// </summary>
        /// <returns>True if welcome message is visible</returns>
        public bool IsWelcomeMessageDisplayed()
        {
            try
            {
                return WaitForElementVisible(_welcomeMessage).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the welcome message text
        /// </summary>
        /// <returns>The welcome message text</returns>
        public string GetWelcomeMessageText()
        {
            return WaitForElementVisible(_welcomeMessage).Text;
        }

        /// <summary>
        /// Clicks the logout button
        /// </summary>
        public void Logout()
        {
            WaitForElementClickable(_logoutButton).Click();
        }

        /// <summary>
        /// Checks if the user profile element is displayed
        /// </summary>
        /// <returns>True if user profile is visible</returns>
        public bool IsUserProfileDisplayed()
        {
            try
            {
                return WaitForElementVisible(_userProfile).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
