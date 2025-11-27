using NUnit.Framework;
using SeleniumTests.Pages;

namespace SeleniumTests.Tests
{
    /// <summary>
    /// Test class for Dashboard functionality demonstrating dependency injection
    /// </summary>
    [TestFixture]
    public class DashboardTests : BaseTest
    {
        private LoginPage _loginPage = null!;
        private DashboardPage _dashboardPage = null!;

        /// <summary>
        /// Sets up the test by getting page objects from the DI container
        /// </summary>
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _loginPage = GetService<LoginPage>();
            _dashboardPage = GetService<DashboardPage>();
        }

        /// <summary>
        /// Test that verifies welcome message is displayed after login
        /// </summary>
        [Test]
        [Category("Dashboard")]
        public void AfterLogin_ShouldDisplayWelcomeMessage()
        {
            // Arrange
            const string loginUrl = "https://example.com/login";
            const string username = "testuser@example.com";
            const string password = "SecurePassword123";

            // Act
            _loginPage.NavigateTo(loginUrl);
            _loginPage.Login(username, password);

            // Assert
            Assert.That(_dashboardPage.IsWelcomeMessageDisplayed(), Is.True);
            Assert.That(_dashboardPage.GetWelcomeMessageText(), Does.Contain("Welcome"));
        }

        /// <summary>
        /// Test that verifies user profile is displayed on dashboard
        /// </summary>
        [Test]
        [Category("Dashboard")]
        public void AfterLogin_ShouldDisplayUserProfile()
        {
            // Arrange
            const string loginUrl = "https://example.com/login";
            const string username = "testuser@example.com";
            const string password = "SecurePassword123";

            // Act
            _loginPage.NavigateTo(loginUrl);
            _loginPage.Login(username, password);

            // Assert
            Assert.That(_dashboardPage.IsUserProfileDisplayed(), Is.True);
        }

        /// <summary>
        /// Test that verifies logout returns to login page
        /// </summary>
        [Test]
        [Category("Dashboard")]
        [Category("Logout")]
        public void Logout_ShouldReturnToLoginPage()
        {
            // Arrange
            const string loginUrl = "https://example.com/login";
            const string username = "testuser@example.com";
            const string password = "SecurePassword123";

            // Act
            _loginPage.NavigateTo(loginUrl);
            _loginPage.Login(username, password);
            _dashboardPage.Logout();

            // Assert
            Assert.That(_loginPage.CurrentUrl, Does.Contain("/login"));
        }
    }
}
