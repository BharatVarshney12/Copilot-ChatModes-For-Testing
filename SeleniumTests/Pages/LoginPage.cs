using OpenQA.Selenium;
using SeleniumTests.Driver;

namespace SeleniumTests.Pages
{
    /// <summary>
    /// Page Object for the Login page
    /// </summary>
    public class LoginPage : BasePage
    {
        // Locators
        private readonly By _usernameField = By.Id("username");
        private readonly By _passwordField = By.Id("password");
        private readonly By _loginButton = By.CssSelector("button[type='submit']");
        private readonly By _errorMessage = By.ClassName("error-message");

        /// <summary>
        /// Initializes a new instance of the LoginPage with the injected driver factory
        /// </summary>
        /// <param name="driverFactory">The driver factory instance</param>
        public LoginPage(IDriverFactory driverFactory) : base(driverFactory)
        {
        }

        /// <summary>
        /// Navigates to the login page
        /// </summary>
        /// <param name="url">The login page URL</param>
        public void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Enters the username
        /// </summary>
        /// <param name="username">The username to enter</param>
        public void EnterUsername(string username)
        {
            var element = WaitForElementVisible(_usernameField);
            element.Clear();
            element.SendKeys(username);
        }

        /// <summary>
        /// Enters the password
        /// </summary>
        /// <param name="password">The password to enter</param>
        public void EnterPassword(string password)
        {
            var element = WaitForElementVisible(_passwordField);
            element.Clear();
            element.SendKeys(password);
        }

        /// <summary>
        /// Clicks the login button
        /// </summary>
        public void ClickLoginButton()
        {
            WaitForElementClickable(_loginButton).Click();
        }

        /// <summary>
        /// Performs a complete login action
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }

        /// <summary>
        /// Checks if the error message is displayed
        /// </summary>
        /// <returns>True if error message is visible</returns>
        public bool IsErrorMessageDisplayed()
        {
            try
            {
                return WaitForElementVisible(_errorMessage).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the error message text
        /// </summary>
        /// <returns>The error message text</returns>
        public string GetErrorMessageText()
        {
            return WaitForElementVisible(_errorMessage).Text;
        }
    }
}
