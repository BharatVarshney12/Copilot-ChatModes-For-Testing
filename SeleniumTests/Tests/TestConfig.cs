namespace SeleniumTests.Tests
{
    /// <summary>
    /// Centralized test configuration containing URLs, credentials, and other test data
    /// </summary>
    public static class TestConfig
    {
        /// <summary>
        /// Base URL for the test application
        /// </summary>
        public static string BaseUrl => Environment.GetEnvironmentVariable("TEST_BASE_URL") ?? "https://example.com";

        /// <summary>
        /// Login page URL
        /// </summary>
        public static string LoginUrl => $"{BaseUrl}/login";

        /// <summary>
        /// Valid test username
        /// </summary>
        public static string ValidUsername => Environment.GetEnvironmentVariable("TEST_USERNAME") ?? "testuser@example.com";

        /// <summary>
        /// Valid test password
        /// </summary>
        public static string ValidPassword => Environment.GetEnvironmentVariable("TEST_PASSWORD") ?? "SecurePassword123";

        /// <summary>
        /// Invalid test username for negative tests
        /// </summary>
        public static string InvalidUsername => "invalid@example.com";

        /// <summary>
        /// Invalid test password for negative tests
        /// </summary>
        public static string InvalidPassword => "wrongpassword";

        /// <summary>
        /// Whether to run browser in headless mode (configurable via environment variable)
        /// </summary>
        public static bool Headless => Environment.GetEnvironmentVariable("TEST_HEADLESS")?.ToLowerInvariant() != "false";
    }
}
