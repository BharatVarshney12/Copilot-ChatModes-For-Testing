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
            // Act
            _loginPage.NavigateTo(TestConfig.LoginUrl);
            _loginPage.Login(TestConfig.ValidUsername, TestConfig.ValidPassword);

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
            // Act
            _loginPage.NavigateTo(TestConfig.LoginUrl);
            _loginPage.Login(TestConfig.InvalidUsername, TestConfig.InvalidPassword);

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
            // Act
            _loginPage.NavigateTo(TestConfig.LoginUrl);
            _loginPage.EnterPassword(TestConfig.ValidPassword);
            _loginPage.ClickLoginButton();

            // Assert
            Assert.That(_loginPage.IsErrorMessageDisplayed(), Is.True);
        }
    }
}
