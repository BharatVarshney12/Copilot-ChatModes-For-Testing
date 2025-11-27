using NUnit.Framework;
using SeleniumTests.Pages;

namespace SeleniumTests.Tests
{
    /// <summary>
    /// Test class for Login functionality demonstrating dependency injection
    /// </summary>
    [TestFixture]
    public class LoginTests : BaseTest
    {
        private LoginPage _loginPage = null!;

        /// <summary>
        /// Sets up the test by getting the LoginPage from the DI container
        /// </summary>
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _loginPage = GetService<LoginPage>();
        }

        /// <summary>
        /// Test that verifies successful login navigates to dashboard
        /// </summary>
        [Test]
        [Category("Login")]
        public void ValidLogin_ShouldNavigateToDashboard()
        {
            // Arrange
            const string loginUrl = "https://example.com/login";
            const string username = "testuser@example.com";
            const string password = "SecurePassword123";

            // Act
            _loginPage.NavigateTo(loginUrl);
            _loginPage.Login(username, password);

            // Assert
            Assert.That(_loginPage.CurrentUrl, Does.Contain("/dashboard"));
        }

        /// <summary>
        /// Test that verifies invalid login shows error message
        /// </summary>
        [Test]
        [Category("Login")]
        public void InvalidLogin_ShouldDisplayErrorMessage()
        {
            // Arrange
            const string loginUrl = "https://example.com/login";
            const string username = "invalid@example.com";
            const string password = "wrongpassword";

            // Act
            _loginPage.NavigateTo(loginUrl);
            _loginPage.Login(username, password);

            // Assert
            Assert.That(_loginPage.IsErrorMessageDisplayed(), Is.True);
            Assert.That(_loginPage.GetErrorMessageText(), Does.Contain("Invalid credentials"));
        }

        /// <summary>
        /// Test that verifies empty username shows validation error
        /// </summary>
        [Test]
        [Category("Login")]
        [Category("Validation")]
        public void EmptyUsername_ShouldShowValidationError()
        {
            // Arrange
            const string loginUrl = "https://example.com/login";
            const string password = "SomePassword123";

            // Act
            _loginPage.NavigateTo(loginUrl);
            _loginPage.EnterPassword(password);
            _loginPage.ClickLoginButton();

            // Assert
            Assert.That(_loginPage.IsErrorMessageDisplayed(), Is.True);
        }
    }
}
